namespace Renessance.Emulator.Hardware.Processor.Instructions;

public enum AddressingMode
{
  Direct,
  Immediate,
  ZeroPage,
  Absolute,
  ZeroPageX,
  ZeroPageY,
  AbsoluteX,
  AbsoluteY,
  IndirectX,
  IndirectY
}