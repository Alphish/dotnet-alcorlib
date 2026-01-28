using System.Diagnostics.CodeAnalysis;

namespace Alphicsh.Alcorlib;

public interface IParsable<TParsable> where TParsable : IParsable<TParsable>
{
    static abstract TParsable Parse(string code);
    static abstract bool TryParse(string code, [NotNullWhen(true)] out TParsable? value);
    string Format();
}
