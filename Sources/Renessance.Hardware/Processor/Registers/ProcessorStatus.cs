using log4net;

namespace Renessance.Hardware.Processor.Registers;

internal class ProcessorStatus
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(ProcessorStatus));
  private readonly List<ProcessorFlag> _activeFlags;

  public ProcessorStatus()
  {
    _activeFlags = new List<ProcessorFlag>();
  }

  public void StoreByteAsFlags(byte flags)
  {
#if DEBUG
    _log.Debug($"Storing bytes {flags} as flags.");
#endif
    
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

  public int GetFlagValue(ProcessorFlag flag)
  {
    return IsFlagActive(flag) ? 1 : 0;
  }

  public bool IsFlagActive(ProcessorFlag flag)
  {
    return _activeFlags.Exists(activeFlag => activeFlag == flag);
  }

  public void RemoveAllFlags()
  {
#if DEBUG
    _log.Debug($"Clearing all flags.");
#endif
    _activeFlags.Clear();
  }

  public void RemoveFlag(ProcessorFlag flag)
  {
#if DEBUG
    _log.Debug($"Removing flag {flag}.");
#endif
    _activeFlags.Remove(flag);
  }

  public void SetFlag(ProcessorFlag flag)
  {
    if (!_activeFlags.Exists(activeFlag => activeFlag == flag))
    {
#if DEBUG
      _log.Debug($"Setting flag {flag}.");
#endif
      _activeFlags.Add(flag);

      return;
    }
    
#if DEBUG
    _log.Debug($"Flag {flag} is already active. Not setting.");
#endif
  }

  public void SetFlag(ProcessorFlag flag, bool condition)
  {
#if DEBUG
    _log.Debug($"Condition for setting the flag {flag} is ${condition}.");
#endif
    
    if (condition)
    {
      SetFlag(flag);
    }
  }

  private static ProcessorFlag ConvertBitPositionToProcessorFlag(int bitPosition)
  {
    return bitPosition switch
    {
      0 => ProcessorFlag.Carry,
      1 => ProcessorFlag.Zero,
      2 => ProcessorFlag.InterruptDisable,
      3 => ProcessorFlag.DecimalMode,
      4 => ProcessorFlag.BreakCommand,
      5 => ProcessorFlag.Overflow,
      7 => ProcessorFlag.Negative,
      _ => throw new NotSupportedException(
        "The specified bit position is not valid. Must be between 0 and 7 inclusive.")
    };
  }
}