using log4net;

namespace Renessance.Hardware.Memory;

internal class Ram : IRam
{
  private static readonly ILog _log = LogManager.GetLogger(typeof(Ram));
  
  private readonly byte[] _memory;

  public Ram(int ramSizeInBytes)
  {
    _memory = new byte[ramSizeInBytes];
  }

  public byte Read(int location)
  {
#if DEBUG
    _log.Debug($"Attempting to read from location {location} in memory.");
#endif
    return _memory[location];
  }

  public void Write(int location, byte data)
  {
    _memory[location] = data;
  }

  public void LoadRom(byte[] data)
  {
    const int ROM_LOCATION = 0x0;
    _log.Info($"Loading {data.Length} bytes of ROM data into location {ROM_LOCATION}.");
    data.CopyTo(_memory, ROM_LOCATION);
  }
}