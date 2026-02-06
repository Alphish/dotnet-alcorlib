using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Tests.CodeEnums;

public class CodeEnumTests
{
    [Fact]
    public void ShouldListAllDirections()
    {
        ThenEnumerable.Of(nameof(TestDirection.AvailableValues), TestDirection.AvailableValues)
            .Enumerated()
            .ShouldYield(TestDirection.Right)
            .ShouldYield(TestDirection.Up)
            .ShouldYield(TestDirection.Left)
            .ShouldYield(TestDirection.Down)
            .ShouldEnd();
    }

    [Fact]
    public void ShouldMatchAllAliases()
    {
        ThenEnumerable.Of(nameof(TestDirection.AvailableValues), TestDirection.AvailableValues)
            .Enumerated()
            .ShouldYield(TestDirection.Right)
            .ShouldYield(TestDirection.Up)
            .ShouldYield(TestDirection.Left)
            .ShouldYield(TestDirection.Down)
            .ShouldEnd();
    }

    // -----
    // Parse
    // -----

    [Fact]
    public void ShouldParseByCode()
    {
        var value = TestDirection.Parse("Right");
        Assert.Equal(TestDirection.Right, value);
    }

    [Fact]
    public void ShouldParseByUppercaseCode()
    {
        var value = TestDirection.Parse("UP");
        Assert.Equal(TestDirection.Up, value);
    }

    [Fact]
    public void ShouldParseByAlias()
    {
        var value = TestDirection.Parse("West");
        Assert.Equal(TestDirection.Left, value);
    }

    [Fact]
    public void ShouldParseByUppercaseAlias()
    {
        var value = TestDirection.Parse("SOUTH");
        Assert.Equal(TestDirection.Down, value);
    }

    [Fact]
    public void ShouldFailToParseByInvalidCode()
    {
        Action action = () => TestDirection.Parse("Center");
        Assert.Throws<ArgumentException>(action);
    }

    // --------
    // TryParse
    // --------

    [Fact]
    public void ShouldAttemptParseByCode()
    {
        var parsed = TestDirection.TryParse("Right", out var value);
        Assert.True(parsed);
        Assert.Equal(TestDirection.Right, value);
    }

    [Fact]
    public void ShouldAttemptParseByUppercaseCode()
    {
        var parsed = TestDirection.TryParse("UP", out var value);
        Assert.True(parsed);
        Assert.Equal(TestDirection.Up, value);
    }

    [Fact]
    public void ShouldAttemptParseByAlias()
    {
        var parsed = TestDirection.TryParse("West", out var value);
        Assert.True(parsed);
        Assert.Equal(TestDirection.Left, value);
    }

    [Fact]
    public void ShouldAttemptParseByUppercaseAlias()
    {
        var parsed = TestDirection.TryParse("SOUTH", out var value);
        Assert.True(parsed);
        Assert.Equal(TestDirection.Down, value);
    }

    [Fact]
    public void ShouldAttemptParseByInvalidCode()
    {
        var parsed = TestDirection.TryParse("Center", out var value);
        Assert.False(parsed);
        Assert.Equal(default(TestDirection), value);
    }
}
