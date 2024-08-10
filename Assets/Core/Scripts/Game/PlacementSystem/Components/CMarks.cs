namespace Game
{
    public struct CMarks
    {
        public MarkMb X;
        public MarkMb O;

        public void Invoke(MarkMb x, MarkMb o)
        {
            X = x;
            O = o;
        }
    }
}