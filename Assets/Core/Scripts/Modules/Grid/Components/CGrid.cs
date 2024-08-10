using UnityEngine;

namespace LGrid
{
    public struct CGrid
    {
        public Grid Grid;

        public void Invoke(Grid grid)
        {
            Grid = grid;
        }
    }
}