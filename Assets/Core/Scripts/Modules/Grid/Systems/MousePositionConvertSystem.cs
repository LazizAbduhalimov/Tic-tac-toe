using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace LGrid
{
    public class MousePositionConvertSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPoolInject<CMousePosition> _mousePositionPool;

        private Camera _camera;
        private LayerMask _layerMask;
        private Vector3 _lastPosition;
        
        public void Init(IEcsSystems systems)
        {
            var view = Object.FindObjectOfType<GridMB>();
            _camera = view.Camera;
            _layerMask = view.RayCastLayerMask;
        }
        
        public void Run(IEcsSystems systems)
        {
            var rayHitPosition = GetRayPointFromScreenToMouse(_layerMask);
            _mousePositionPool.NewEntity(out _).Position = rayHitPosition;
        }

        private Vector3 GetRayPointFromScreenToMouse(LayerMask rayCastLayerMask)
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, 100, rayCastLayerMask))
            {
                _lastPosition = hit.point;
            }
            return _lastPosition;
        }
    }
}