using Microsoft.Extensions.DependencyInjection;

namespace TestProject1.Preparations;

public abstract class TestFixtureBase : IClassFixture<BlogAppFactory>, IDisposable
{
    protected readonly IServiceScope Scope;

    protected TestFixtureBase(BlogAppFactory factory)
    {
        factory.InitializeAsync().GetAwaiter().GetResult();
        Scope = factory.Services.CreateScope();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Scope.Dispose();
        }
    }
}