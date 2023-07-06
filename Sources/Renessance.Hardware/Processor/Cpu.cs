using log4net;
using Renessance.Hardware.Memory;
using Renessance.Hardware.Processor.Instructions;
using Renessance.Hardware.Processor.Registers;

namespace Renessance.Hardware.Processor;

internal class Cpu : ICpu
{
  public CpuRegisters Registers { get; private set; }

  private readonly IRam _ram;
  private readonly OperationFactory _operationFactory;
  
  private static readonly ILog _log = LogManager.GetLogger(typeof(Cpu));

  public Cpu(IRam ram)
  {
    Registers = new CpuRegisters();
    _ram = ram;
    _operationFactory = new OperationFactory(this, new AddressingModeFactory(this));
  }
  
  public void ExecuteCycle()
  {
    // Read
    var opcode = Read(Registers.ProgramCounter);
    Registers.ProgramCounter++; // TODO: Maybe move to read?

    Operation instruction;
    
    // Lookup
    try
    {
      instruction = _operationFactory.GetOperationFromOpcode(opcode);
    }
    catch (NotImplementedException exception)
    {
      _log.Warn($"Could not map an opcode: {exception.Message}");
      return;
    }
    
    instruction.Execute();
  }

  public ushort Read(ushort location)
  {
    return _ram.Read(location);
  }

  public void Write(ushort location, byte data)
  {
    _ram.Write(location, data);
  }

  public void Reset()
  {
    _log.Info("Resetting the CPU to initial state.");
    
    Registers.Reset(this);
    // TODO: Reset cycles
  }

  public void LoadRom(byte[] data)
  {
    _ram.LoadRom(data);
  }
}