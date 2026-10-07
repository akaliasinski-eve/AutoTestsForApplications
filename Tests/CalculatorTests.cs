using AutoTestsForApplications.Components;

namespace AutoTestsForApplications;

[TestFixture]
public class CalculatorTests
{
    [TestCase(1, 2, 3)]
    [TestCase(-2, -6, -8)]
    [TestCase(-5, -10, 5)]
    public void AddCalulatorTest(int a, int b, int result)
    {
        int res = Calculator.Add(a, b);
        Assert.That(res, Is.EqualTo(result), $"Expected result: {result}, Actual result: {res}");
    }
}