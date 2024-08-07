using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Benchmarker
{
    public class Benchmarker
    {
        public static BenchmarkResult CollectResult<T>(int attemptCount, Func<T> work)
        {
            var stopWatch = new Stopwatch();
            var timesheet = new List<long>();
            var memorySheet = new List<long>();
            for (var i = 0; i < attemptCount; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                long initialMemory = GetTotalMemory();
                
                stopWatch.Restart();
                var returnVal = work();
                stopWatch.Stop();
               
                long endMemory = GetTotalMemory();
                memorySheet.Add(endMemory - initialMemory);
                timesheet.Add(stopWatch.ElapsedMilliseconds);

                if(returnVal is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                stopWatch.Reset();
            }

            return new BenchmarkResult() 
            { 
                MilliSeconds = timesheet.ToArray(),
                MemoryUsage = memorySheet.ToArray()
            };
        }

        public static BenchmarkResult CollectResult(int attemptCount, Action work)
        {
            var stopWatch = new Stopwatch();
            var timesheet = new List<long>();
            var memorySheet = new List<long>();

            for (var i = 0; i < attemptCount; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                long initialMemory = GetTotalMemory();

                stopWatch.Restart();
                work();
                stopWatch.Stop();

                long endMemory = GetTotalMemory();
                memorySheet.Add(endMemory - initialMemory);

                timesheet.Add(stopWatch.ElapsedMilliseconds);
                stopWatch.Reset();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            return new BenchmarkResult()
            {
                MilliSeconds = timesheet.ToArray(),
                MemoryUsage = memorySheet.ToArray()
            };
        }

        private static long GetTotalMemory()
        {
            using (var  process = Process.GetCurrentProcess())
            {
                return process.WorkingSet64;
            }
        }
    }
}
