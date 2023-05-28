namespace Renessance.Emulator.Tests.Helpers;

public class InstructionTest
{
  public string Name { get; set; }
  public CPUState Initial { get; set; }
  public CPUState Final { get; set; }
  public List<object[]> Cycles { get; set; }
  
  public InstructionTest(string name, CPUState initial, CPUState final, List<object[]> cycles)
  {
    Name = name;
    Initial = initial;
    Final = final;
    Cycles = cycles;
  }
}