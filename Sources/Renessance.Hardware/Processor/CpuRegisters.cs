namespace Renessance.Hardware.Processor;

public class CpuRegisters
{
  public IProcessorStatus ProcessorStatus { get; set; }
  public ushort ProgramCounter { get; set; }
  public byte StackPointer { get; set; }
  public byte Accumulator { get; set; }
  public byte XIndexRegister { get; set; }
  public byte YIndexRegister { get; set; }

  public CpuRegisters()
  {
    ProcessorStatus = new ProcessorStatus();
  }
}