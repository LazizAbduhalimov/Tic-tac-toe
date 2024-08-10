using System;
using UnityEngine;

namespace PoolSystem.Alternative
{
    public class PoolContainer : MonoBehaviour
    {
        public PoolMono<PoolObject> Pool { get; private set; }

        [SerializeField] private string _poolName = "New Pool";
        [SerializeField] private int _poolCount = 5;
        [SerializeField] private bool _autoExpand = true;
        [SerializeField] private PoolObject _poolObject;
        [SerializeField] private Transform _container;

        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_poolName))
            {
                name = _poolName;
            }

            if (_container == null)
                _container = transform;
        }

        private void Start()
        {
            Pool = new PoolMono<PoolObject>(_poolObject, _poolCount, _container);
            Pool.AutoExpand = _autoExpand;
        }

        public PoolObject GetFromPool(Vector3 position)
        {
            var poolObject = Pool.GetFreeElement();
            poolObject.transform.position = position;
            return poolObject;
        }
    }
}
