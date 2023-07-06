namespace Renessance.Hardware.Processor.Instructions;

internal class AddressingModeFactory
{
  private readonly Cpu _cpu;

  public AddressingModeFactory(Cpu cpu)
  {
    _cpu = cpu;
  }

  public AddressingMode GetAddressingModeFromOpcode(ushort opcode)
  {
    return opcode switch
    {
      0x4E => Imp(),
      _ => throw new NotImplementedException($"Could not map the opcode '${opcode}' to an addressing mode.")
    };
  }
  
  private AddressingMode Imp()
  {
    return new AddressingMode(nameof(Imp), _cpu.Registers.Accumulator);
  }
  
  private AddressingMode Imm()
  {
    return new AddressingMode(nameof(Imm), _cpu.Read(_cpu.Registers.ProgramCounter++));
  }

  private AddressingMode Zp0()
  {
    var absoluteAddress = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;
    absoluteAddress &= 0x00FF;

    return new AddressingMode(nameof(Zp0), _cpu.Read(absoluteAddress));
  }

  private AddressingMode Zpx()
  {
    var absoluteAddress = (ushort)(_cpu.Read(_cpu.Registers.ProgramCounter) + _cpu.Registers.XIndexRegister);
    _cpu.Registers.ProgramCounter++;
    absoluteAddress &= 0x00FF;

    return new AddressingMode(nameof(Zpx), _cpu.Read(absoluteAddress));
  }
  
  private AddressingMode Zpy()
  {
    var absoluteAddress = (ushort)(_cpu.Read(_cpu.Registers.ProgramCounter) + _cpu.Registers.YIndexRegister);
    _cpu.Registers.ProgramCounter++;
    absoluteAddress &= 0x00FF;

    return new AddressingMode(nameof(Zpy), _cpu.Read(absoluteAddress));
  }

  private AddressingMode Abs()
  {
    var lo = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var hi = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var absoluteAddress = (ushort)((hi << 8) | lo);

    return new AddressingMode(nameof(Abs), _cpu.Read(absoluteAddress));
  }
  
  private AddressingMode Abx()
  {
    var lo = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var hi = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var absoluteAddress = (ushort)((hi << 8) | lo);
    absoluteAddress += _cpu.Registers.XIndexRegister;

    var requiresCycle = (absoluteAddress & 0xFF00) != hi << 8;

    return new AddressingMode(nameof(Abx), _cpu.Read(absoluteAddress), requiresCycle);
  }
  
  private AddressingMode Aby()
  {
    var lo = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var hi = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var absoluteAddress = (ushort)((hi << 8) | lo);
    absoluteAddress += _cpu.Registers.YIndexRegister;

    var requiresCycle = (absoluteAddress & 0xFF00) != hi << 8;

    return new AddressingMode(nameof(Aby), _cpu.Read(absoluteAddress), requiresCycle);
  }

  private AddressingMode Ind()
  {
    var lo = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var hi = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var pointer = (hi << 8) | lo;

    ushort absoluteAddress;
    
    // Simulate page boundary hardware bug
    if (lo == 0x00FF)
    {
      absoluteAddress = (ushort)((_cpu.Read((ushort)(pointer & 0xFF00)) << 8) | _cpu.Read((ushort)pointer));
    }
    // Behave normally
    else
    {
      absoluteAddress = (ushort)((_cpu.Read((ushort)(pointer + 1)) << 8) | _cpu.Read((ushort)pointer));
    }

    return new AddressingMode(nameof(Ind), _cpu.Read(absoluteAddress));
  }

  private AddressingMode Izx()
  {
    var suppliedAddress = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var lo = _cpu.Read((ushort)((ushort)(suppliedAddress + _cpu.Registers.XIndexRegister) & 0x00FF));
    var hi = _cpu.Read((ushort)((ushort)(suppliedAddress + _cpu.Registers.XIndexRegister + 1) & 0x00FF));

    var absoluteAddress = (ushort)((hi << 8) | lo);

    return new AddressingMode(nameof(Izx), _cpu.Read(absoluteAddress));
  }
  
  private AddressingMode Izy()
  {
    var suppliedAddress = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    var lo = _cpu.Read((ushort)(suppliedAddress & 0x00FF));
    var hi = _cpu.Read((ushort)((suppliedAddress + 1) & 0x00FF));

    var absoluteAddress = (ushort)((hi << 8) | lo);
    absoluteAddress += _cpu.Registers.YIndexRegister;

    var requiresCycle = (absoluteAddress & 0xFF00) != hi << 8;

    return new AddressingMode(nameof(Izx), _cpu.Read(absoluteAddress), requiresCycle);
  }

  private AddressingMode Rel()
  {
    var relativeAddress = _cpu.Read(_cpu.Registers.ProgramCounter);
    _cpu.Registers.ProgramCounter++;

    if ((relativeAddress & 0x80) == 1)
    {
      relativeAddress |= 0xFF00;
    }

    return new AddressingMode(nameof(Rel), _cpu.Read(relativeAddress));
  }
}