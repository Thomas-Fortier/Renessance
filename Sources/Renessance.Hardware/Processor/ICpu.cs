namespace Renessance.Hardware.Processor;

/// <summary>
/// Represents the 6502 CPU.
/// </summary>
public interface ICpu
{
  /// <summary>
  /// Executes one CPU cycle.
  /// </summary>
  void ExecuteCycle();

  /// <summary>
  /// Reads the data from the specified location in memory.
  /// </summary>
  /// <param name="location">The location to read from in memory.</param>
  /// <returns>The data read from the location in memory.</returns>
  ushort Read(ushort location);

  /// <summary>
  /// Writes the specified data to the specified location in memory.
  /// </summary>
  /// <param name="location">The location to write to in memory.</param>
  /// <param name="data">The data to write.</param>
  void Write(ushort location, byte data);

  /// <summary>
  /// Resets the CPU to its initial state.
  /// </summary>
  void Reset();

  /// <summary>
  /// Loads the specified ROM data into memory.
  /// </summary>
  /// <param name="data">The ROM data.</param>
  void LoadRom(byte[] data);
}