using Flags.Application.RemoveLater;

namespace Flags.Tests;

public class CalculatorTest
{
    private readonly Calculator _calculator = new();

    [Fact]
    public void Add_ShouldRemoveTheSum()
    {
        var result = _calculator.Add(4, 5);
        Assert.Equal(9, result);
    }
}