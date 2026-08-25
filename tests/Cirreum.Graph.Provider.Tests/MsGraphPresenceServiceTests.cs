namespace Cirreum.Graph.Provider.Tests;

using Cirreum.Presence;
using Cirreum.Security;
using Microsoft.Graph;
using Microsoft.Graph.Models;

public class MsGraphPresenceServiceTests {

	// ---------------------------------------------------------------------
	// Harness
	// ---------------------------------------------------------------------

	// The delegate is never invoked: what is under test is how a Graph presence is mapped and
	// published, not the SDK call that fetches it.
	private sealed class StubClientProvider(Presence? presence) : IGraphServiceClientProvider {

		public int Calls { get; private set; }

		public Task<T> UseClientAsync<T>(Func<GraphServiceClient, Task<T>> action) {
			this.Calls++;
			return Task.FromResult((T)(object?)presence!);
		}

		public Task UseClientAsync(Func<GraphServiceClient, Task> action) {
			this.Calls++;
			return Task.CompletedTask;
		}

	}

	private static (MsGraphPresenceService Service, IUserPresenceState State, StubClientProvider Provider)
		CreateService(Presence? presence, bool authenticated = true) {

		var user = Substitute.For<IUserState>();
		user.IsAuthenticated.Returns(authenticated);

		var state = Substitute.For<IUserPresenceState>();
		var provider = new StubClientProvider(presence);

		return (new MsGraphPresenceService(user, state, provider), state, provider);

	}

	private static Presence GraphPresence(string? availability = null, string? activity = null, string? message = null) {
		return new Presence {
			Availability = availability,
			Activity = activity,
			StatusMessage = message is null ? null : new PresenceStatusMessage {
				Message = new ItemBody { Content = message },
			},
		};
	}

	// ---------------------------------------------------------------------
	// Availability mapping
	// ---------------------------------------------------------------------

	[Theory]
	[InlineData("Available", PresenceStatus.Available)]
	[InlineData("AvailableIdle", PresenceStatus.Available)]
	[InlineData("Busy", PresenceStatus.Busy)]
	[InlineData("BusyIdle", PresenceStatus.Busy)]
	[InlineData("Away", PresenceStatus.Away)]
	[InlineData("BeRightBack", PresenceStatus.Away)]
	[InlineData("DoNotDisturb", PresenceStatus.DoNotDisturb)]
	[InlineData("Offline", PresenceStatus.Offline)]
	[InlineData("PresenceUnknown", PresenceStatus.Unknown)]
	public void Every_documented_availability_maps(string availability, PresenceStatus expected) {

		MsGraphPresenceService.MapAvailability(availability).Should().Be(expected);

	}

	[Theory]
	[InlineData("AVAILABLE")]
	[InlineData("available")]
	[InlineData("AvAiLaBlE")]
	public void Availability_matching_ignores_case(string availability) {

		// Graph documents PascalCase, but the value arrives as a free-form string.
		MsGraphPresenceService.MapAvailability(availability).Should().Be(PresenceStatus.Available);

	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("SomethingGraphAddedLater")]
	public void An_unrecognised_availability_maps_to_Unknown(string? availability) {

		// A value this mapping has never seen must not throw: Graph can add one at any time.
		MsGraphPresenceService.MapAvailability(availability).Should().Be(PresenceStatus.Unknown);

	}

	// ---------------------------------------------------------------------
	// Publishing
	// ---------------------------------------------------------------------

	[Fact]
	public async Task An_unauthenticated_user_is_not_asked_of_Graph() {

		var (service, state, provider) = CreateService(GraphPresence("Available"), authenticated: false);

		await service.UpdateUserPresence();

		provider.Calls.Should().Be(0);
		state.DidNotReceive().SetPresence(Arg.Any<UserPresence>());

	}

	[Fact]
	public async Task The_presence_Graph_reports_is_published() {

		var (service, state, _) = CreateService(GraphPresence("Busy", "InACall", "Back at 3"));

		await service.UpdateUserPresence();

		state.Received(1).SetPresence(Arg.Is<UserPresence>(p =>
			p.Status == PresenceStatus.Busy
			&& p.Activity == "In a call"
			&& p.Message == "Back at 3"));

	}

	[Fact]
	public async Task A_null_presence_publishes_Unknown_rather_than_failing() {

		// Graph answers with no presence for a user it cannot resolve.
		var (service, state, _) = CreateService(presence: null);

		await service.UpdateUserPresence();

		state.Received(1).SetPresence(Arg.Is<UserPresence>(p =>
			p.Status == PresenceStatus.Unknown && p.Activity == null && p.Message == null));

	}

	[Fact]
	public async Task A_presence_without_a_status_message_publishes_no_message() {

		var (service, state, _) = CreateService(GraphPresence("Available", "Available"));

		await service.UpdateUserPresence();

		state.Received(1).SetPresence(Arg.Is<UserPresence>(p => p.Message == null));

	}

	// ---------------------------------------------------------------------
	// Activity humanization
	// ---------------------------------------------------------------------

	[Theory]
	[InlineData("InAMeeting", "In a meeting")]
	[InlineData("Presenting", "Presenting")]
	[InlineData("OffWork", "Off work")]
	[InlineData("UrgentInterruptionsOnly", "Urgent interruptions only")]
	public async Task A_camel_cased_activity_is_split_into_words(string activity, string expected) {

		var (service, state, _) = CreateService(GraphPresence("Busy", activity));

		await service.UpdateUserPresence();

		state.Received(1).SetPresence(Arg.Is<UserPresence>(p => p.Activity == expected));

	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("   ")]
	public async Task A_blank_activity_passes_through_unchanged(string? activity) {

		var (service, state, _) = CreateService(GraphPresence("Available", activity));

		await service.UpdateUserPresence();

		state.Received(1).SetPresence(Arg.Is<UserPresence>(p => p.Activity == activity));

	}

	[Fact]
	public async Task The_service_reports_itself_enabled() {

		var (service, _, _) = CreateService(GraphPresence("Available"));

		// Presence capability is read from IsEnabled rather than from whether the service is
		// registered, so this value is load-bearing for callers.
		service.IsEnabled.Should().BeTrue();

	}

}
