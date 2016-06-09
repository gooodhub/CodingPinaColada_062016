using System;

namespace HideAndSeek.Model
{
    public class OrientationGenerator : IOrientationGenerator
    {
        public Orientation GetOrientation()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return (Orientation)rnd.Next(0, 3);
        }
    }
}