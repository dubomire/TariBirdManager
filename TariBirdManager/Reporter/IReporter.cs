using System.Collections.Generic;
using System.IO;

namespace TariBirdManager.Reporter
{
  /// <summary>
  /// Interface for reporter instance.
  /// </summary>
  public interface IReporter
  {
    /// <summary>
    /// Prepares the report and saves the result to file.
    /// </summary>
    void Report(List<FileInfo> files);
  }
}
