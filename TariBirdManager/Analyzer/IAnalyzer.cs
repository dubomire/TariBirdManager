namespace TariBirdManager.Analyzer
{
  /// <summary>
  /// Interface for Analyzer.
  /// </summary>
  public interface IAnalyzer
  {
    /// <summary>
    /// Invokes analyzer to perform analysis.
    /// </summary>
    public void Start(string targetPath);
  }
}
