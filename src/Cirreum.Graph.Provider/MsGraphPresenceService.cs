namespace Cirreum.Graph.Provider;

using Cirreum.Presence;
using Cirreum.Security;

public sealed class MsGraphPresenceService(
	IUserState user,
	IUserPresenceState presenceState,
	IGraphServiceClientProvider provider
) : IUserPresenceService {

	/// <inheritdoc/>
	public bool IsEnabled { get; } = true;

	/// <inheritdoc/>
	public async Task UpdateUserPresence() {
		if (this.IsEnabled && user.IsAuthenticated) {
			var presence = await provider.UseClientAsync(c => c.Me.Presence.GetAsync());
			presenceState.SetPresence(new(
				Status: MapAvailability(presence?.Availability),
				Activity: HumanizeIdentifier(presence?.Activity),
				Message: presence?.StatusMessage?.Message?.Content
			));
		}
	}

	public static PresenceStatus MapAvailability(string? msGraphAvailability) =>
		msGraphAvailability?.ToLower() switch {
			"available" or "availableidle" => PresenceStatus.Available,
			"busy" or "busyidle" => PresenceStatus.Busy,
			"away" or "berightback" => PresenceStatus.Away,
			"donotdisturb" => PresenceStatus.DoNotDisturb,
			"offline" => PresenceStatus.Offline,
			"presenceunknown" => PresenceStatus.Unknown,
			_ => PresenceStatus.Unknown
		};

	private static string? HumanizeIdentifier(string? value) {
		if (string.IsNullOrWhiteSpace(value)) {
			return value;
		}

		var sb = new System.Text.StringBuilder(value.Length + 8);

		for (var i = 0; i < value.Length; i++) {
			var c = value[i];

			if (i > 0 && char.IsUpper(c) && !char.IsWhiteSpace(value[i - 1])) {
				sb.Append(' ');
			}

			sb.Append(i == 0 ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c));
		}

		return sb.ToString();
	}

}