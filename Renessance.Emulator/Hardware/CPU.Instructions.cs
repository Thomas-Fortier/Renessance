using System.Reflection;

namespace Renessance.Emulator.Hardware;

public sealed partial class CPU
{
  private Dictionary<byte, Instruction> InitializeInstructions()
  {
    var instructions = new Dictionary<byte, Instruction>();
    
    var methods = typeof(CPU).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
      .Where(t => t.GetCustomAttributes(typeof(InstructionAttributes), false).Length > 0).ToArray();

    foreach (var method in methods)
    {
      foreach (var attributes in method.GetCustomAttributes())
      {
        var instructionAttributes = (InstructionAttributes)attributes;
        var key = instructionAttributes.Opcode;
        var instructionLogic = (Action) method.CreateDelegate(typeof(Action), this);
        var value = new Instruction(
          method.Name, 
          instructionAttributes.Opcode, 
          instructionAttributes.Mode, 
          instructionAttributes.Cycles, 
          instructionLogic
        );
        
        instructions.Add(key, value);
      }
    }

    return instructions;
  }
  
  [InstructionAttributes(Cycles = 2, Mode = AddressingMode.Immediate, Opcode = 0x29)]
  private void AND()
  {
    Accumulator &= Read(_currentAbsoluteAddress);

    Status.Zero = Accumulator == 0x0;
    Status.Negative = (Accumulator & (1 << 7)) != 0;
  }

  [InstructionAttributes(Cycles = 2, Mode = AddressingMode.Immediate, Opcode = 0xA9)]
  [InstructionAttributes(Cycles = 3, Mode = AddressingMode.ZeroPage, Opcode = 0xA5)]
  [InstructionAttributes(Cycles = 4, Mode = AddressingMode.ZeroPageX, Opcode = 0xB5)]
  private void LDA()
  {
    Accumulator = Read(_currentAbsoluteAddress);

    Status.Zero = Accumulator == 0x0;
    Status.Negative = (Accumulator & (1 << 7)) != 0;
  }

  [InstructionAttributes(Cycles = 2, Mode = AddressingMode.Immediate, Opcode = 0xA2)]
  private void LDX()
  {
    XRegister = Read(_currentAbsoluteAddress);
    
    Status.Zero = Accumulator == 0x0;
    Status.Negative = (Accumulator & (1 << 7)) != 0;
  }

  [InstructionAttributes(Cycles = 3, Mode = AddressingMode.Absolute, Opcode = 0x4C)]
  private void JMP()
  {
    
  }
}