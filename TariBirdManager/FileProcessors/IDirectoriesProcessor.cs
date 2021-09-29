using System.Collections.Generic;
using System.IO;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// Common interface for processors.
  /// </summary>
  public interface IDirectoriesProcessor
  {
    /// <summary>
    /// Process file or directory.
    /// </summary>
    public List<FileInfo> ProcessDirectory(string path);
  }
}
