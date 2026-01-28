using System.Text.Json;
using Alphicsh.Alcorlib.Testing.Givens;
using Alphicsh.Alcorlib.Testing.Thens;
using Alphicsh.Alcorlib.Testing.Whens;

namespace Alphicsh.Alcorlib.Tests.CodeEnums;

public class CodeEnumJsonWriteTests
{
    [Fact]
    public void ShouldWriteNullToJson()
    {
        GivenDirection.Of(null);
        WhenDirectionWrite.IsExecuted();
        ThenJson.ShouldBe("null");
    }

    [Fact]
    public void ShouldWriteDirectionToJson()
    {
        GivenDirection.Of(TestDirection.Right);
        WhenDirectionWrite.IsExecuted();
        ThenJson.ShouldBe($"\"Right\"");
    }

    [Fact]
    public void ShouldWriteDictionaryToJson()
    {
        GivenDictionary.Of(new Dictionary<TestDirection, TestDirection>
        {
            [TestDirection.Right] = TestDirection.Left,
            [TestDirection.Up] = TestDirection.Down,
            [TestDirection.Left] = TestDirection.Right,
            [TestDirection.Down] = TestDirection.Up,
        });
        WhenDictionaryWrite.IsExecuted();
        Assert.Contains("\"Right\":\"Left\"", ThenJson.Result);
        Assert.Contains("\"Up\":\"Down\"", ThenJson.Result);
        Assert.Contains("\"Left\":\"Right\"", ThenJson.Result);
        Assert.Contains("\"Down\":\"Up\"", ThenJson.Result);
    }

    // -----
    // Setup
    // -----

    private GivenValue<TestDirection?> GivenDirection { get; } = new GivenValue<TestDirection?>(nameof(GivenDirection));
    private GivenValue<Dictionary<TestDirection, TestDirection>> GivenDictionary { get; } = new GivenValue<Dictionary<TestDirection, TestDirection>>(nameof(GivenDictionary));

    private WhenFunction<string> WhenDirectionWrite { get; }
    private WhenFunction<string> WhenDictionaryWrite { get; }

    private ThenResult<string> ThenJson { get; } = new ThenResult<string>(nameof(ThenJson));

    public CodeEnumJsonWriteTests()
    {
        WhenDirectionWrite = new WhenFunction<string>(nameof(WhenDirectionWrite), () => JsonSerializer.Serialize(GivenDirection.Value))
            .LinkOutput(ThenJson);

        WhenDictionaryWrite = new WhenFunction<string>(nameof(WhenDictionaryWrite), () => JsonSerializer.Serialize(GivenDictionary.Value))
            .LinkOutput(ThenJson);
    }
}
