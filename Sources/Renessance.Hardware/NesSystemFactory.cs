using Renessance.Hardware.Memory;
using Renessance.Hardware.Processor;

namespace Renessance.Hardware;

public static class NesSystemFactory
{
  public static INesSystem CreateNesSystem()
  {
    return new NesSystem(new Cpu(new Ram(64 * 1024)));
  }
}