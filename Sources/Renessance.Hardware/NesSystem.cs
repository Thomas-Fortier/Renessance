using Renessance.Hardware.Processor;

namespace Renessance.Hardware;

internal class NesSystem : INesSystem
{
  private readonly ICpu _cpu;
  
  public NesSystem(ICpu cpu)
  {
    _cpu = cpu;
  }
  
  public void LoadRom(byte[] data)
  {
    _cpu.LoadRom(data);
  }

  public void ExecuteCycle()
  {
    _cpu.ExecuteCycle();
    Thread.Sleep(TimeSpan.FromSeconds(0.55866));
  }
}