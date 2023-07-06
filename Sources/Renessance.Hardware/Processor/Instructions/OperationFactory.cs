using Renessance.Hardware.Processor.Registers;

namespace Renessance.Hardware.Processor.Instructions;

internal class OperationFactory
{
  private readonly Cpu _cpu;
  private readonly AddressingModeFactory _addressingModeFactory;

  public OperationFactory(Cpu cpu, AddressingModeFactory addressingModeFactory)
  {
    _cpu = cpu;
    _addressingModeFactory = addressingModeFactory;
  }

  public Operation GetOperationFromOpcode(ushort opcode)
  {
    var addressingMode = _addressingModeFactory.GetAddressingModeFromOpcode(opcode);

    return opcode switch
    {
      0x4E => Sei(),
      _ => throw new NotImplementedException($"Could not map the opcode '{opcode}' to an addressing mode.")
    };
  }

  private Operation Adc(AddressingMode addressingMode)
  {
    return new Operation("ADC", addressingMode, () =>
    {
      var temp = (ushort)(_cpu.Registers.Accumulator + addressingMode.OperandAddress +
                 _cpu.Registers.ProcessorStatus.GetFlagValue(ProcessorFlag.Carry));
      var overflow = (~(_cpu.Registers.Accumulator ^ addressingMode.OperandAddress) &
                      (_cpu.Registers.Accumulator ^ temp) & 0x0080) == 1;
      
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Carry, temp > 255);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Zero, (temp & 0x00FF) == 0);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Negative, temp > 0x80);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Overflow, overflow);

      _cpu.Registers.Accumulator = (byte)(temp & 0x00FF);
      return 1;
    });
  }
  
  private Operation Sbc(AddressingMode addressingMode)
  {
    return new Operation("SBC", addressingMode, () =>
    {
      var value = (ushort)(addressingMode.OperandAddress ^ 0x00FF);
      var temp = (ushort)(_cpu.Registers.Accumulator + value +
                          _cpu.Registers.ProcessorStatus.GetFlagValue(ProcessorFlag.Carry));
      var overflow = ((temp ^ _cpu.Registers.Accumulator) & (temp ^ value) & 0x0080) == 1;
      
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Carry, (temp & 0xFF00) == 1);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Zero, (temp & 0x00FF) == 0);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Negative, (temp & 0x0080) == 1);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Overflow, overflow);
      
      _cpu.Registers.Accumulator = (byte)(temp & 0x00FF);
      return 1;
    });
  }
  
  private Operation Pha(AddressingMode addressingMode)
  {
    return new Operation("PHA", addressingMode, () =>
    {
      _cpu.Write((ushort)(0x0100 + _cpu.Registers.StackPointer), _cpu.Registers.Accumulator);
      _cpu.Registers.StackPointer--;

      return 0;
    });
  }
  
  private Operation Pla(AddressingMode addressingMode)
  {
    return new Operation("PLA", addressingMode, () =>
    {
      _cpu.Registers.StackPointer++;
      _cpu.Registers.Accumulator = (byte)_cpu.Read((ushort)(0x0100 + _cpu.Registers.StackPointer));
      
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Zero, _cpu.Registers.Accumulator == 0x00);
      _cpu.Registers.ProcessorStatus.SetFlag(ProcessorFlag.Negative, (_cpu.Registers.Accumulator & 0x80) == 1);

      return 0;
    });
  }

  private Operation Sei()
  {
    var implied = _addressingModeFactory.GetAddressingModeFromOpcode(0x4E);
    
    return new Operation("SEI", implied, () =>
    {
      return 2;
    });
  }
}