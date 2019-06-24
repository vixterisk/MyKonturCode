using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if (FindCosSignBetweenVectors(x - ax, y - ay, bx - ax, by - ay) >= 0
             && FindCosSignBetweenVectors(x - bx, y - by, ax - bx, ay - by) >= 0)
                return GetDistanceFromDotToLine(x, y, ax, ay, bx, by);
            return Math.Min(FindVectorLength(x - ax, y - ay), FindVectorLength(x - bx, y - by));
        }

        public static double FindCosSignBetweenVectors(double x1, double y1, double x2, double y2)
        {
            return x1 * x2 + y1 * y2;
        }

        public static double GetDistanceFromDotToLine(double x, double y, double x1, double y1, double x2, double y2)
        {
            double A = y1 - y2;
            double B = x2 - x1;
            if (A == 0 && B == 0) return Math.Min(FindVectorLength(x - x1, y - y1), FindVectorLength(x - x2, y - y2));
            double C = x1 * (y2 - y1) + y1 * (x1 - x2);
            return Math.Abs(A*x + B*y + C) / Math.Sqrt(A*A+B*B);
        }

        public static double FindVectorLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
