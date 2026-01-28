namespace Alphicsh.Alcorlib;

public interface ICodeEnum
{
    string Code { get; }
}

public interface ICodeEnum<TEnum> : ICodeEnum, IParsable<TEnum>
    where TEnum : ICodeEnum<TEnum>
{
    static abstract IEnumerable<TEnum> AvailableValues { get; }
}
