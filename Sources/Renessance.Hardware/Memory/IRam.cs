namespace Renessance.Hardware.Memory;

public interface IRam
{
  /// <summary>
  /// Fetches the byte at the specified memory address.
  /// </summary>
  /// <param name="location">The location of the memory.</param>
  /// <returns>The data.</returns>
  byte Read(int location);

  /// <summary>
  /// Writes the specified data at the specified location in memory.
  /// </summary>
  /// <param name="location">The location of the memory.</param>
  /// <param name="data">The data to set.</param>
  void Write(int location, byte data);

  /// <summary>
  /// Writes the specified ROM data.
  /// </summary>
  /// <param name="data">The data to set.</param>
  void LoadRom(byte[] data);
}