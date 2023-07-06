using log4net;

namespace Renessance.Hardware.Processor.Instructions;

public class Operation
{
  public string Name { get; private set; }
  public AddressingMode AddressingMode { get; private set; }

  private static readonly ILog _log = LogManager.GetLogger(typeof(Operation));
  private readonly Func<int> _action;

  public Operation(string name, AddressingMode addressingMode, Func<int> action)
  {
    Name = name;
    AddressingMode = addressingMode;
    _action = action;
  }
  
  public void Execute()
  {
#if DEBUG
    _log.Debug($"Executing operation {Name} with addressing mode {AddressingMode.Name}.");
#endif
    _action.Invoke();
  }
}