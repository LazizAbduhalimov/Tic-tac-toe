using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace LGrid
{
    public class GridInitSystem : IEcsInitSystem
    {
        private EcsPoolInject<CGrid> _gridPool;
        
        public void Init(IEcsSystems systems)
        {
            var gridMb = Object.FindObjectOfType<GridMB>();
            _gridPool.NewEntity(out _).Invoke(gridMb.Grid);
            gridMb.GridPlane.localScale = Vector3.one / 10 * 3; 
        }
    }
}