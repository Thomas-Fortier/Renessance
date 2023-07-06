namespace Renessance.Hardware;

public interface INesSystem
{
  /// <summary>
  /// Loads the specified ROM data into memory.
  /// </summary>
  /// <param name="data">The ROM data.</param>
  void LoadRom(byte[] data);

  /// <summary>
  /// Executes one clock cycle.
  /// </summary>
  void ExecuteCycle();
}