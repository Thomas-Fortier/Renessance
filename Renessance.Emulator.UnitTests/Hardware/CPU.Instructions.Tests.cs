using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Renessance.Emulator.Hardware;

namespace Renessance.Emulator.Tests.Hardware;

[TestFixture]
public class CPUInstructionTests
{
  #region LDA

  [Test]
  public void LDA_ZP0_ShouldSetAccumulatorToProperDataFromMemory()
  {
    // Arrange
    const byte expectedValue = 0xFF;
    var internalRam = new byte[1024 * 64];
    internalRam[0x0] = 0xA5;
    internalRam[0x1] = 0xA6;
    internalRam[0xA6] = expectedValue;
    
    var ram = Substitute.For<RAM>(internalRam);
    var subject = new CPU(ram);
    
    // Act
    Clock(subject, 8); // Reset
    Clock(subject, 3); // LDA_ZERO
    
    var result = subject.Accumulator;

    // Assert
    result.Should().Be(expectedValue);
  }
  
  [Test]
  public void LDA_IMM_ShouldSetAccumulatorToProperDataFromMemory()
  {
    // Arrange
    const byte expectedValue = 0xFF;
    var internalRam = new byte[1024 * 64];
    internalRam[0x0] = 0xA9;
    internalRam[0x1] = expectedValue;

    var ram = Substitute.For<RAM>(internalRam);
    var subject = new CPU(ram);
    
    // Act
    Clock(subject, 8); // Reset
    Clock(subject, 2); // LDA_ZERO
    
    var result = subject.Accumulator;

    // Assert
    result.Should().Be(expectedValue);
  }
  
  [Test]
  public void LDX_IMM_ShouldSetXRegisterToProperDataFromMemory()
  {
    // Arrange
    const byte expectedValue = 0xFF;
    var internalRam = new byte[1024 * 64];
    internalRam[0x0] = 0xA2;
    internalRam[0x1] = expectedValue;

    var ram = Substitute.For<RAM>(internalRam);
    var subject = new CPU(ram);
    
    // Act
    Clock(subject, 8); // Reset
    Clock(subject, 2); // LDA_ZERO
    
    var result = subject.XRegister;

    // Assert
    result.Should().Be(expectedValue);
  }

  #endregion

  private static void Clock(ICPU cpu, int cycles)
  {
    while (cycles > 0)
    {
      cpu.Clock();
      cycles--;
    }
  }
}