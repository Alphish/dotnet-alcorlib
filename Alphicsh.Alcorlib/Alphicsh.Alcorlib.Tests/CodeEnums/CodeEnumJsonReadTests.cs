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
        ThenParseExceptionMessage.ShouldContain("cannot be parsed from");
        ThenParseExceptionMessage.ShouldContain(nameof(TestDirection));
    }

    [Fact]
    public void ShouldFailToReadNonString()
    {
        GivenJson.Of("123");
        WhenDirectionParse.IsAttempted();
        ThenParseException.ShouldBeSet();
        ThenParseExceptionMessage.ShouldContain("can only be parsed from a string");
        ThenParseExceptionMessage.ShouldContain(nameof(TestDirection));
    }

    [Fact]
    public void ShouldReadDictionary()
    {
        GivenJson.Of("{\"Right\":\"Left\",\"Up\":\"Down\",\"Left\":\"Right\",\"Down\":\"Up\"}");
        WhenDictionaryParse.IsExecuted();

        ThenParsedDictionary.Indexed()
            .ShouldHaveEntry(TestDirection.Right, TestDirection.Left)
            .ShouldHaveEntry(TestDirection.Up, TestDirection.Down)
            .ShouldHaveEntry(TestDirection.Left, TestDirection.Right)
            .ShouldHaveEntry(TestDirection.Down, TestDirection.Up)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public void ShouldFailToReadInvalidDictionaryKey()
    {
        GivenJson.Of("{\"Center\":\"Left\"}");
        WhenDictionaryParse.IsAttempted();
        ThenParseException.ShouldBeSet();
        ThenParseExceptionMessage.ShouldContain("cannot be parsed from");
        ThenParseExceptionMessage.ShouldContain(nameof(TestDirection));
    }

    // -----
    // Setup
    // -----

    GivenValue<string> GivenJson { get; } = new GivenValue<string>(nameof(GivenJson));

    WhenFunction<TestDirection?> WhenDirectionParse { get; }
    WhenFunction<IDictionary<TestDirection, TestDirection>> WhenDictionaryParse { get; }

    ThenResult<TestDirection?> ThenParsedDirection { get; } = new ThenResult<TestDirection?>(nameof(ThenParsedDirection));
    ThenDictionary<TestDirection, TestDirection> ThenParsedDictionary { get; } = new ThenDictionary<TestDirection, TestDirection>(nameof(ThenParsedDictionary));
    ThenResult<JsonException> ThenParseException { get; } = new ThenResult<JsonException>(nameof(ThenParseException));
    ThenString ThenParseExceptionMessage { get; } = new ThenString(nameof(ThenParseExceptionMessage));

    public CodeEnumJsonReadTests()
    {
        WhenDirectionParse = new WhenFunction<TestDirection?>(nameof(WhenDirectionParse), () => JsonSerializer.Deserialize<TestDirection>(GivenJson))
            .LinkOutput(ThenParsedDirection)
            .LinkException(ThenParseException)
            .LinkException(ThenParseExceptionMessage, (JsonException exception) => exception.Message);

        WhenDictionaryParse = new WhenFunction<IDictionary<TestDirection, TestDirection>>(nameof(WhenDirectionParse), () => JsonSerializer.Deserialize<Dictionary<TestDirection, TestDirection>>(GivenJson)!)
            .LinkOutput(ThenParsedDictionary)
            .LinkException(ThenParseException)
            .LinkException(ThenParseExceptionMessage, (JsonException exception) => exception.Message);
    }
}
