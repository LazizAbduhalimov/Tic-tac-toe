using Game;
using UnityEngine;

namespace LGrid
{
    public struct CCell
    {
        public Vector3 Position;
        public MarkMb MarkObject;

        public CCell Invoke(Vector3 position, MarkMb markObject)
        {
            Position = position;
            MarkObject = markObject;
            return this;
        }
    }
}