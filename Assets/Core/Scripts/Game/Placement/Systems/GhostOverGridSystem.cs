using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using LGrid;
using UnityEngine;

namespace Game
{
    public class GhostOverGridSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<CGrid>> _cGrid;
        private EcsFilterInject<Inc<CMousePosition>> _cMousePosition;
        private EcsFilterInject<Inc<CGhost>> _cGhostOverGrid;

        private EcsPoolInject<CGhost> _cGhostOverGridPool;
        private EcsPoolInject<CMarks> _cMarksPool;

        public void Init(IEcsSystems systems)
        {
            var marksMb = Object.FindObjectOfType<MarksMb>();
            var placementMb = Object.FindObjectOfType<GhostMarksMb>();
            _cMarksPool.NewEntity(out _).Invoke(marksMb.X, marksMb.O);
            _cGhostOverGridPool.NewEntity(out _)
                .Invoke(placementMb.OPrefab, placementMb.XPrefab);
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _cGhostOverGrid.Value)
            {
                ref var component = ref _cGhostOverGrid.Pools.Inc1.Get(entity);
                MoveView(in component);
            }
        }

        private void MoveView(in CGhost view)
        {
            foreach (var entity in _cMousePosition.Value)
            {
                var position = _cMousePosition.Pools.Inc1.Get(entity).Position;
                foreach (var gridEntity in _cGrid.Value)
                {
                    var grid = _cGrid.Pools.Inc1.Get(gridEntity).Grid;
                    var gridPosition = grid.WorldToCell(position);
                    var cellPosition = grid.CellToWorld(gridPosition);
                    view.XPrefab.transform.position = cellPosition;
                    view.OPrefab.transform.position = cellPosition;
                }
            }
        }
    }
}