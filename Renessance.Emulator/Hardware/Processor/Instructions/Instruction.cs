namespace Renessance.Emulator.Hardware.Processor.Instructions;

public class Instruction
{
  public InstructionAttributes Attributes { get; }

  private readonly Action _instructionLogic;

  public Instruction(InstructionAttributes attributes, Action instructionLogic)
  {
    Attributes = attributes;
    _instructionLogic = instructionLogic;
  }

  public void Execute()
  {
    _instructionLogic();
  }
}