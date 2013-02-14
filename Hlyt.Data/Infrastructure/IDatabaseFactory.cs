using System;


namespace Hlyt.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        HlytDataContex Get();
    }
}
