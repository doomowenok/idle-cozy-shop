using System;
using System.Collections.Generic;

namespace Infrastructure.Pool
{
    public sealed class ObjectPool : IDisposable, IObjectPool
    {
        private readonly IDictionary<Type, Stack<IPoolable>> _pools = new Dictionary<Type, Stack<IPoolable>>(32);

        public bool TryRent<TPoolable>(out TPoolable instance) where TPoolable : class, IPoolable
        {
            instance = null;
            
            if (!_pools.TryGetValue(typeof(TPoolable), out Stack<IPoolable> pool)) return false;

            if (_pools[typeof(TPoolable)].TryPop(out IPoolable poolObject))
            {
                instance = poolObject as TPoolable;
                return true;
            }

            return false;
        }

        public void Release<TPoolable>(TPoolable instance) where TPoolable : class, IPoolable
        {
            if (instance == null) return;
            
            instance.Release();

            if (!_pools.ContainsKey(typeof(TPoolable)))
            {
                _pools.Add(typeof(TPoolable), new Stack<IPoolable>(32));
            }

            _pools[typeof(TPoolable)].Push(instance);
        }

        public int GetSize<TPoolable>() where TPoolable : class, IPoolable => 
            !_pools.TryGetValue(typeof(TPoolable), out Stack<IPoolable> pool) ? 0 : pool.Count;

        public void Clear<TPoolable>() where TPoolable : class, IPoolable
        {
            if (_pools.TryGetValue(typeof(TPoolable), out Stack<IPoolable> collection))
            {
                collection.Clear();
            }
        }

        public void Dispose()
        {
            foreach (KeyValuePair<Type, Stack<IPoolable>> pool in _pools)
            {
                pool.Value.Clear();
            }
            
            _pools.Clear();
        }
    }
}