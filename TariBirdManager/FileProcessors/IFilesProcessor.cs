using System.Collections.Generic;
using System.IO;
using TariBirdManager.Enums;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// Interface for files processor.
  /// </summary>
  public interface IFilesProcessor
  {
    /// <summary>
    /// File type.
    /// </summary>
    public FileTypes FileType { get; }

    /// <summary>
    /// Process file or directory.
    /// </summary>
    public List<FileInfo> Process(DirectoryInfo directoryInfo);
  }
}
