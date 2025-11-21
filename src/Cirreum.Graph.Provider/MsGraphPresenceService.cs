namespace Cirreum.Graph.Provider;

using Cirreum.Presence;
using Cirreum.Security;
using Humanizer;

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
				Activity: presence?.Activity?.Humanize(),
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

}