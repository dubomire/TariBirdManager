using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using TariBirdManager.Enums;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// Directories processor.
  /// </summary>
  public class DirectoriesProcessor : IDirectoriesProcessor
  {
    #region private members

    private readonly IEnumerable<IFilesProcessor> filesProcessors;
    private readonly ILogger<DirectoriesProcessor> logger;
    private readonly IReferenceProcessor referenceProcessor;

    #endregion

    #region private methods

    private IFilesProcessor GetFilesProcessor(FileTypes fileType)
    {
      return filesProcessors
        .First(x => x.FileType == fileType);
    }

    private List<FileInfo> RetrieveDirectoryCsFiles(string path)
    {
      var processedFiles = new List<FileInfo>();

      var directoryInfo = new DirectoryInfo(path);

      processedFiles.AddRange(
        GetFilesProcessor(FileTypes.Cs)
          .Process(directoryInfo));

      foreach (var directory in directoryInfo.EnumerateDirectories())
      {
        processedFiles.AddRange(
          RetrieveDirectoryCsFiles(
            directory.FullName));
      }

      return processedFiles;
    }

    private List<FileInfo> RetrieveDirectoryXmlFiles(string path)
    {
      var processedFiles = new List<FileInfo>();

      var directoryInfo = new DirectoryInfo(path);

      processedFiles.AddRange(
        GetFilesProcessor(FileTypes.Xml)
          .Process(directoryInfo));

      return processedFiles;
    }

    #endregion

    #region public methods

    /// <summary>
    /// Creates new instance of <see cref="DirectoriesProcessor"/>.
    /// </summary>
    public DirectoriesProcessor(
      IEnumerable<IFilesProcessor> filesProcessors,
      IReferenceProcessor referenceProcessor,
      ILogger<DirectoriesProcessor> logger)
    {
      this.filesProcessors = filesProcessors;
      this.logger = logger;
      this.referenceProcessor = referenceProcessor;
    }

    /// <summary>
    /// Process directory by specified path.
    /// </summary>
    public List<FileInfo> ProcessDirectory(string path)
    {
      logger.LogInformation($"Processing '{path}' directory.");

      logger.LogInformation("Start CS files processing.");
      List<FileInfo> sourceFiles = RetrieveDirectoryCsFiles(path);

      logger.LogInformation("Start XML files processing.");
      List<FileInfo> xmlFiles = RetrieveDirectoryXmlFiles(path);

      return referenceProcessor.SetReferences(sourceFiles, xmlFiles);
    }

    #endregion
  }
}
