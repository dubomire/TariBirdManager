using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TariBirdManager.Analyzer;

namespace TariBirdManager.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="IServiceProvider"/>.
	/// </summary>
	public static class ServiceProviderExtensions
	{
		#region public methods
		/// <summary>
		/// Host initialization and startup.
		/// </summary>
		public static IServiceProvider StartAnalyzer(
			this IServiceProvider provider,
			string targetPath)
		{
			provider
				.GetService<IAnalyzer>()
				?.Start(targetPath);

			return provider;
		}

		/// <summary>
		/// Waiting for stop signal to terminate app.
		/// </summary>
		public static IServiceProvider WaitForStopSignal(this IServiceProvider provider)
		{
			var logger = provider.GetService<ILogger<Analyzer.Analyzer>>();

			logger.LogInformation("Waiting for stop signal");

			Console.ReadKey();

			logger.LogInformation("Terminating {Name} app", nameof(TariBirdManager));

			return provider;
		}
		#endregion
	}
}