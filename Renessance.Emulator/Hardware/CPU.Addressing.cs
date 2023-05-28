namespace Renessance.Emulator.Hardware;

public sealed partial class CPU
{
  private ushort GetAddressFromAddressingMode(AddressingMode mode)
  {
    return mode switch
    {
      AddressingMode.Direct => throw new NotImplementedException(),
      AddressingMode.Immediate => ProgramCounter,
      AddressingMode.ZeroPage => Read(ProgramCounter),
      AddressingMode.Absolute => GetAbsoluteAddress(),
      AddressingMode.ZeroPageX => Read((ushort)(ProgramCounter + XRegister)),
      AddressingMode.ZeroPageY => Read((ushort)(ProgramCounter + YRegister)),
      AddressingMode.AbsoluteX => GetAbsoluteAddress(XRegister),
      AddressingMode.AbsoluteY => GetAbsoluteAddress(YRegister),
      AddressingMode.IndirectX => throw new NotImplementedException(),
      AddressingMode.IndirectY => throw new NotImplementedException(),
      _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
    };
  }

  private ushort GetAbsoluteAddress(byte offset = 0)
  {
    ushort lowByte = Read(ProgramCounter);
    ProgramCounter++;
    ushort highByte = Read(ProgramCounter);
    ProgramCounter++;

    var address = (ushort)((highByte << 8) | lowByte);
    address += offset;

    return address;
  }
}