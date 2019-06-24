using System;

namespace GeometryTasks
{
	class Vector
	{
        public double X;
        public double Y;
		
		public double GetLength()
		{
			return Geometry.GetLength(this);
		}
		
		public Vector Add(Vector vector)
		{
			return Geometry.Add(this, vector);
		}
		
		public bool Belongs(Segment segment)
		{
			return Geometry.IsVectorInSegment(this, segment);
		}
    }

    class Segment
    {
        public Vector Begin;
        public Vector End;
		
		public double GetLength()
		{
			return Geometry.GetLength(this);
		}
		
		public bool Contains(Vector vector)
		{
			return Geometry.IsVectorInSegment(vector, this);
		}
    }

    class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Segment segment)
        {            
            return GetLength(new Vector() { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y });
        }

        public static Vector Add(Vector firstVector, Vector secondVector)
        {
            return new Vector()
            {
                X = firstVector.X + secondVector.X,
                Y = firstVector.Y + secondVector.Y
            };
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            Vector segmentVector = new Vector() { X = segment.End.X - segment.Begin.X, 
												  Y = segment.End.Y - segment.Begin.Y };
            Vector givenDotVector = new Vector() { X = vector.X - segment.Begin.X, 
												   Y = vector.Y - segment.Begin.Y };
            bool vectorProductIsNull = givenDotVector.X * segmentVector.Y - 
									   givenDotVector.Y * segmentVector.X == 0;
            bool isDotBetweenSegmentCoordinates = 
				IsDotWithinSegment(vector.X, segment.Begin.X, segment.End.X) &&					                           IsDotWithinSegment(vector.Y, segment.Begin.Y, segment.End.Y);
            return isDotBetweenSegmentCoordinates && vectorProductIsNull;
        }

        public static bool IsDotWithinSegment(double x, double x1, double x2)
        {
            return x <= Math.Max(x1, x2) && x >= Math.Min(x1, x2);
        }
    }
}
