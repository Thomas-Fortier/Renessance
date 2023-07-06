using log4net;

namespace Renessance.Hardware.Processor.Registers;

internal class CpuRegisters
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(CpuRegisters));
  
  public ProcessorStatus ProcessorStatus { get; set; }
  public ushort ProgramCounter { get; set; }
  public byte StackPointer { get; set; }
  public byte Accumulator { get; set; }
  public byte XIndexRegister { get; set; }
  public byte YIndexRegister { get; set; }

  public CpuRegisters()
  {
    ProcessorStatus = new ProcessorStatus();
  }

  public void Reset(ICpu cpu)
  {
    Accumulator = 0;
    XIndexRegister = 0;
    YIndexRegister = 0;
    StackPointer = 0xFD;
    ProgramCounter = GetInitialProgramCounter(cpu);
    ProcessorStatus.RemoveAllFlags();
  }

  private static ushort GetInitialProgramCounter(ICpu cpu)
  {
    const ushort ABSOLUTE_ADDRESS = 0xFFFC;
    var lo = cpu.Read(ABSOLUTE_ADDRESS);
    var hi = cpu.Read(ABSOLUTE_ADDRESS + 1);

    var programCounter = (ushort)((hi << 8) | lo);

#if DEBUG
    _log.Debug($"Initial program counter state was evaluated to {programCounter:X}.");
#endif

    return programCounter;
  }
}