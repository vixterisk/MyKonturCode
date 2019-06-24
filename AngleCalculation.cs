using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            var triangleExists = a + b >= c && a + c >= b && c + b >= a;
            if (triangleExists)
                return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            return Double.NaN;
        }
    }
	
    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(0, 0, 0, Double.NaN)] //Вырожденный, несуществующий, все стороны 0
        [TestCase(0, 1, 2, Double.NaN)] //Вырожденный, несуществующий, одна сторона = 0
        [TestCase(0, 1, 0, Double.NaN)] //Вырожденный, несуществующий, две стороны, одна из которых с = 0
        [TestCase(1, 1, 0, 0)] //Вырожденный, существующий
        [TestCase(-1, -1, -1, Double.NaN)] // Все отрицательные
        [TestCase(-1, -1, 1, Double.NaN)] // Две стороны отрицательные
        [TestCase(-1, 1, 1, Double.NaN)] // Одна из сторон отрицательная
        [TestCase(3, 4, 5, Math.PI / 2)] // обычный с прямым углом
        [TestCase(1, 1, 1, Math.PI / 3)] // обычный с острым углом
        [TestCase(2, 2, 2 * 1.73205080757, 2 * Math.PI / 3)] // обычный с тупым углом

        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var actualAngle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, actualAngle, 1e-5);
        }
    }
}
