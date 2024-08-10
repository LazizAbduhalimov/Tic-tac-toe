using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public class SwitchTurnSystem : IEcsRunSystem
    {
        private EcsCustomInject<Map> _map;
        private EcsCustomInject<EventsBus> _bus;
        
        private EcsFilterInject<Inc<CTurn>> _cTurnFilter;
        private EcsFilterInject<Inc<CMarks>> _cMarksFilter;

        private EcsFilterInject<Inc<CGrid>> _cGrid;
        private EcsFilterInject<Inc<CMousePosition>> _cMousePosition;
        
        private EcsPoolInject<ESetup> _eSetupPool;
        
        public void Run(IEcsSystems systems)
        {
            if (!_bus.Value.HasEventSingleton<ELeftMouseClicked>()) return;
            
            foreach (var entity in _cTurnFilter.Value)
            {
                ref var currentTurn = ref _cTurnFilter.Pools.Inc1.Get(entity);
                foreach (var markEntity in _cMarksFilter.Value)
                {
                    ref var marks = ref _cMarksFilter.Pools.Inc1.Get(markEntity);
                    var position = GetMarkPosition();
                    if (_map.Value.IsCellExists(position, out _)) return;
                    _bus.Value.NewEvent(new EOnTurnSwitched());
                    switch (currentTurn.MarksTurn)
                    {
                        case Marks.X:
                            currentTurn.MarksTurn = Marks.O;
                            CreateMark(marks.X, position);
                            break;
                        case Marks.O:
                            currentTurn.MarksTurn = Marks.X;
                            CreateMark(marks.O, position);
                            break;
                    }
                }
            }    
        }

        private void CreateMark(MarkMb mark, Vector3 position)
        {
            _bus.Value.NewEvent(new ESetup(mark, position));
        }
        
        private Vector3 GetMarkPosition()
        {
            foreach (var entity in _cMousePosition.Value)
            {
                var position = _cMousePosition.Pools.Inc1.Get(entity).Position;
                foreach (var gridEntity in _cGrid.Value)
                {
                    var grid = _cGrid.Pools.Inc1.Get(gridEntity).Grid;
                    var gridPosition = grid.WorldToCell(position);
                    var cellPosition = grid.CellToWorld(gridPosition);
                    return cellPosition;
                }
            }
#if DEBUG
            Debug.LogError("Mouse out of screen or grid not inited");
#endif
            return Vector3.zero;
        }
    }
}