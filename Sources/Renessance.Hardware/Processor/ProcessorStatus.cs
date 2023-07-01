namespace Renessance.Hardware.Processor;

public class ProcessorStatus : IProcessorStatus
{
  private readonly List<ProcessorFlag> _activeFlags;

  public ProcessorStatus()
  {
    _activeFlags = new List<ProcessorFlag>();
  }

  public void StoreByteAsFlags(byte flags)
  {
    for (var index = 0; index < byte.MaxValue; index++)
    {
      var isBitSet = (flags & (1 << index)) != 0;

      if (isBitSet)
        _activeFlags.Add(ConvertBitPositionToProcessorFlag(index));
    }
  }

  public IReadOnlyCollection<ProcessorFlag> GetActiveFlags()
  {
    return _activeFlags;
  }

  public bool IsFlagActive(ProcessorFlag flag)
  {
    return _activeFlags.Exists(activeFlag => activeFlag == flag);
  }

  public void RemoveAllFlags()
  {
    _activeFlags.Clear();
  }

  public void RemoveFlag(ProcessorFlag flag)
  {
    _activeFlags.Remove(flag);
  }

  private ProcessorFlag ConvertBitPositionToProcessorFlag(int bitPosition)
  {
    return bitPosition switch
    {
      0 => ProcessorFlag.Carry,
      1 => ProcessorFlag.Zero,
      2 => ProcessorFlag.InterruptDisable,
      3 => ProcessorFlag.DecimalMode,
      4 => ProcessorFlag.Unused,
      5 => ProcessorFlag.Unused,
      7 => ProcessorFlag.Overflow,
      8 => ProcessorFlag.Negative,
      _ => throw new NotSupportedException(
        "The specified bit position is not valid. Must be between 0 and 7 inclusive.")
    };
  }
}