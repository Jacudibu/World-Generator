using UnityEngine;

namespace Utility
{
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public float Distance(Point other)
        {
            float xDistance = X - other.X;
            float yDistance = Y - other.Y;

            return Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
        }
    }
}