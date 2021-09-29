using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TariBirdManager.Extensions
{
  /// <summary>
  /// Extension methods for <see cref="IServiceCollection"/>.
  /// </summary>
  public static class ServiceCollectionExtensions
  {
    #region private methods
    /// <summary>
    /// Configures configuration.
    /// </summary>
    private static IServiceCollection ConfigureConfiguration(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      services.AddSingleton(configuration);

      return services;
    }

    #endregion

    #region public methods
    /// <summary>
    /// Adds services to the container.
    /// </summary>
    public static IServiceCollection ConfigureServices(
      this IServiceCollection services)
    {
      IConfiguration configuration =
        new ConfigurationBuilder()
          .Configure()
          .Build();

      Enum.TryParse(configuration["Logging.Console.LogLevel.Default"], out LogLevel level);

      return services
        .ConfigureConfiguration(configuration)
        .AddLogging(
          b =>
          {
            b.AddConsole();
            b.SetMinimumLevel(level);
          });
        // .AddSingleton<IAnalyzer, Analyzer.Analyzer>()
        // .AddTransient<IReferenceProcessor, ReferenceProcessor>()
        // .AddTransient<IReporter, Reporter.Reporter>()
        // .AddTransient<IDirectoriesProcessor, DirectoriesProcessor>()
        // .AddTransient<IFilesProcessor, CsFilesProcessor>()
        // .AddTransient<IFilesProcessor, XmlFilesProcessor>();
    }
    #endregion
  }
}
