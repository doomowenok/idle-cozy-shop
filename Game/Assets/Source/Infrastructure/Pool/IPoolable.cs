using UnityEngine;

namespace Infrastructure.Pool
{
    public interface IPoolable
    {
        GameObject PoolObject { get; }
        void Release();
    }
}