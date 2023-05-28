namespace Renessance.Emulator.Tests.Helpers;

// ReSharper disable once ClassNeverInstantiated.Global
public class CPUState
{
  public ushort PC { get; set; }
  public byte S { get; set; }
  public byte A { get; set; }
  public byte X { get; set; }
  public byte Y { get; set; }
  public byte P { get; set; }
  public List<int[]> RAM { get; set; } = null!;
}