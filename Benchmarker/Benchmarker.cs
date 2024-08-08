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
            return CollectResultInternal(attemptCount, work, shouldDispose: true);
        }

        public static BenchmarkResult CollectResult(int attemptCount, Action work)
        {
            return CollectResultInternal(attemptCount, () =>
            {
                work();
                return (object)null;
            }, shouldDispose: false);
        }

        private static BenchmarkResult CollectResultInternal<T>(int attemptCount, Func<T> work, bool shouldDispose)
        {
            var stopWatch = new Stopwatch();
            var timesheet = new List<long>();
            var privateMemorySheet = new List<long>();
            var workingMemorySheet = new List<long>();
            var virtualMemorySheet = new List<long>();
            var peakWorkingMemorySheet = new List<long>();
            var peakVirtualMemorySheet = new List<long>();

            for (var i = 0; i < attemptCount; i++)
            {
                Cleanup();

                long initialPrivateMemory = GetPrivateMemorySize();
                long initialWorkingMemory = GetWorkingMemorySize();
                long initialVirtualMemory = GetVirtualMemorySize();

                stopWatch.Start();
                T returnVal = work();
                stopWatch.Stop();

                long endPrivateMemory = GetPrivateMemorySize();
                long endWorkingMemory = GetWorkingMemorySize();
                long endVirtualMemory = GetVirtualMemorySize();
                long peakWorkingMemory = GetPeakWorkingMemorySize();
                long peakVirtualMemory = GetPeakVirtualMemorySize();

                privateMemorySheet.Add(endPrivateMemory - initialPrivateMemory);
                workingMemorySheet.Add(endWorkingMemory - initialWorkingMemory);
                virtualMemorySheet.Add(endVirtualMemory - initialVirtualMemory);
                peakWorkingMemorySheet.Add(peakWorkingMemory);
                peakVirtualMemorySheet.Add(peakVirtualMemory);
                timesheet.Add(stopWatch.ElapsedMilliseconds);

                if (shouldDispose)
                {
                    DisposeIfDisposable(returnVal);
                }

                Cleanup();
                stopWatch.Reset();
            }

            return CreateBenchmarkResult(timesheet, privateMemorySheet, workingMemorySheet, virtualMemorySheet, peakWorkingMemorySheet, peakVirtualMemorySheet);
        }

        private static BenchmarkResult CreateBenchmarkResult(List<long> timesheet, List<long> privateMemorySheet, List<long> workingMemorySheet, List<long> virtualMemorySheet, List<long> peakWorkingMemorySheet, List<long> peakVirtualMemorySheet)
        {
            return new BenchmarkResult
            {
                MilliSeconds = timesheet.ToArray(),
                PrivateMemoryUsage = privateMemorySheet.ToArray(),
                WorkingMemoryUsage = workingMemorySheet.ToArray(),
                VirtualMemoryUsage = virtualMemorySheet.ToArray(),
                PeakWorkingMemoryUsage = peakWorkingMemorySheet.ToArray(),
                PeakVirtualMemoryUsage = peakVirtualMemorySheet.ToArray(),
            };
        }

        private static void Cleanup()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private static long GetPrivateMemorySize()
        {
            using (var process = Process.GetCurrentProcess())
            {
                return process.PrivateMemorySize64;
            }
        }

        private static long GetWorkingMemorySize()
        {
            using (var process = Process.GetCurrentProcess())
            {
                return process.WorkingSet64;
            }
        }

        private static long GetVirtualMemorySize()
        {
            using (var process = Process.GetCurrentProcess())
            {
                return process.VirtualMemorySize64;
            }
        }

        private static long GetPeakWorkingMemorySize()
        {
            using (var process = Process.GetCurrentProcess())
            {
                return process.PeakWorkingSet64;
            }
        }

        private static long GetPeakVirtualMemorySize()
        {
            using (var process = Process.GetCurrentProcess())
            {
                return process.PeakVirtualMemorySize64;
            }
        }

        private static void DisposeIfDisposable<T>(T returnVal)
        {
            if (returnVal is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
