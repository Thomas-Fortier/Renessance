namespace Renessance.Hardware.Processor;

public class Cpu : ICpu
{
  private CpuRegisters _registers;

  public Cpu()
  {
    _registers = new CpuRegisters();
  }
  
  public void ExecuteCycle()
  {
    throw new NotImplementedException();
  }
}