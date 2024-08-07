using System;
using System.Text;

namespace Benchmarker
{
    public class BenchmarkResult
    { 
        public long[] MilliSeconds { get; internal set; } = Array.Empty<long>();
        public double Average { get; internal set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var idx = 1;
            foreach (var item in MilliSeconds)
            {
                sb.AppendLine($"Trial {idx}: {item} ms");
                idx++;
            }
            sb.AppendLine($"Average: {Average} ms");
            return sb.ToString();
        }

    }
}
