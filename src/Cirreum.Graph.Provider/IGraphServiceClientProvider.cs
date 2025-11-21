namespace Cirreum.Graph.Provider;

using Microsoft.Graph;

/// <summary>
/// Provides access to Microsoft Graph API functionality through a properly authenticated
/// <see cref="GraphServiceClient"/> instance while respecting service lifetimes.
/// </summary>
/// <remarks>
/// This provider handles the complexities of authenticating with Microsoft Graph,
/// including token acquisition and scope management. It creates properly scoped
/// instances of the required authentication services for each request.
/// </remarks>
public interface IGraphServiceClientProvider {
	/// <summary>
	/// Executes an operation using a <see cref="GraphServiceClient"/> that returns a value.
	/// </summary>
	/// <typeparam name="T">The type of value returned by the operation.</typeparam>
	/// <param name="action">A function that defines the operation to perform with the client.</param>
	/// <returns>
	/// A task representing the asynchronous operation that, when completed, 
	/// contains the result of the operation performed with the Graph client.
	/// </returns>
	/// <remarks>
	/// This method creates a properly authenticated Graph client for the duration of the operation,
	/// ensuring all necessary authentication tokens are acquired within the correct scope.
	/// </remarks>
	Task<T> UseClientAsync<T>(Func<GraphServiceClient, Task<T>> action);

	/// <summary>
	/// Executes an operation using a <see cref="GraphServiceClient"/> that does not return a value.
	/// </summary>
	/// <param name="action">A function that defines the operation to perform with the client.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <remarks>
	/// This method creates a properly authenticated Graph client for the duration of the operation,
	/// ensuring all necessary authentication tokens are acquired within the correct scope.
	/// </remarks>
	Task UseClientAsync(Func<GraphServiceClient, Task> action);
}