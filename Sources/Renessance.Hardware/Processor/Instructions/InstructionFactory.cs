namespace Renessance.Hardware.Processor.Instructions;

public class InstructionFactory
{
  private Cpu _cpu;
  private AddressingMode _addressingMode;

  public InstructionFactory(Cpu cpu, AddressingMode addressingMode)
  {
    _cpu = cpu;
    _addressingMode = addressingMode;
  }

  private Instruction And()
  {
    return new Instruction()
    {
      
    };
  }
}