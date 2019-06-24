using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF TransferJoint(PointF startPoint, PointF pointToTransfer)
        {
            return new PointF(pointToTransfer.X + startPoint.X, pointToTransfer.Y + startPoint.Y);
        }

        public static PointF GetCoordinatesOfAJoint(double jointLength, double jointSurfaceAngle)
        {
            var x = Math.Cos(jointSurfaceAngle) * jointLength;
            var y = Math.Sin(jointSurfaceAngle) * jointLength;
            return new PointF((float) x, (float) y);
        }

        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = GetCoordinatesOfAJoint(Manipulator.UpperArm, shoulder);
            var wristPos = GetCoordinatesOfAJoint(Manipulator.Forearm, (shoulder + Math.PI + elbow));
            wristPos = TransferJoint(elbowPos, wristPos);
            var palmEndPos = GetCoordinatesOfAJoint(Manipulator.Palm, shoulder + Math.PI + elbow + Math.PI + wrist);
            palmEndPos = TransferJoint(wristPos, palmEndPos);
            return new PointF[] {elbowPos, wristPos, palmEndPos};
        }
    }
	
	[TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {		
		const double cos45 = 0.70710678118654757;
		const double sin45 = 0.70710678118654757;
		// C наложениями
		[TestCase(0, 0, 0, Manipulator.UpperArm - Manipulator.Forearm + Manipulator.Palm, 0)]
		//Два сегмента накладываются
		[TestCase(0, Math.PI, 0, Manipulator.UpperArm + Manipulator.Forearm - Manipulator.Palm, 0)]
		// Все сегменты лежат горизонтально без наложений
		[TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]
		// Все сегменты стоят вертикально без наложений
		[TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
		// "Лесница" вправо 
		[TestCase(Math.PI / 2, Math.PI / 2, 3* Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm + Manipulator.Palm)]
		// Отрицательные углы 
        [TestCase(- Math.PI / 4, - Math.PI / 4, Math.PI, 106.066017178, Manipulator.Forearm + Manipulator.Palm - 106.066017178)]
		[TestCase(- 3* Math.PI / 2, 0, 3 * Math.PI / 2, Manipulator.Palm, Manipulator.UpperArm - Manipulator.Forearm)]
		//Все сегменты в первой четверти
		[TestCase(Math.PI / 4, Math.PI, Math.PI, (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * cos45, (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * sin45)]
		//Все сегменты в второй четверти
		[TestCase(3 * Math.PI / 4, Math.PI, Math.PI, (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * -cos45, (Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm) * sin45)]
		//Все сегменты в третьей четверти
		[TestCase( - 3 * Math.PI / 4,Math.PI, Math.PI, (Manipulator.Palm + Manipulator.UpperArm + Manipulator.Forearm) * -cos45, (Manipulator.Palm + Manipulator.UpperArm + Manipulator.Forearm) * -sin45)]
		//Все сегменты в четвертой четверти
		[TestCase( - Math.PI / 4,  Math.PI, Math.PI, (Manipulator.Palm + Manipulator.UpperArm + Manipulator.Forearm) * cos45, (Manipulator.Palm + Manipulator.UpperArm + Manipulator.Forearm) * -sin45)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            var expectedUpperarmLength = Math.Sqrt(joints[0].X * joints[0].X + joints[0].Y * joints[0].Y);
            Assert.AreEqual(Manipulator.UpperArm, expectedUpperarmLength, 1e-5, "UpperArm Length");
            var expectedForearmLength = Math.Sqrt((joints[1].X - joints[0].X) * (joints[1].X - joints[0].X)
                                                + (joints[1].Y - joints[0].Y) * (joints[1].Y - joints[0].Y));
            Assert.AreEqual(Manipulator.Forearm, expectedForearmLength, 1e-4, "Forearm Length");
            var expectedPalmLength = Math.Sqrt((joints[2].X - joints[1].X) * (joints[2].X - joints[1].X)
                                             + (joints[2].Y - joints[1].Y) * (joints[2].Y - joints[1].Y));
            Assert.AreEqual(Manipulator.Palm, expectedPalmLength, 1e-4, "Palm Length");
        }
    }
}
