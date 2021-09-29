using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TariBirdManager.Enums;

namespace TariBirdManager.FileProcessors
{
  /// <summary>
  /// Reference processor.
  /// Ыets dependencies between files.
  /// </summary>
  class ReferenceProcessor : IReferenceProcessor
  {
    #region regex patterns

    private const string AnyParametersPattern = @"\([A-z^,^<^>^\s^\n^\r^.^,^:]*\)";
    private const string AnyDivider = @"[\s^\r^\n]*";
    #endregion

    #region private methods

    /// <summary>
    /// Locates which statements is used by iBatis XML specifications
    /// and sets statements links and files cross-references.
    /// </summary>
    private void SetStatementReferencesFromXmlFiles(
      FileInfo xmlFile,
      List<FileInfo> filesWithStatements)
    {
      if (xmlFile.Extension != FileTypes.Xml.ToString())
      {
        return;
      }

      foreach (var sourceFile in filesWithStatements)
      {
        // Process statements
      }
    }

    /// <summary>
    /// Checks usage one cs file in the other.
    /// </summary>
    private static void CheckCsUsage(FileInfo file, FileInfo fileWithStatements)
    {
      using (StreamReader reader = file.OpenText())
      {
        string fileContent = reader.ReadToEnd();

        string usagePattern =
          $"(({fileWithStatements.Name}" + AnyDivider + @"\()|" +
          $"(:" + AnyDivider + fileWithStatements.Name + @"))";

        var usageRegex =
          new Regex(usagePattern);

        Match match = usageRegex.Match(fileContent);

        if (match.Success)
        {
          // file.TryAddReference(fileWithStatements);
          // fileWithStatements.TryAddReference(file);
        }
      }
    }

    /// <summary>
    /// Locates usages of specifications within other specifications and sets cross-references.
    /// </summary>
    private void SetSpecificationReferencesFromOtherSpecifications(
      List<FileInfo> filesWithStatements)
    {
      foreach (var specification in filesWithStatements)
      {
        if (specification.Extension != FileTypes.Cs.ToString())
        {
          continue;
        }

        foreach (var fileWithStatements in filesWithStatements)
        {
          CheckCsUsage(specification, fileWithStatements);
        }
      }
    }

    /// <summary>
    /// Locates usages of specifications within other CS files and sets cross-references.
    /// </summary>
    private void SetSpecificationReferencesFromCsFiles(
      FileInfo file,
      List<FileInfo> filesWithStatements)
    {
      if (file.Extension != FileTypes.Cs.ToString())
      {
        return;
      }

      foreach (var fileWithStatements in filesWithStatements)
      {
        CheckCsUsage(file, fileWithStatements);
      }
    }

    /// <summary>
    /// Sets the references to specification from other CS files.
    /// </summary>
    private List<FileInfo> SetRelationships(
      List<FileInfo> files,
      List<FileInfo> filesWithStatements,
      FileTypes fileType)
    {
      SetSpecificationReferencesFromOtherSpecifications(filesWithStatements);

      foreach (var file in files
        .Except(filesWithStatements))
      {
        if (fileType == FileTypes.Cs)
        {
          SetSpecificationReferencesFromCsFiles(file, filesWithStatements);
        }
        else
        {
          SetStatementReferencesFromXmlFiles(file, filesWithStatements);
        }
      }

      return files;
    }

    #endregion

    #region public methods

    /// <summary>
    /// Sets the references within files.
    /// </summary>
    public List<FileInfo> SetReferences(
      List<FileInfo> csharpFiles,
      List<FileInfo> xmlFiles)
    {
      var filesWithStatements = csharpFiles
        //.Where(f => f.IsSpecification)
        .ToList();

      var processedCsFiles =
        SetRelationships(
          csharpFiles,
          filesWithStatements,
          FileTypes.Cs);

      var processedXmlFiles =
        SetRelationships(
          xmlFiles,
          filesWithStatements,
          FileTypes.Xml);

      return processedCsFiles
        .Union(processedXmlFiles)
        .ToList();
    }

    #endregion

  }
}
