using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            if (x * x + y * y <= Math.Pow(Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 2))
            {
                var wristPos = GetWristPos(x, y, angle);
				var lineConnectingUpperarmForearm = GetVectorLength(wristPos[0], wristPos[1]);
                var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, 
													Manipulator.Forearm, 
													lineConnectingUpperarmForearm);
                if (elbow == Double.NaN)
                    return new[] { double.NaN, double.NaN, double.NaN };
                var shoulderAnglePart1 = TriangleTask.GetABAngle(lineConnectingUpperarmForearm,
																 Manipulator.UpperArm, 
																 Manipulator.Forearm);
                var shoulderAnglePart2 = Math.Atan2(wristPos[1], wristPos[0]);
                if (shoulderAnglePart1 == Double.NaN || shoulderAnglePart2 == Double.NaN)
                    return new[] { double.NaN, double.NaN, double.NaN };
                var shoulder = shoulderAnglePart1 + shoulderAnglePart2;
                var wrist = - angle - shoulder - elbow;
                return new[] {shoulder, elbow, wrist};
            }
            return new[] { double.NaN, double.NaN, double.NaN };
        }

        public static double[] GetWristPos(double x, double y, double angle)
        {
            var deltaX = Manipulator.Palm * Math.Cos(angle);
            var deltaY = Manipulator.Palm * Math.Sin(angle);
            return new double[] { x - deltaX, y + deltaY };
        }

        public static double GetVectorLength(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            Random rand = new Random();
            double x, y, angle;
            x = Convert.ToDouble(rand.Next(1234567890)) / 300;
            y = Convert.ToDouble(rand.Next(1234567890)) / 400;
            angle = Convert.ToDouble(rand.Next(1234567890)) / 500;
            var joints = ManipulatorTask.MoveManipulatorTo(x, y, angle);
            var actual = AnglesToCoordinatesTask.GetJointPositions(joints[0], joints[1], joints[2])[2];
            if (x * x + y * y <= Math.Pow(Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 2))
            {
                Assert.AreEqual(x, actual.X, 1e-5);
                Assert.AreEqual(y, actual.Y, 1e-5);
            }
            else
            {
                Assert.AreEqual(Double.NaN, joints[0]);
                Assert.AreEqual(Double.NaN, joints[1]);
                Assert.AreEqual(Double.NaN, joints[2]);
            }
        }
    }
}
