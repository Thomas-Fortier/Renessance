namespace Renessance.Hardware.Processor;

public class Cpu : ICpu
{
  public CpuRegisters Registers { get; private set; }

  public Cpu()
  {
    Registers = new CpuRegisters();
  }
  
  public void ExecuteCycle()
  {
    throw new NotImplementedException();
  }

  public ushort Read(ushort location)
  {
    throw new NotImplementedException();
  }
}