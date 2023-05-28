namespace Renessance.Emulator.Hardware.Processor.Instructions;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class InstructionAttributes : Attribute
{
  public int Cycles { get; set; }
  public AddressingMode Mode { get; set; }
  public byte Opcode { get; set; }
}