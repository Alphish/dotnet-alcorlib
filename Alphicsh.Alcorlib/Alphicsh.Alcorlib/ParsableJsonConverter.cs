using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alphicsh.Alcorlib;

public class ParsableJsonConverter<TParsable> : JsonConverter<TParsable>
    where TParsable : IParsable<TParsable>
{
    // -------
    // Reading
    // -------

    public override TParsable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"{typeToConvert.Name} can only be parsed from a string.");

        return ReadFromString(ref reader, typeToConvert);
    }

    public override TParsable ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => ReadFromString(ref reader, typeToConvert);

    private TParsable ReadFromString(ref Utf8JsonReader reader, Type typeToConvert)
    {
        if (!TParsable.TryParse(reader.GetString()!, out var value))
            throw new JsonException($"{typeToConvert.Name} cannot be parsed from '{value}'.");

        return value;
    }

    // -------
    // Writing
    // -------

    public override void Write(Utf8JsonWriter writer, TParsable value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Format());
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] TParsable value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.Format());
    }
}
