using Game;
using UnityEngine;

namespace LGrid
{
    public class Cell
    {
        public MarkMb Mark;
        public Vector3Int Position;

        public Cell(Vector3Int position)
        {
            Position = position;
        }
        
        public Cell(MarkMb mark, Vector3Int position)
        {
            Mark = mark;
            Position = position;
        }
    }
}