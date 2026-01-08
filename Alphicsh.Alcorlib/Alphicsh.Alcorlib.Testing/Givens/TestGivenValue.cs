namespace Alphicsh.Alcorlib.Testing.Givens;

public class TestGivenValue<TValue>
{
    public string Name { get; }
    public TValue Value { get; private set; } = default!;
    protected bool IsValueSet { get; private set; } = false;

    public TestGivenValue(string name)
    {
        Name = name;
    }

    public static implicit operator TValue(TestGivenValue<TValue> given) => given.Value;

    public virtual void Of(TValue value)
    {
        SetValue(value);
    }

    protected void SetValue(TValue value)
    {
        if (IsValueSet)
            throw new InvalidOperationException($"The given value of '{Name}' has been already set.");

        Value = value;
        IsValueSet = true;
    }

    public override string ToString()
    {
        var valueString = IsValueSet ? (Value?.ToString() ?? "null") : "<unset>";
        return $"{Name} of {valueString}";
    }
}
