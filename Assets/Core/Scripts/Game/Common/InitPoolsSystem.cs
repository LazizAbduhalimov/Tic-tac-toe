using Game;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    public class InitPoolsSystem : IEcsInitSystem
    {
        private EcsPoolInject<CMarkPool> _cMarkPool;
        
        public void Init(IEcsSystems systems)
        {
            var marksPools = Object.FindObjectOfType<PremadePools>();
            _cMarkPool.NewEntity(out _).Invoke(marksPools.XPool, marksPools.OPool);
        }
    }
}