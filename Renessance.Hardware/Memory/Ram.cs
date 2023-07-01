namespace Renessance.Hardware.Memory;

public class Ram : IRam
{
  private readonly byte[] _memory;

  public Ram()
  {
    _memory = new byte[2048];
  }

  public byte Read(int location)
  {
    return _memory[location];
  }

  public void Write(int location, byte data)
  {
    _memory[location] = data;
  }
}