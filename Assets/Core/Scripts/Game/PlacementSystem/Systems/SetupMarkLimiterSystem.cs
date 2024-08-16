using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class SetupMarkLimiterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<Map> _map;
        private EcsCustomInject<EventsBus> _bus;
        private EcsFilterInject<Inc<CMarkLimiter>> _cMarkLimiterFilter;
        private EcsPoolInject<CMarkLimiter> _cMarkLimiterPool;
        private EcsPool<EOnMarkSetup> _eOnMarkSetupPool;
        
        public void Init(IEcsSystems systems)
        {
            _cMarkLimiterPool.NewEntity(out _).Invoke();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eOnMarkSetupPool))
            {
                ref var setup = ref _eOnMarkSetupPool.Get(entity);
                foreach (var limiterEntity in _cMarkLimiterFilter.Value)
                {
                    ref var limiters = ref _cMarkLimiterFilter.Pools.Inc1.Get(limiterEntity);
                    TryDequeueIfMoreThan3Marks(ref limiters, setup.MarkType);
                    AddMarkToQueue(ref limiters, setup);
                }
            }
        }

        private void AddMarkToQueue(ref CMarkLimiter limiters, EOnMarkSetup setup)
        {
            switch (setup.MarkType)
            {
                case Marks.X:
                    limiters.XMarks.Enqueue(setup.Mark);
                    break;
                case Marks.O:
                    limiters.OMarks.Enqueue(setup.Mark);
                    break;
            }
        }

        private void TryDequeueIfMoreThan3Marks(ref CMarkLimiter limiters, Marks markType)
        {
            switch (markType)
            {
                case Marks.X:
                    DequeueIfMoreOrEqual3(limiters.XMarks);
                    break;
                case Marks.O:
                    DequeueIfMoreOrEqual3(limiters.OMarks);
                    break;
            }
        }

        private void DequeueIfMoreOrEqual3(Queue<GameObject> marks)
        {
            if (marks.Count >= 3)
            {
                var poolObject = marks.Dequeue();
                poolObject.gameObject.SetActive(false);
                if (_map.Value.IsCellExists(poolObject.transform.position, out var cell))
                {
                    _map.Value.RemoveCell(poolObject.transform.position);
                }
            }
        }
    }
}