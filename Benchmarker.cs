using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Benchmarker
{
    public class Benchmarker
    {
        public static BenchmarkResult CollectResult<T>(int attemptCount, Func<T> work)
        {
            var stopWatch = new Stopwatch();
            var timesheet = new List<long>();
            for (var i = 0; i < attemptCount; i++)
            { 
                stopWatch.Restart();
                _ = work();
                stopWatch.Stop();
                timesheet.Add(stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
            }

            return new BenchmarkResult() 
            { 
                MilliSeconds = timesheet.ToArray(),
                Average = timesheet.Average()
            };
        }

        public static BenchmarkResult CollectResult(int attemptCount, Action work)
        {
            var stopWatch = new Stopwatch();
            var timesheet = new List<long>();
            for (var i = 0; i < attemptCount; i++)
            {
                stopWatch.Restart();
                work();
                stopWatch.Stop();
                timesheet.Add(stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
            }

            return new BenchmarkResult()
            {
                MilliSeconds = timesheet.ToArray(),
                Average = timesheet.Average()
            };
        }
    }
}
