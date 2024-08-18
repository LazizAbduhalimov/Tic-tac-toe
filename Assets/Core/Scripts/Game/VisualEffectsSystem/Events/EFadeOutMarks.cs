using SevenBoldPencil.EasyEvents;

namespace Game
{
    public struct EFadeOutMarks : IEventReplicant
    {
        public MarkMb[] WinRow;
        public MarkMb[] Others;

        public EFadeOutMarks(MarkMb[] winRow, MarkMb[] others)
        {
            WinRow = winRow;
            Others = others;
        }
    }
}