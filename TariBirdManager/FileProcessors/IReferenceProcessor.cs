using System.Collections.Generic;
using System.IO;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// Interface for reference processor.
  /// </summary>
  public interface IReferenceProcessor
  {
    /// <summary>
    /// Sets the references within files
    /// </summary>
    List<FileInfo> SetReferences(
      List<FileInfo> csharpFiles,
      List<FileInfo> xmlFiles);
  }
}
