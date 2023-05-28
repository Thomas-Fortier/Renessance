namespace Renessance.Emulator.Hardware;

public interface IRAM
{
  void Initialize();
  byte Read(ushort address);
  void Write(ushort address, byte data);
}