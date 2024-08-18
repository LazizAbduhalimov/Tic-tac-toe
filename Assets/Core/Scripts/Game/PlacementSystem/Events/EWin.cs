using SevenBoldPencil.EasyEvents;

namespace Game
{
    public struct EWin : IEventSingleton
    {
        public Marks WonMark;
        public MarkMb[] MarkRow;

        public EWin(Marks wonMark, MarkMb[] markRow)
        {
            WonMark = wonMark;
            MarkRow = markRow;
        }
    }
}