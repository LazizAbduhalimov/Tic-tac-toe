using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem.Alternative
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public bool AutoExpand { get; set; }
        public Transform Container { get; }
        
        private List<T> _pool;

        public PoolMono(T prefab, int count, Transform container = null, bool autoExpand = true)
        {
            Prefab = prefab;
            Container = container;
            AutoExpand = autoExpand;
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActivateByDefault = false)
        {
            var createdObjcet = UnityEngine.Object.Instantiate(Prefab, Container);
            createdObjcet.gameObject.SetActive(isActivateByDefault);
            _pool.Add(createdObjcet);
            return createdObjcet;
        }

        public bool HasFreeElement(out T element, bool activeInHierarchy = true)
        {
            foreach (var mono in _pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(activeInHierarchy);
                    return true;
                }
            }
            element = null;
            return false;
        }

        public T GetFreeElement(bool activeInHierarchy = true)
        {
            if (HasFreeElement(out var element, activeInHierarchy))
                return element;

            if (AutoExpand)
                return CreateObject(true);

            throw new Exception($"There is no free element of type <{typeof(T)}> in pool");
        }
    }
}