namespace Renessance.Hardware.Processor.Instructions;

public class AddressingMode
{
  public string Name { get; private set; }
  public ushort OperandAddress { get; set; }
  public bool RequiresExtraCycle { get; set; }

  public AddressingMode(string name, ushort operandAddress, bool requiresExtraCycle = false)
  {
    Name = name;
    OperandAddress = operandAddress;
    RequiresExtraCycle = requiresExtraCycle;
  }
}