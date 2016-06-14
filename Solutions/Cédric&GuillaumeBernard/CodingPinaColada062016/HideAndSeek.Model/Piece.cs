namespace HideAndSeek.Model
{
    public class Piece
    {
        public Piece() : this(new OrientationGenerator()) { }

        public Piece(IOrientationGenerator orientationGenerator)
        {
            Wall = orientationGenerator.GetOrientation();
        }

        public Piece(Orientation orientation)
        {
            Wall = orientation;
        }

        public Orientation Wall { get; set; }
    }
}