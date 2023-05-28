namespace Renessance.Emulator.Hardware;

public class Instruction
{
  public byte Opcode { get; }
  public AddressingMode Mode { get; }
  public int Cycles { get; }
  public string Name { get; }

  private readonly Action _operation;

  public Instruction(string name, byte opcode, AddressingMode mode, int cycles, Action operation)
  {
    Name = name;
    Opcode = opcode;
    Mode = mode;
    Cycles = cycles;
    _operation = operation;
  }

  public void Execute()
  {
    _operation();
  }
}