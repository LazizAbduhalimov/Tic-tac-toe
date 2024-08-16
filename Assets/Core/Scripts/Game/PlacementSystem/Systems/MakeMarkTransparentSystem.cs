using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class MakeMarkTransparentSystem : IEcsRunSystem
    {
        private EcsCustomInject<Map> _map;
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CMarkLimiter>> _cMarkLimiterFilter;
        private EcsPoolInject<CMarkLimiter> _cMarkLimiterPool;
        private EcsPool<EOnMarkSetup> _eOnMarkSetupPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eOnMarkSetupPool))
            {
                ref var setup = ref _eOnMarkSetupPool.Get(entity);
                MakeObjectTransparent(setup.Mark, 1f);
                foreach (var limiterEntity in _cMarkLimiterFilter.Value)
                {
                    ref var limiters = ref _cMarkLimiterFilter.Pools.Inc1.Get(limiterEntity);
                    MakeDisappearingMarkTransparent(ref limiters);
                }
            }
        }

        private void MakeDisappearingMarkTransparent(ref CMarkLimiter limiters)
        {
            if (limiters.XMarks.Count == 3)
            {
                var xMark = limiters.XMarks.Peek();
                MakeObjectTransparent(xMark, 0.5f);
            }
            
            if (limiters.OMarks.Count == 3)
            {
                var oMark = limiters.OMarks.Peek();
                MakeObjectTransparent(oMark, 0.5f);
            }
        }

        private void MakeObjectTransparent(GameObject gameObject, float transparency)
        {
            var material = gameObject.GetComponentInChildren<MeshRenderer>().material;
            var color = material.color;
            color.a = transparency;
            material.color = color;
        }
    }
}