namespace Alphicsh.Alcorlib.Testing.Givens;

public class TestGivenList<TItem> : TestGivenValue<List<TItem>>
{
    protected bool IsConfirmedEmpty { get; set; }

    public TestGivenList(string name) : base(name)
    {
        Value = new List<TItem>();
    }

    public virtual TestGivenList<TItem> With(TItem item)
    {
        AddItem(item);
        return this;
    }

    public virtual void Add(TItem item)
        => AddItem(item);

    protected void AddItem(TItem item)
    {
        if (IsConfirmedEmpty)
            throw new InvalidOperationException($"The given list of '{Name}' cannot have items.");

        Value.Add(item);

        // prevent replacing with another list after adding any item
        IsValueSet = true;
    }

    public virtual TestGivenList<TItem> WithNoItems()
    {
        if (Value.Count > 0)
            throw new InvalidOperationException($"Cannot mark given list of '{Name}' as having no items, because it already has items.");

        IsConfirmedEmpty = true;
        IsValueSet = true; // prevent replacing with another list after confirming empty
        return this;
    }
}
