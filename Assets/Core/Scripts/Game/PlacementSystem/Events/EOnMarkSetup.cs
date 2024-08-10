using LGrid;
using SevenBoldPencil.EasyEvents;

namespace Game
{
    public struct EOnMarkSetup : IEventReplicant
    {
        public Cell Cell;
        public Marks Mark;

        public EOnMarkSetup(Cell cell, Marks mark)
        {
            Cell = cell;
            Mark = mark;
        }
    }
}