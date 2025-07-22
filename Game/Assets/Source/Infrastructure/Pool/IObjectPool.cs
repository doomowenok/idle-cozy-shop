namespace Infrastructure.Pool
{
    public interface IObjectPool
    {
        bool TryRent<TPoolable>(out TPoolable instance) where TPoolable : class, IPoolable;
        void Release<TPoolable>(TPoolable instance) where TPoolable : class, IPoolable;
        int GetSize<TPoolable>() where TPoolable : class, IPoolable;
        void Clear<TPoolable>() where TPoolable : class, IPoolable;
    }
}