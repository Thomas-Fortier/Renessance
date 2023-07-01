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
}