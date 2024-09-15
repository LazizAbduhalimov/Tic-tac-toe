using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace Game
{
    public struct ESetup : IEventReplicant
    {
        public MarkMb Mark;
        public Vector3 Position;

        public ESetup(MarkMb mark, Vector3 position)
        {
            Mark = mark;
            Position = position;
        }
    }
}