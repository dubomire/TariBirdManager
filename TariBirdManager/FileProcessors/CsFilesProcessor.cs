using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using TariBirdManager.Enums;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// CS source files processor.
  /// </summary>
  public class CsFilesProcessor : IFilesProcessor
  {
    #region regex patterns

    #region consts

    private const string AnyParametersPattern = @"\([A-z^,^<^>^\s^\n^\r^.^,^:]*\)";

    #endregion

    #endregion

    #region private members

    private readonly ILogger<CsFilesProcessor> logger;

    #endregion

    #region public members

    /// <summary>
    /// Type of file.
    /// </summary>
    public FileTypes FileType => FileTypes.Cs;

    #endregion

    #region private methods

    private List<FileInfo> GetDirectoryFiles(DirectoryInfo directoryInfo)
    {
      return directoryInfo
        .GetFiles($"*.{FileTypes.Cs}", SearchOption.TopDirectoryOnly)
        .ToList();
    }
    #endregion

    #region public methods

    /// <summary>
    /// Creates new instance of <see cref="CsFilesProcessor"/>.
    /// </summary>
    public CsFilesProcessor(
      ILogger<CsFilesProcessor> logger)
    {
      this.logger = logger;
    }

    /// <summary>
    /// Process files by specified path.
    /// </summary>
    public List<FileInfo> Process(DirectoryInfo directoryInfo)
    {
      List<FileInfo> processedCsFiles =
        GetDirectoryFiles(directoryInfo);

      return processedCsFiles
        .ToList();
    }

    #endregion
  }
}
