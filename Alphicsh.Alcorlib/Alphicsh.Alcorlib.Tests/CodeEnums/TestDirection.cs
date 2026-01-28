using System.Diagnostics.CodeAnalysis;

namespace Alphicsh.Alcorlib.Tests.CodeEnums;

[ParsableJsonConverter<TestDirection>]
public class TestDirection : ICodeEnum<TestDirection>
{
    private static CodeEnumLookup<TestDirection> Lookup { get; } = new CodeEnumLookup<TestDirection>();

    // ------
    // Values
    // ------

    public static TestDirection Right { get; } = new TestDirection(nameof(Right));
    public static TestDirection East { get; } = Right.WithAlias(nameof(East));
    public static TestDirection Up { get; } = new TestDirection(nameof(Up));
    public static TestDirection North { get; } = Up.WithAlias(nameof(North));
    public static TestDirection Left { get; } = new TestDirection(nameof(Left));
    public static TestDirection West { get; } = Left.WithAlias(nameof(West));
    public static TestDirection Down { get; } = new TestDirection(nameof(Down));
    public static TestDirection South { get; } = Down.WithAlias(nameof(South));

    // -----
    // Setup
    // -----

    public static IEnumerable<TestDirection> AvailableValues => Lookup.AvailableValues;
    public static TestDirection Parse(string code) => Lookup.Parse(code);
    public static bool TryParse(string code, [NotNullWhen(true)] out TestDirection? value) => Lookup.TryParse(code, out value);

    public string Code { get; set; }

    private TestDirection(string code)
    {
        Code = code;
        Lookup.AddValue(this);
    }

    private TestDirection WithAlias(string alias)
    {
        Lookup.AddAlias(alias, this);
        return this;
    }

    public string Format() => Code;
    public override string ToString() => Code;
}
