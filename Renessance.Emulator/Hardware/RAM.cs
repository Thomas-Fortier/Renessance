namespace Renessance.Emulator.Hardware;

public class RAM
{
  private readonly byte[] _data;

  public RAM(byte[] data)
  {
    const int maximumMemory = 1024 * 64;

    if (data.Length != maximumMemory)
    {
      throw new ArgumentException($"Data length must be set to {maximumMemory}.");
    }
    
    _data = data;
  }

  public void Initialize()
  {
    for (var index = 0; index < _data.Length; index++)
    {
      _data[index] = 0x0;
    }
  }
  
  public byte Read(ushort address)
  {
    return _data[address];
  }

  public void Write(ushort address, byte data)
  {
    _data[address] = data;
  }
}