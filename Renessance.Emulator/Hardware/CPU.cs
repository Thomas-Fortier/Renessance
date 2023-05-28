using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Renessance.Emulator.UnitTests")]
namespace Renessance.Emulator.Hardware;

public sealed partial class CPU : ICPU
{
  internal ushort ProgramCounter { get; private set; }
  internal byte Accumulator { get; private set; }
  internal byte XRegister { get; private set; }
  internal byte YRegister { get; private set; }
  internal byte StackPointer { get; private set; }
  internal StatusRegister Status { get; private set; }
  
  private ushort _currentAbsoluteAddress;
  private ushort _currentRelativeAddress;
  private byte _fetchedData;
  private int _cycles;
  private Dictionary<byte, Instruction> _instructions;

  private readonly IRAM _ram;

  public CPU(IRAM ram)
  {
    _ram = ram;
    Status = new StatusRegister(); // TODO: Dependency injection?
    _instructions = InitializeInstructions();
    
    Reset();
  }
  
  public void Clock()
  {
    if (_cycles == 0)
    {
      var opcode = Read(ProgramCounter);

      Status.Unused = true;
      ProgramCounter++;

      var instruction = _instructions[opcode];
      _cycles = instruction.Cycles;
      _currentAbsoluteAddress = GetAddressFromAddressingMode(instruction.Mode);
      
      instruction.Execute();
    }
    
    _cycles--;
  }

  public void Reset()
  {
    _currentAbsoluteAddress = 0xFFFC;
    var lowByte = _ram.Read((ushort)(_currentAbsoluteAddress + 0));
    var highByte = _ram.Read((ushort)(_currentAbsoluteAddress + 1));
    ProgramCounter = (ushort)((highByte << 8) | lowByte);

    // Clear registers
    Accumulator  = 0;
    XRegister    = 0;
    YRegister    = 0;
    StackPointer = 0;
    Status.Reset();
    
    // Clear helpers
    _currentAbsoluteAddress = 0x0000;
    _currentRelativeAddress = 0x0000;
    _fetchedData            = 0x00;

    // Reset takes 8 cycles
    _cycles = 8;
  }

  public void InterruptRequest()
  {
    throw new NotImplementedException();
  }

  public void NonMaskableInterruptRequest()
  {
    throw new NotImplementedException();
  }

  private byte FetchByte()
  {
    // TODO: If current opcode is not IMP, then return data from absolute address...
    return _fetchedData = Read(_currentAbsoluteAddress);
  }

  private byte Read(ushort address) => _ram.Read(address);

  private void Write(ushort address, byte data) => _ram.Write(address, data);
}