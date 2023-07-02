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
}