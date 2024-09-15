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
                    if (IsCellMarkTypeAtPositionEqualTo(position, markType, out var cell2))
                    {
                        position = cell.Position - neigh * 2;
                        if (IsCellMarkTypeAtPositionEqualTo(position, markType, out var cell3))
                        {
                            MarkMb[] marks = {cell.Mark, cell2.Mark, cell3.Mark};
                            _bus.Value.NewEventSingleton(new EWin(markType, marks));
                            return;
                        }
                        position = cell.Position - -neigh;
                        if (IsCellMarkTypeAtPositionEqualTo(position, markType, out cell3))
                        {
                            MarkMb[] marks = {cell.Mark, cell2.Mark, cell3.Mark};
                            _bus.Value.NewEventSingleton(new EWin(markType, marks));
                            return;
                        }
                    }
                }
            }
        }

        private bool IsCellMarkTypeAtPositionEqualTo(Vector3 position, Marks markType, out Cell cell)
        {
            return _map.Value.IsCellExists(position, out cell) && cell.Mark.Type == markType;
        }
    }
}