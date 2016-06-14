using System;

namespace HideAndSeek.Model
{
    public class Board
    {
        private readonly IGameImplementation _gameImplementation;
        public Piece[,] Pieces { get; set; }
        public Tuple<int, int> PlayerCurrentPiece { get; set; }
        public Tuple<int, int> Culprit { get; set; }

        public Board() : this(new GameImplementation()) { }

        public Board(IGameImplementation gameImplementation)
        {
            _gameImplementation = gameImplementation;

            PlayerCurrentPiece = new Tuple<int, int>(0, 0);

            var rnd = new Random(DateTime.Now.Millisecond);
            Culprit = new Tuple<int, int>(rnd.Next(0, 3), rnd.Next(0, 3));

            Pieces = new[,] {
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() },
                { new Piece(), new Piece(), new Piece() }
            };
        }

        public void MovePlayer(Orientation orientation)
        {
            var playersPosition = Pieces[PlayerCurrentPiece.Item1, PlayerCurrentPiece.Item2];

            if (playersPosition.Wall == orientation) return;
            if (orientation == Orientation.North && PlayerCurrentPiece.Item1 > 0) PlayerCurrentPiece = new Tuple<int, int>(PlayerCurrentPiece.Item1 - 1, PlayerCurrentPiece.Item2);
            if (orientation == Orientation.South && PlayerCurrentPiece.Item1 < 2) PlayerCurrentPiece = new Tuple<int, int>(PlayerCurrentPiece.Item1 + 1, PlayerCurrentPiece.Item2);
            if (orientation == Orientation.East && PlayerCurrentPiece.Item2 < 2) PlayerCurrentPiece = new Tuple<int, int>(PlayerCurrentPiece.Item1, PlayerCurrentPiece.Item2 + 1);
            if (orientation == Orientation.West && PlayerCurrentPiece.Item2 > 0) PlayerCurrentPiece = new Tuple<int, int>(PlayerCurrentPiece.Item1, PlayerCurrentPiece.Item2 - 1);

        }


        public void Rotate(Direction direction, Tuple<int, int> pieceLocation)
        {
            var piece = Pieces[pieceLocation.Item1, pieceLocation.Item2];

            if (direction == Direction.ClockWise)
            {
                switch (piece.Wall)
                {
                    case Orientation.North:
                        piece.Wall = Orientation.East;
                        break;
                    case Orientation.East:
                        piece.Wall = Orientation.South;
                        break;
                    case Orientation.South:
                        piece.Wall = Orientation.West;
                        break;
                    case Orientation.West:
                        piece.Wall = Orientation.North;
                        break;
                }
            }

            if (direction == Direction.AntiClockWise)
            {
                switch (piece.Wall)
                {
                    case Orientation.North:
                        piece.Wall = Orientation.West;
                        break;
                    case Orientation.West:
                        piece.Wall = Orientation.South;
                        break;
                    case Orientation.South:
                        piece.Wall = Orientation.East;
                        break;
                    case Orientation.East:
                        piece.Wall = Orientation.North;
                        break;
                }
            }
        }

        public bool IsGameOver()
        {
            return _gameImplementation.IsGameOver(this);
        }

        public bool MovePlayerUntilFind()
        {
            int movePlayerSouthDirection = 1;

            for (int i = 0; i < Pieces.Length; i++)
            {
                if ((i != 0) && (i == 2 || i == 4))
                {

                    Tuple<int, int> currentPosition = PlayerCurrentPiece;

                    int y = currentPosition.Item1;
                    int x = currentPosition.Item2;
                    x = x + 1;
                    Tuple<int, int> nextPosition = new Tuple<int, int>(y, x);

                    if (Pieces[currentPosition.Item1, currentPosition.Item2].Wall == Orientation.East)
                        Rotate(Direction.ClockWise, currentPosition);

                    if (Pieces[nextPosition.Item1, nextPosition.Item2].Wall == Orientation.West)
                        Rotate(Direction.ClockWise, nextPosition);

                    MovePlayer(Orientation.East);

                    movePlayerSouthDirection = movePlayerSouthDirection * -1;
                }

                if (movePlayerSouthDirection > 0)
                {
                    Tuple<int, int> currentPosition = PlayerCurrentPiece;

                    int y = currentPosition.Item1;
                    int x = currentPosition.Item2;
                    y = y + movePlayerSouthDirection;
                    Tuple<int, int> nextPosition = new Tuple<int, int>(y, x);

                    if (Pieces[currentPosition.Item1, currentPosition.Item2].Wall == Orientation.South)
                        Rotate(Direction.ClockWise, currentPosition);

                    if (Pieces[nextPosition.Item1, nextPosition.Item2].Wall == Orientation.North)
                        Rotate(Direction.ClockWise, nextPosition);

                    MovePlayer(Orientation.South);


                }
                else
                {
                    Tuple<int, int> currentPosition = PlayerCurrentPiece;

                    int y = currentPosition.Item1;
                    int x = currentPosition.Item2;
                    y = y + movePlayerSouthDirection;
                    Tuple<int, int> nextPosition = new Tuple<int, int>(y, x);

                    if (Pieces[currentPosition.Item1, currentPosition.Item2].Wall == Orientation.North)
                        Rotate(Direction.ClockWise, currentPosition);

                    if (Pieces[nextPosition.Item1, nextPosition.Item2].Wall == Orientation.South)
                        Rotate(Direction.ClockWise, nextPosition);

                    MovePlayer(Orientation.North);
                }
                if (IsGameOver())
                    return true;
            }
            return false;
        }
    }
}