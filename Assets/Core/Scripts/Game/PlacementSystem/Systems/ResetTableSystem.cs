using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class ResetTableSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsCustomInject<Map> _map;
        private EcsFilterInject<Inc<CMarkPool>> _cMarkPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _bus.Value.GetEventBodies<EResetTable>(out _))
            {
                foreach (var entity in _cMarkPool.Value)
                {
                    var markPools = _cMarkPool.Pools.Inc1.Get(entity);
                    InactivateChildren(markPools.XPool);       
                    InactivateChildren(markPools.OPool);      
                    _map.Value.Clear();
                    Debug.Log("Clear");
                }
            }    
        }

        private void InactivateChildren(Component component)
        {
            foreach (Transform child in component.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}