using FluentAssertions;
using System.Collections;
using Xunit;


namespace TTO.UnitTests;

class TestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 1, 2, 3 };
        yield return new object[] { 1, 1, 2 };
        yield return new object[] { 1, 0, 1 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class CalculatorTest
{
    private readonly Calculator _calculator;

    public static IEnumerable<object[]> TestData => new List<object[]>
    {
        new object[] { 1, 2, 3 },
        new object[] { 1, 1, 2 },
        new object[] { int.MaxValue, 1, int.MinValue }
    };

    public CalculatorTest()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void SumTest1()
    {
        // arrange
        var a = 1;
        var b = 1;

        // act
        var sum = _calculator.Sum(a, b);

        // assert
        Assert.Equal(a + b, sum);
    }

    [Fact] //dane wstawiane wprost w metodzie
    public void SumTest2()
    {
        // arrange
        var a = -4;
        var b = -5;

        // act
        var sum = _calculator.Sum(a, b);

        // assert
        Assert.Equal(a + b, sum);
    }

    //dane przekazywane przez parametry
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(-1, -1, -2)]
    [InlineData(int.MaxValue, 1, int.MinValue)]
    [InlineData(-1, 5, 4)]
    public void SumTestInlineData(int a, int b, int validResult)
    {
        // arrange (jest w parametrze)

        // act
        var sum = _calculator.Sum(a, b);

        // assert
        sum.Should().Be(validResult); //Fluid Assertion (nuget)
        //Assert.Equal(validResult, sum);
    }

    [Theory]
    [InlineData(0, 1, -1)]
    [InlineData(int.MinValue, 1, int.MaxValue)]
    [InlineData(100, 1, 99)]
    [InlineData(100, 2, 98)]
    public void SubtractTestInlineData(int a, int b, int validResult)
    {
        var res = _calculator.Subtract(a, b);

        Assert.Equal(validResult, res);
    }

    [Theory]
    [MemberData(nameof(TestData))] //property z klasy
    public void SumTestMemberData(int a, int b, int validResult)
    {
        var sum = _calculator.Sum(a, b);

        Assert.Equal(validResult, sum);
    }

    [Theory]
    [ClassData(typeof(TestData))] //dane z klasy implementującej IEnumerable<object[]>
    public void SumTestClassData(int a, int b, int validResult)
    {
        var sum = _calculator.Sum(a, b);

        Assert.Equal(validResult, sum);
    }

}