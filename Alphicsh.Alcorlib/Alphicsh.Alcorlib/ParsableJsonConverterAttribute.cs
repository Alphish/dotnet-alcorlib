using System.Text.Json.Serialization;

namespace Alphicsh.Alcorlib;

public class ParsableJsonConverterAttribute<TParsable> : JsonConverterAttribute
    where TParsable : IParsable<TParsable>
{
    public ParsableJsonConverterAttribute() : base(typeof(ParsableJsonConverter<TParsable>)) { }
}
