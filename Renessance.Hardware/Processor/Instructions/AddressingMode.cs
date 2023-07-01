namespace Renessance.Hardware.Processor.Instructions;

public enum AddressingMode
{
  XZeroPageIndexed,
  YZeroPageIndexed,
  XAbsoluteIndexed,
  YAbsoluteIndexed,
  XIndirectIndexed,
  YIndirectIndexed,
  Implicit,
  Accumulator,
  Immediate,
  ZeroPage,
  Absolute,
  Relative,
  Indirect,
}