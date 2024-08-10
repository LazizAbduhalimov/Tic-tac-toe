using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class CheckForWinSystem : IEcsRunSystem
    {
        private EcsCustomInject<Map> _map;
        private EcsCustomInject<EventsBus> _bus;

        private EcsFilterInject<Inc<CGrid>> _cGrids;

        private EcsPool<EOnMarkSetup> _eOnMarkSetupPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _bus.Value.GetEventBodies(out _eOnMarkSetupPool))
            {
                ref var setup = ref _eOnMarkSetupPool.Get(entity);
                var cell = setup.Cell;
                var markType = cell.Mark.Type;
                foreach (var neigh in _map.Value.CellsAround)
                {
                    var position = cell.Position - neigh;
                    if (IsCellMarkTypeAtPositionEqualTo(position, markType))
                    {
                        position = cell.Position - neigh * 2;
                        if (IsCellMarkTypeAtPositionEqualTo(position, markType))
                        {
                            _bus.Value.NewEventSingleton(new EWin(markType));
                            return;
                        }
                        position = cell.Position - -neigh;
                        if (IsCellMarkTypeAtPositionEqualTo(position, markType))
                        {
                            _bus.Value.NewEventSingleton(new EWin(markType));
                            return;
                        }
                    }
                }
            }
        }

        private bool IsCellMarkTypeAtPositionEqualTo(Vector3 position, Marks markType)
        {
            return _map.Value.IsCellExists(position, out var cell) && cell.Mark.Type == markType;
        }
    }
}