using System;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            double x = 1, y = 0;
			Tuple<double, double> dot;
            pixels.SetPixel(x, y);
            var random = new Random(seed);
            for (int i = 0; i < iterationsCount; i++)
            {
                var nextNumber = random.Next(2);
                if (nextNumber % 2 == 0)
					dot = CalculateDot(x, y, FromDegToRad(45), 0);
                else
					dot = CalculateDot(x, y, FromDegToRad(135), 1);
                pixels.SetPixel(dot.Item1, dot.Item2);
                x = dot.Item1;
                y = dot.Item2;
            }
        }

		public static Tuple<double, double> CalculateDot(double x, double y, double rad, int addition)
		{
			double newX = ((Math.Cos(rad)) * x - y * Math.Sin(rad)) / Math.Sqrt(2) + addition;
            double newY = (x * Math.Sin(rad) + Math.Cos(rad) * y) / Math.Sqrt(2);
			return Tuple.Create(newX, newY);
		}
		
        public static double FromDegToRad(double deg)
        {
            return Math.PI * deg / 180;
        }
    }
}
