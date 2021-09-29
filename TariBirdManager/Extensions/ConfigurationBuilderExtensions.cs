using System;
using Microsoft.Extensions.Configuration;

namespace TariBirdManager.Extensions
{
	/// <summary>
	/// Extension methods for <see cref="ConfigurationBuilder"/>.
	/// </summary>
	public static class ConfigurationBuilderExtensions
	{
		#region private methods

		private static IConfigurationBuilder ConfigureBuilder(
			this IConfigurationBuilder configurationBuilder,
			string location)
		{
			configurationBuilder.SetBasePath(location);

			configurationBuilder.AddJsonFile(
				path: "appsettings.json",
				optional: true,
				reloadOnChange: true);

			return configurationBuilder;
		}

		#endregion

		#region public methods
		/// <summary>
		/// Configures configuration builder.
		/// </summary>
		public static IConfigurationBuilder Configure(
			this ConfigurationBuilder configurationBuilder)
		{
			return configurationBuilder.ConfigureBuilder(
				AppDomain.CurrentDomain.BaseDirectory);
		}
		#endregion
	}
}