using Alphicsh.Alcorlib.Testing.Givens;

namespace Alphicsh.Alcorlib.Testing.Tests.Givens;

public class TestGivenListTests
{
    [Fact]
    public void ShouldHaveGivenName()
    {
        var given = new TestGivenList<int>("GivenLorem");
        Assert.Equal("GivenLorem", given.Name);
    }

    [Fact]
    public void ShouldStartWithEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        Assert.IsType<List<int>>(given.Value);
        Assert.Empty(given.Value);
    }

    [Fact]
    public void ShouldSetDirectValue()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Of(new List<int> { 123, 456, 789 });
        Assert.Equal(3, given.Value.Count);
        Assert.Equal(123, given.Value[0]);
        Assert.Equal(456, given.Value[1]);
        Assert.Equal(789, given.Value[2]);
    }

    [Fact]
    public void ShouldAddSingleValue()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Add(123);
        Assert.Single(given.Value);
        Assert.Equal(123, given.Value[0]);
    }

    [Fact]
    public void ShouldAddManyValues()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Add(123);
        given.Add(456);
        given.Add(789);
        Assert.Equal(3, given.Value.Count);
        Assert.Equal(123, given.Value[0]);
        Assert.Equal(456, given.Value[1]);
        Assert.Equal(789, given.Value[2]);
    }

    [Fact]
    public void ShouldChainManyValues()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.With(123).With(456).With(789);
        Assert.Equal(3, given.Value.Count);
        Assert.Equal(123, given.Value[0]);
        Assert.Equal(456, given.Value[1]);
        Assert.Equal(789, given.Value[2]);
    }

    [Fact]
    public void ShouldPreventSettingListTwice()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Of(new List<int>());

        Action testAction = () => given.Of(new List<int>());
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldPreventSettingListAfterAddingItems()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Add(123);

        Action testAction = () => given.Of(new List<int>());
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldAddItemsAfterSettingList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Of(new List<int> { 123, 456 });
        given.Add(789);
        Assert.Equal(3, given.Value.Count);
        Assert.Equal(123, given.Value[0]);
        Assert.Equal(456, given.Value[1]);
        Assert.Equal(789, given.Value[2]);
    }

    [Fact]
    public void ShouldConfirmEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.WithNoItems();
        Assert.IsType<List<int>>(given.Value);
        Assert.Empty(given.Value);
    }

    [Fact]
    public void ShouldConfirmNewlySetEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Of(new List<int>());
        given.WithNoItems();
        Assert.IsType<List<int>>(given.Value);
        Assert.Empty(given.Value);
    }

    [Fact]
    public void ShouldPreventSettingListAfterConfirmingEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.WithNoItems();

        Action testAction = () => given.Of(new List<int>());
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldPreventAddingItemsAfterConfirmingEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.WithNoItems();

        Action testAction = () => given.Add(123);
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldPreventEmptyConfirmationAfterSettingNonEmptyList()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Of(new List<int> { 123, 456, 789 });

        Action testAction = () => given.WithNoItems();
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldPreventEmptyConfirmationAfterAddingItems()
    {
        var given = new TestGivenList<int>("GivenLorem");
        given.Add(123);

        Action testAction = () => given.WithNoItems();
        Assert.Throws<InvalidOperationException>(testAction);
    }
}
