using System.Text.Json;
using Alphicsh.Alcorlib.Testing.Givens;
using Alphicsh.Alcorlib.Testing.Thens;
using Alphicsh.Alcorlib.Testing.Whens;

namespace Alphicsh.Alcorlib.Tests.CodeEnums;

public class CodeEnumJsonReadTests
{
    [Fact]
    public void ShouldReadNull()
    {
        GivenJson.Of("null");
        WhenDirectionParse.IsExecuted();
        ThenParsedDirection.ShouldBe(null);
    }

    [Fact]
    public void ShouldReadCorrectValue()
    {
        GivenJson.Of("\"Right\"");
        WhenDirectionParse.IsExecuted();
        ThenParsedDirection.ShouldBe(TestDirection.Right);
    }

    [Fact]
    public void ShouldReadUppercaseValue()
    {
        GivenJson.Of("\"UP\"");
        WhenDirectionParse.IsExecuted();
        ThenParsedDirection.ShouldBe(TestDirection.Up);
    }

    [Fact]
    public void ShouldReadAlias()
    {
        GivenJson.Of("\"West\"");
        WhenDirectionParse.IsExecuted();
        ThenParsedDirection.ShouldBe(TestDirection.Left);
    }

    [Fact]
    public void ShouldReadUppercaseAlias()
    {
        GivenJson.Of("\"SOUTH\"");
        WhenDirectionParse.IsExecuted();
        ThenParsedDirection.ShouldBe(TestDirection.Down);
    }

    [Fact]
    public void ShouldFailToReadInvalidString()
    {
        GivenJson.Of("\"Center\"");
        WhenDirectionParse.IsAttempted();
        ThenParseException.ShouldBeSet();
        Assert.Contains("cannot be parsed from", ThenParseException.Result.Message);
        Assert.Contains(nameof(TestDirection), ThenParseException.Result.Message);
    }

    [Fact]
    public void ShouldFailToReadNonString()
    {
        GivenJson.Of("123");
        WhenDirectionParse.IsAttempted();
        ThenParseException.ShouldBeSet();
        Assert.Contains("can only be parsed from a string", ThenParseException.Result.Message);
        Assert.Contains(nameof(TestDirection), ThenParseException.Result.Message);
    }

    [Fact]
    public void ShouldReadDictionary()
    {
        GivenJson.Of("{\"Right\":\"Left\",\"Up\":\"Down\",\"Left\":\"Right\",\"Down\":\"Up\"}");
        WhenDictionaryParse.IsExecuted();
        Assert.Equal(ThenParsedDictionary.Result[TestDirection.Right], TestDirection.Left);
        Assert.Equal(ThenParsedDictionary.Result[TestDirection.Up], TestDirection.Down);
        Assert.Equal(ThenParsedDictionary.Result[TestDirection.Left], TestDirection.Right);
        Assert.Equal(ThenParsedDictionary.Result[TestDirection.Down], TestDirection.Up);
    }

    [Fact]
    public void ShouldFailToReadInvalidDictionaryKey()
    {
        GivenJson.Of("{\"Center\":\"Left\"}");
        WhenDictionaryParse.IsAttempted();
        ThenParseException.ShouldBeSet();
        Assert.Contains("cannot be parsed from", ThenParseException.Result.Message);
        Assert.Contains(nameof(TestDirection), ThenParseException.Result.Message);
    }

    // -----
    // Setup
    // -----

    GivenValue<string> GivenJson { get; } = new GivenValue<string>(nameof(GivenJson));

    WhenFunction<TestDirection?> WhenDirectionParse { get; }
    WhenFunction<Dictionary<TestDirection, TestDirection>> WhenDictionaryParse { get; }

    ThenResult<TestDirection?> ThenParsedDirection { get; } = new ThenResult<TestDirection?>(nameof(ThenParsedDirection));
    ThenResult<Dictionary<TestDirection, TestDirection>> ThenParsedDictionary { get; } = new ThenResult<Dictionary<TestDirection, TestDirection>>(nameof(ThenParsedDictionary));
    ThenResult<JsonException> ThenParseException { get; } = new ThenResult<JsonException>(nameof(ThenParseException));

    public CodeEnumJsonReadTests()
    {
        WhenDirectionParse = new WhenFunction<TestDirection?>(nameof(WhenDirectionParse), () => JsonSerializer.Deserialize<TestDirection>(GivenJson))
            .LinkOutput(ThenParsedDirection)
            .LinkException(ThenParseException);

        WhenDictionaryParse = new WhenFunction<Dictionary<TestDirection, TestDirection>>(nameof(WhenDirectionParse), () => JsonSerializer.Deserialize<Dictionary<TestDirection, TestDirection>>(GivenJson)!)
            .LinkOutput(ThenParsedDictionary)
            .LinkException(ThenParseException);
    }
}
