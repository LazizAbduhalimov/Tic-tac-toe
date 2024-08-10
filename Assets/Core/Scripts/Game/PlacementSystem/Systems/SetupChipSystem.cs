using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class SetupChipSystem : IEcsRunSystem
    {
        private EcsCustomInject<EventsBus> _bus;
        private EcsCustomInject<Map> _map;

        private EcsFilterInject<Inc<CMarkPool>> _cMarkFilter;
        private EcsPool<ESetup> _eSetupPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eSetupPool))
            {
                ref var setup = ref _eSetupPool.Get(entity);
                SetupObject(setup);
            }
        }

        private void SetupObject(in ESetup setup)
        {
            var go = setup.Mark;
            var position = setup.Position;
            if (_cMarkFilter.TryGetFirst(out var marksPools))
            {
                switch (go.Type)
                {
                    case Marks.X:
                        marksPools.XPool.GetFromPool(position);
                        break;
                    case Marks.O:
                        marksPools.OPool.GetFromPool(position);
                        break;
                }
            }
            var cell = _map.Value.CreateCell(Vector3Int.RoundToInt(position), go);
            _bus.Value.NewEvent(new EOnMarkSetup(cell, go.Type));
        }
    }
}