using Renessance.Emulator.Hardware.Processor.Instructions;

namespace Renessance.Emulator.Hardware.Processor;

public sealed partial class CPU
{
  private ushort ExecuteAddressingMode(AddressingMode mode)
  {
    switch (mode)
    {
      case AddressingMode.Direct:
        throw new NotImplementedException();
      case AddressingMode.Immediate:
        return _programCounter++;
      case AddressingMode.ZeroPage:
        return ReadByte(_programCounter++);
      case AddressingMode.Absolute:
        throw new NotImplementedException();
      case AddressingMode.ZeroPageX:
        ushort zeroPage = ReadByte(_programCounter++);
        zeroPage += _xRegister;
        _currentCycle--;
        return zeroPage;
      case AddressingMode.ZeroPageY:
        throw new NotImplementedException();
      case AddressingMode.AbsoluteX:
        throw new NotImplementedException();
      case AddressingMode.AbsoluteY:
        throw new NotImplementedException();
      case AddressingMode.IndirectX:
        throw new NotImplementedException();
      case AddressingMode.IndirectY:
        throw new NotImplementedException();
      default:
        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
    }
  }
}