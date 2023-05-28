namespace Renessance.Emulator.Hardware;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class InstructionAttributes : Attribute
{
  public byte Opcode { get; set; }
  public AddressingMode Mode { get; set; }
  public int Cycles { get; set; }
}