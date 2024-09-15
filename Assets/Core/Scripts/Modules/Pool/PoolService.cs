using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PoolSystem.Alternative
{
    public class PoolService
    {
        private Dictionary<Type, object> _pools = new();
        private Transform _containersHolder;

        public PoolService(string containerName)
        {
            _containersHolder = new GameObject(containerName).transform;
        }

        public PoolMono<T> GetPool<T>(T poolObject) where T : PoolObject
        {
            if (!_pools.TryGetValue(typeof(T), out var pool)) return null;
            var typedPool = pool as List<PoolMono<T>>;
            return typedPool?.Find(p => p.Prefab == poolObject);
        }

        public bool HasPool<T>(T poolObject) where T : PoolObject
        {
            if (!_pools.TryGetValue(typeof(T), out var pool)) return false;
            var typedPool = pool as List<PoolMono<T>>;
            return typedPool?.Any(poolMono => poolMono.Prefab == poolObject) ?? false;
        }

        public PoolMono<T> RegisterPool<T>(T prefab, int count, Transform container = null, bool autoExpand = true) where T : PoolObject
        {
            if (prefab == null)
                throw new Exception("Prefab for pool cannot be null");

            container = CreateContainerIfNullAndReturnParent(container, prefab.name);
            var pool = new PoolMono<T>(prefab, count, container, autoExpand);

            if (!_pools.TryGetValue(typeof(T), out var poolList))
            {
                poolList = new List<PoolMono<T>>();
                _pools[typeof(T)] = poolList;
            }

            var typedPoolList = poolList as List<PoolMono<T>>;
            typedPoolList?.Add(pool);

            return pool;
        }

        public PoolMono<T> GetOrRegisterPool<T>(T prefab, int count, Transform container = null, bool autoExpand = true) where T : PoolObject
        {
            return HasPool(prefab) ? GetPool(prefab) : RegisterPool(prefab, count, container, autoExpand);
        }

        private Transform CreateContainerIfNullAndReturnParent(Transform container, string containerName)
        {
            if (container == null)
                container = new GameObject(containerName).transform;
            container.parent = _containersHolder;
            return container;
        }
    }
}