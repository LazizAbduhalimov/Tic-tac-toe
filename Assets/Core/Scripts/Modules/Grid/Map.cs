using System.Collections.Generic;
using Game;
using UnityEngine;

namespace LGrid
{
    public class Map
    {
        public Dictionary<Vector3Int, Cell> Cells = new();
        public Vector3Int[] CellsAround =
        {
            new (-1, 0, -1),
            new (0, 0, -1),
            new (1, 0, -1),
            new (-1, 0, 0),
            new (1, 0, 0),
            new (-1, 0, 1),
            new (0, 0, 1),
            new (1, 0, 1),
        };

        public Cell CreateCell(Vector3Int position)
        {
            if (IsCellExists(position, out var cell)) return cell;

            var newCell = new Cell(position);
            Cells.Add(position, newCell);
            return newCell;
        }
        
        public Cell CreateCell(Vector3Int position, MarkMb markMb)
        {
            var cell = CreateCell(position);
            cell.Mark = markMb;
            return cell;
        }
        
        public bool IsCellExists(Vector3Int position, out Cell cell)
        {
            cell = null;
            return Cells.TryGetValue(position, out cell);
        }
        
        public bool IsCellExists(Vector3 position, out Cell cell)
        {
            cell = null;
            return Cells.TryGetValue(Vector3Int.RoundToInt(position), out cell);
        }
        
        public void RemoveCell(Vector3 position)
        {
            var positionInt = Vector3Int.RoundToInt(position);
            if (Cells.ContainsKey(positionInt))
            {
                Cells.Remove(positionInt);
            }
        }

        public void Clear()
        {
            Cells.Clear();
        }
    }
}