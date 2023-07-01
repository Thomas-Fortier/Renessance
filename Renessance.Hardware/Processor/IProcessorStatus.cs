namespace Renessance.Hardware.Processor;

/// <summary>
/// Represents the status flags of the CPU.
/// </summary>
public interface IProcessorStatus
{
  /// <summary>
  /// Takes in a byte representing the status flags to set and stores the
  /// appropriate <see cref="ProcessorFlag"/>s.
  /// </summary>
  /// <param name="flags">The 8-bit representation of the flags.</param>
  void StoreByteAsFlags(byte flags);

  /// <summary>
  /// Gets a list of the currently set flags.
  /// </summary>
  /// <returns>A collection of <see cref="ProcessorFlag"/>s.</returns>
  IReadOnlyCollection<ProcessorFlag> GetActiveFlags();

  /// <summary>
  /// Returns true if the specified flag is currently active.
  /// </summary>
  /// <param name="flag">The <see cref="ProcessorFlag"/> to check.</param>
  /// <returns>True if the specified flag is currently active and false otherwise.</returns>
  bool IsFlagActive(ProcessorFlag flag);

  /// <summary>
  /// Removes all flags from the active flags.
  /// </summary>
  void RemoveAllFlags();

  /// <summary>
  /// Removes the specified flag from the active flags.
  /// </summary>
  /// <param name="flag">The <see cref="ProcessorFlag"/> to remove.</param>
  void RemoveFlag(ProcessorFlag flag);
}