using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TariBirdManager.FileProcessors;
using TariBirdManager.Reporter;

namespace TariBirdManager.Analyzer
{
  /// <summary>
  /// Main class to perform specification analysis.
  /// </summary>
  public class Analyzer : IAnalyzer
  {
    #region private members

    private readonly ILogger<Analyzer> logger;
    private readonly IDirectoriesProcessor directoriesProcessor;
    private readonly IReporter reporter;

    #endregion

    #region public methods

    /// <summary>
    /// Creates new instance of <see cref="Analyzer"/>.
    /// </summary>
    public Analyzer(
      ILogger<Analyzer> logger,
      IDirectoriesProcessor directoriesProcessor,
      IReporter reporter)
    {
      this.logger = logger;
      this.directoriesProcessor = directoriesProcessor;
      this.reporter = reporter;
    }

    /// <summary>
    /// Invokes analyzer to perform analysis.
    /// </summary>
    public void Start(string targetPath)
    {
      Console.WriteLine("Analyzer started.");
      var files = directoriesProcessor.ProcessDirectory(targetPath);
      logger.LogInformation("Preparing report.");
      reporter.Report(files);
    }

    #endregion
  }
}
