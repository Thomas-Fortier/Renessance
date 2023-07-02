namespace Renessance.Hardware.Processor.Instructions;

public class AddressingMode
{
  public string Name { get; private set; }
  public ushort FetchedData { get; set; }
  public bool RequiresExtraCycle { get; set; }

  public AddressingMode(string name, ushort fetchedData, bool requiresExtraCycle = false)
  {
    Name = name;
    FetchedData = fetchedData;
    RequiresExtraCycle = requiresExtraCycle;
  }
}