namespace Renessance.Emulator.Hardware;

public class StatusRegister
{
  public bool Carry { get; set; }
  public bool Zero { get; set; }
  public bool InterruptDisable { get; set; }
  public bool Decimal { get; set; }
  public bool Unused { get; set; }
  public bool Overflow { get; set; }
  public bool Negative { get; set; }

  public void Reset()
  {
    Carry = false;
    Zero = false;
    InterruptDisable = false;
    Decimal = false;
    Unused = false;
    Overflow = false;
    Negative = false;
  }
}