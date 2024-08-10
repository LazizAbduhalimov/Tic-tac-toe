namespace Game
{
    public struct CTurn
    {
        public Marks MarksTurn;

        public void Invoke(Marks marksTurn)
        {
            MarksTurn = marksTurn;
        }
    }
}