using log4net;
using Renessance.Hardware.Processor;

namespace Renessance.Emulator;

public class Emulator
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(Emulator));

  private Rom? _rom;

  public Emulator()
  {
    _rom = null;
  }

  public void Start(Rom rom)
  {
    _log.Info($"Starting emulation with ROM '{rom.RomInfo.Name}'.");
  }
}