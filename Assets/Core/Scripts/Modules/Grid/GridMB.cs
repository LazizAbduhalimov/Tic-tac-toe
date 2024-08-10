using UnityEngine;
using UnityEngine.Serialization;

namespace LGrid
{
    public class GridMB : MonoBehaviour
    {
        public Camera Camera;
        public LayerMask RayCastLayerMask;
        public Grid Grid;
        public Transform GridPlane;
    }
}