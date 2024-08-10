using SevenBoldPencil.EasyEvents;

namespace Game
{
    public struct EWin : IEventSingleton
    {
        public Marks WonMark;

        public EWin(Marks wonMark)
        {
            WonMark = wonMark;
        }
    }
}