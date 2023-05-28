namespace Renessance.Emulator.Hardware.Processor;

public interface IProcessor
{
  void Clock();
  void Reset();
  void InterruptRequest();
  void NonMaskableInterruptRequest();

  byte FetchByte();
}