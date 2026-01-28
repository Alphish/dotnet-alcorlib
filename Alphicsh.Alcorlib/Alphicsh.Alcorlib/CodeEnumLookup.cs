namespace Alphicsh.Alcorlib;

public class CodeEnumLookup<TEnum> where TEnum : ICodeEnum<TEnum>
{
    private List<TEnum> ValuesList { get; }
    public IEnumerable<TEnum> AvailableValues => ValuesList;

    private Dictionary<string, TEnum> ValuesByCode { get; }

    public CodeEnumLookup() : this(StringComparer.OrdinalIgnoreCase) { }

    public CodeEnumLookup(IEqualityComparer<string> codeComparer)
    {
        ValuesList = new List<TEnum>();
        ValuesByCode = new Dictionary<string, TEnum>(codeComparer);
    }

    public void AddValue(TEnum value)
    {
        ValuesList.Add(value);
        ValuesByCode.Add(value.Code, value);
    }

    public void AddAlias(string code, TEnum value)
    {
        ValuesByCode.Add(code, value);
    }

    public TEnum Parse(string code)
    {
        if (!ValuesByCode.TryGetValue(code, out var value))
            throw new ArgumentException($"'{code}' is not a valid code for {typeof(TEnum).Name}.", nameof(code));

        return value;
    }

    public bool TryParse(string code, out TEnum? result)
    {
        if (!ValuesByCode.TryGetValue(code, out var value))
        {
            result = default;
            return false;
        }
        result = value;
        return true;
    }
}
