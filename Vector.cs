using System;

namespace GeometryTasks
{
    class Vector
    {
        public double X;
        public double Y;
    }

    class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            return new Vector() { X = firstVector.X + secondVector.X,
                				  Y = firstVector.Y + secondVector.Y};
        }
    }
}
