using Renessance.Emulator.Hardware.Processor.Instructions;

namespace Renessance.Emulator.Hardware.Processor;

public sealed partial class CPU
{
  // Registers
  private byte _accumulator;
  private byte _xRegister;
  private byte _yRegister;
  private ushort _stackPointer;
  private ushort _programCounter;
  private StatusRegister _status;
  
  private Dictionary<byte, Instruction> _instructions;
  private int _currentCycle;
  private ushort _currentMemoryAddress;
  
  private readonly RAM _memory;

  public CPU(RAM memory)
  {
    _memory = memory;
    _status = new StatusRegister();
    _instructions = InitializeInstructions();
    
    Reset();

    // TODO: Remove
    {
      _memory.Write(0xFFFC, 0xB5);
      _memory.Write(0xFFFD, 0x10);
      _memory.Write(0x10, 0xFF);
    }
  }

  private void Reset()
  {
    _accumulator    = 0x0;
    _xRegister      = 0x0;
    _yRegister      = 0x0;
    _stackPointer   = 0x0100;
    _programCounter = 0xFFFC;
    
    _status.Reset();
    _memory.Reset();
  }

  public void ExecuteNextInstruction(int cycles)
  {
    _currentCycle = cycles;
    
    while (_currentCycle > 0)
    {
      var instruction = ReadNextInstruction();
      _currentCycle = instruction.Attributes.Cycles - 1;
      _currentMemoryAddress = ExecuteAddressingMode(instruction.Attributes.Mode);
      instruction.Execute();
    }
  }

  private byte ReadByte(ushort address)
  {
    var data = _memory.Read(address);
    _currentCycle--;

    return data;
  }

  private byte ReadNextByte() => ReadByte(_programCounter++);

  private Instruction ReadNextInstruction()
  {
    var opcode = ReadNextByte();
    
    try
    {
      return _instructions[opcode];
    }
    catch (KeyNotFoundException e)
    {
      throw new Exception($"Instruction with opcode '{opcode}' does not exist.");
    }
  }
}