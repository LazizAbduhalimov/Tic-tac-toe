using LGrid;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public struct EOnMarkSetup : IEventReplicant
    {
        public Cell Cell;
        public Marks MarkType;
        public GameObject Mark;

        public EOnMarkSetup(Cell cell, Marks markType, GameObject mark)
        {
            Cell = cell;
            MarkType = markType;
            Mark = mark;
        }
    }
}