namespace HideAndSeek.Model
{
    public class GameImplementation : IGameImplementation
    {
        public bool IsGameOver(Board board)
        {
            return board.PlayerCurrentPiece.Item1 == board.Culprit.Item1 &&
                   board.PlayerCurrentPiece.Item2 == board.Culprit.Item2;
        }
    }
}