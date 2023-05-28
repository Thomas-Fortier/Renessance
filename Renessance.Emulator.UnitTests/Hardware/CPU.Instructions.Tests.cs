using FluentAssertions;
using Newtonsoft.Json.Linq;
using NSubstitute;
using NUnit.Framework;
using Renessance.Emulator.Hardware;
using Renessance.Emulator.Tests.Helpers;

namespace Renessance.Emulator.Tests.Hardware;

[TestFixture]
public class CPUInstructionTests
{
  #region LDA

  [Test]
  public void LDA_ZP0_ShouldSetAccumulatorToProperValueFromMemory()
  {
    RunInstructionTest("a5", 3);
  }
  
  [Test]
  public void LDA_IMM_ShouldSetAccumulatorToProperValueFromMemory()
  {
    RunInstructionTest("a9", 2);
  }
  
  #endregion

  #region LDX

  [Test]
  public void LDX_IMM_ShouldSetXRegisterToProperValueFromMemory()
  {
    RunInstructionTest("a2", 2);
  }

  #endregion

  #region Helpers

  private static void Clock(CPU cpu, int cycles)
  {
    while (cycles > 0)
    {
      cpu.Clock();
      cycles--;
    }
  }

  private static void RunInstructionTest(string opcode, int instructionCycles)
  {
    foreach (var test in GetTestsFromJson(opcode))
    {
      // Arrange
      var subject = InitializeNewCpu(test.Initial);

      // Act
      Clock(subject, 8); // Reset
      Clock(subject, instructionCycles);

      // Assert
      AssertIsEqualToState(subject, test.Final);
    }
  }

  private static List<InstructionTest> GetTestsFromJson(string opcode)
  {
    var json = File.ReadAllText($@"C:\Users\Thomas\Documents\Coding\Repos\Renessance\Renessance.Emulator.UnitTests\JsonInstructionTests\{opcode}.json");
    var jsonTests = JArray.Parse(json);
    return (from JObject testAttribute in jsonTests select testAttribute.ToObject<InstructionTest>()).ToList();
  }

  private static CPU InitializeNewCpu(CPUState state)
  {
    var internalRam = new byte[1024 * 64];
    foreach (var ramKeyValue in state.RAM)
      internalRam[ramKeyValue[0]] = (byte) ramKeyValue[1];
    
    var ram = Substitute.For<RAM>(internalRam);
    
    return new CPU(ram)
    {
      ProgramCounter = state.PC,
      Accumulator = state.A,
      XRegister = state.X,
      YRegister = state.Y
    };
  }

  private static void AssertIsEqualToState(CPU subject, CPUState expectedResult)
  {
    subject.Accumulator.Should().Be(expectedResult.A);
    subject.XRegister.Should().Be(expectedResult.X);
    subject.YRegister.Should().Be(expectedResult.Y);
    subject.ProgramCounter.Should().Be(expectedResult.PC);
  }

  #endregion
}