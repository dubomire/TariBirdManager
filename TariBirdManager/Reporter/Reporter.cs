using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TariBirdManager.Reporter
{
  /// <summary>
  /// Class to make reports.
  /// </summary>
  public class Reporter : IReporter
  {
    #region private members

    private readonly string underscoreDivider = $"\n{new string('_', 10)}\n";
    private readonly string asteriskDivider = $"\n{new string('*', 10)}\n";

    private readonly IConfiguration configuration;
    private readonly ILogger<Reporter> logger;

    private string ReportFileName => configuration[Constants.ReportFileNameKey];
    private string ReportLocation => configuration[Constants.ReportFolderKey];
    private string ReportFullFileName => ReportLocation + "/" + ReportFileName;

    #endregion

    #region private methods
    private void RemoveExistedReport()
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(ReportLocation);

      if (!directoryInfo.Exists)
      {
        directoryInfo.Create();
        return;
      }

      FileInfo fileInfo = new FileInfo(ReportFullFileName);

      if (fileInfo.Exists)
      {
        fileInfo.Delete();
      }
    }

    private void WriteTextToFile(string content)
    {
      using (FileStream fstream = new FileStream(ReportFullFileName, FileMode.OpenOrCreate))
      {
        fstream.Seek(0, SeekOrigin.End);
        byte[] array =
          System.Text.Encoding.Default
            .GetBytes(content);

        fstream.Write(array, 0, array.Length);
      }
    }

    private void WriteHeader(string header)
    {
      string divider = $"\n{new string('*', header.Length + 4)}\n";
      string headerString =
        divider + $"* {header} *" + divider;
      WriteTextToFile(headerString);
    }

    private void WriteIssueTitle(int issueNumber)
    {
      string divider = $"{new string('_', 3)}";
      string headerString =
        $"\n{divider} Issue {issueNumber} {divider}\n";
      WriteTextToFile(headerString);
    }

    private void FinalizeReport()
    {
      WriteTextToFile("- END OF REPORT -");
    }

    #endregion

    #region public methods

    /// <summary>
    /// Creates instance of <see cref="Reporter"/>.
    /// </summary>
    public Reporter(
      IConfiguration configuration,
      ILogger<Reporter> logger)
    {
      this.configuration = configuration;
      this.logger = logger;
    }

    /// <summary>
    /// Prepares the report and saves the result to file.
    /// </summary>
    public void Report(List<FileInfo> files)
    {
      RemoveExistedReport();
      FinalizeReport();
    }

    #endregion
  }
}
