using System;
using System.Linq;
using System.Text;

namespace Benchmarker
{
    public class BenchmarkResult
    { 
        public long[] MilliSeconds { get; internal set; } = Array.Empty<long>();
        public long[] MemoryUsage { get; internal set; } = Array.Empty<long>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var idx = 0; idx < MilliSeconds.Length; idx++)
            {
                var ms = MilliSeconds[idx];
                var mem = MemoryUsage[idx];
                sb.AppendLine($"Trial {idx+1}: {ms} ms | {(mem/1048576.0):F2} mb");
            }
            var avgMem = MemoryUsage.Average() / 1048576.0;
            var avgMS = MilliSeconds.Average();
            sb.AppendLine($"Average: {avgMS:F2} ms | {avgMem:F2} mb");
            return sb.ToString();
        }

    }
}
