using System;
namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            return IntersectionSquare(r1, r2) != 0 
                || AreSidesTouching(r1.Left, r2.Left, r1.Width, r2.Width)
                && AreSidesTouching(r1.Top, r2.Top, r1.Height, r2.Height);
        }

        public static bool AreSidesTouching(int dot1, int dot2, int dot1Length, int dot2Length)
        {
            return DifferenceBetweenMinAndMax(dot1, dot2, dot1Length, dot2Length) >= 0;
        }
		
		public static int DifferenceBetweenMinAndMax(int dot1, int dot2, int dot1Length, int dot2Length)
		{
			return Math.Min(dot1 + dot1Length, dot2 + dot2Length) - Math.Max(dot1, dot2);
		}

        public static int CalculateOneSide(int dot1, int dot2, int dot1Length, int dot2Length)
        {
            if (AreSidesTouching(dot1, dot2, dot1Length, dot2Length))
                return DifferenceBetweenMinAndMax(dot1, dot2, dot1Length, dot2Length);
            else return 0;
        }
		
        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            return CalculateOneSide(r1.Left, r2.Left, r1.Width, r2.Width) * 
				   CalculateOneSide(r1.Top, r2.Top, r1.Height, r2.Height);
        }

        // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
        // Иначе вернуть -1
        // Если прямоугольники совпадают, можно вернуть номер любого из них.
        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (AreIntersected(r1, r2))
            {
                if (IsRectangleFullyIn(r1, r2))
                    return 0;
                if (IsRectangleFullyIn(r2, r1))
                    return 1;
            }
            return -1;
        }

        public static bool IsRectangleFullyIn(Rectangle r1, Rectangle r2)
        {
            return IsSideFullyIn(r1.Left, r2.Left, r1.Width, r2.Width) 
				&& IsSideFullyIn(r1.Top, r2.Top, r1.Height, r2.Height);
        }

        public static bool IsSideFullyIn(int dot1, int dot2, int dot1Length, int dot2Length)
        {
            return (dot1 >= dot2) && (dot1 + dot1Length <= dot2 + dot2Length);
        }
    }
}
