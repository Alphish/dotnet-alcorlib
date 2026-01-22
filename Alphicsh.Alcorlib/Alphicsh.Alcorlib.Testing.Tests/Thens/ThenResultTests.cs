using Alphicsh.Alcorlib.Testing.Thens;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public partial class ThenResultTests
{
    private void AssertCheckPasses<TResult>(Func<ThenResult<TResult>> check)
    {
        try
        {
            check();
        }
        catch (XunitException)
        {
            Assert.Fail($"Expected the check to pass but it didn't.");
        }
    }

    private void AssertCheckFails<TResult>(Func<ThenResult<TResult>> check)
    {
        Assert.ThrowsAny<XunitException>(check);
    }
}
