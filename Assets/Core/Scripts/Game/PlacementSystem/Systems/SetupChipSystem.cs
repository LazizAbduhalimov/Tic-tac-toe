using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using PoolSystem.Alternative;
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
            GameObject gameObject = null;
            MarkMb mark = null;
            var go = setup.Mark;
            var position = setup.Position;
            if (_cMarkFilter.TryGetFirst(out var marksPools))
            {
                gameObject = go.Type switch
                {
                    Marks.X => marksPools.XPool.GetFromPool(position).gameObject,
                    Marks.O => marksPools.OPool.GetFromPool(position).gameObject,
                    _ => null
                };
                mark = gameObject.GetComponent<MarkMb>();
            }
            var cell = _map.Value.CreateCell(Vector3Int.RoundToInt(position), mark);
            _bus.Value.NewEvent(new EOnMarkSetup(cell, go.Type, gameObject));
        }
    }
}