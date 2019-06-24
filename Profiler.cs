using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Profiling
{
	public class ProfilerTask : IProfiler
	{
		public List<ExperimentResult> Measure(IRunner runner, int repetitionsCount)
		{
            var result = new List<ExperimentResult>();
            var classWatch = new Stopwatch();
            var structWatch = new Stopwatch();
            foreach (var size in Constants.FieldCounts)
            {
                runner.Call(true, size, 1);
                runner.Call(false, size, 1);
                classWatch.Restart();
                runner.Call(true, size, repetitionsCount);
                classWatch.Stop();
                structWatch.Restart();
                runner.Call(false, size, repetitionsCount);
                structWatch.Stop();
                var totalClassTime = classWatch.ElapsedMilliseconds / (double)repetitionsCount;
                var totalStructTime = structWatch.ElapsedMilliseconds / (double)repetitionsCount;
                result.Add(new ExperimentResult(size, totalClassTime, totalStructTime));
            }
            return result;
		}
	}
}
