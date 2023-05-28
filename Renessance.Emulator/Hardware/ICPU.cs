namespace Renessance.Emulator.Hardware;

public interface ICPU
{
  void Clock();
  void Reset();
  void InterruptRequest();
  void NonMaskableInterruptRequest();
}