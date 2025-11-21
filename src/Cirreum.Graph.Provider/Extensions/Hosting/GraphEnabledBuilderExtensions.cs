namespace Cirreum;

using Cirreum.Graph.Provider;
using Cirreum.Presence;

public static class GraphEnabledBuilderExtensions {

	/// <summary>
	/// Adds Microsoft Graph-based user presence service.
	/// </summary>
	/// <param name="builder">The <see cref="IGraphEnabledBuilder"/> to configure services for.</param>
	/// <param name="refreshInterval">The interval in milliseconds between presence updates. This determines how frequently
	/// the system will poll Microsoft Graph for updated presence information.</param>
	/// <returns>The provided <see cref="IGraphEnabledBuilder"/> for method chaining.</returns>
	/// <remarks>
	/// This method configures the following services:
	/// <list type="bullet">
	///   <item><description>Registers <see cref="MsGraphPresenceService"/> as the implementation for <see cref="IUserPresenceService"/></description></item>
	///   <item><description>Configures <see cref="UserPresenceMonitorOptions"/> with the specified refresh interval</description></item>
	/// </list>
	/// Use this method in your Blazor WebAssembly Program.cs file during service configuration.
	/// </remarks>
	public static IGraphEnabledBuilder WithGraphUserPresence(this IGraphEnabledBuilder builder, int refreshInterval) {
		builder.Presence.AddPresenceService<MsGraphPresenceService>(refreshInterval);
		return builder;
	}

}