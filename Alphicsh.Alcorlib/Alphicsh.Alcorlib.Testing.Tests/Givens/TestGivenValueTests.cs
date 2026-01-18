using Alphicsh.Alcorlib.Testing.Givens;

namespace Alphicsh.Alcorlib.Testing.Tests.Givens;

public class TestGivenValueTests
{
    [Fact]
    public void ShouldHaveGivenName()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        Assert.Equal("GivenLorem", given.Name);
    }

    [Fact]
    public void ShouldStartWithDefaultValue()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        Assert.Equal(default(int), given.Value);
    }

    [Fact]
    public void ShouldUsePassedValue()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        given.Of(123);
        Assert.Equal(123, given.Value);
    }

    [Fact]
    public void ShouldPreventSettingValueTwice()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        given.Of(123);

        Action testAction = () => given.Of(456);
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldActAsInnerType()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        given.Of(123);
        var result = given + 456;
        Assert.Equal(579, result);
    }

    [Fact]
    public void ShouldHaveCorrectStringRepresentation()
    {
        var given = new TestGivenValue<int>("GivenLorem");
        given.Of(123);
        Assert.Equal("GivenLorem of 123", given.ToString());
    }
}
