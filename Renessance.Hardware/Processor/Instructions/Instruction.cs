namespace Renessance.Hardware.Processor.Instructions;

public class Instruction : IInstruction
{
  public string Opcode { get; private set; }
  public AddressingMode AddressingMode { get; private set; }
  
  public void Execute()
  {
    throw new NotImplementedException();
  }
}