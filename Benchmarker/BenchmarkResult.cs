using System.Linq;
using System.Text;

public class BenchmarkResult
{
    public long[] MilliSeconds { get; set; }
    public long[] PrivateMemoryUsage { get; set; }
    public long[] WorkingMemoryUsage { get; set; }
    public long[] VirtualMemoryUsage { get; set; }
    public long[] PeakWorkingMemoryUsage { get; set; }
    public long[] PeakVirtualMemoryUsage { get; set; }

    public override string ToString()
    {
        return ToString("KB");
    }
    public string ToString(string memUnit)
    {
        if (memUnit.ToUpper() != "MB")
            memUnit = "KB";

        var markdown = new StringBuilder();

        // Convert memory units
        double memFactor = GetMemoryConversionFactor(memUnit);

        // Summary
        markdown.AppendLine("## Summary");
        markdown.AppendLine("|--------------------------------|----------------------|");
        markdown.AppendLine(string.Format("| {0,-30} | {1,20} |", "Metric", "Value"));
        markdown.AppendLine("|--------------------------------|----------------------|");
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} ms |", "Average Execution Time", MilliSeconds.Average()));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Peak Working Memory Usage(Avg)", ConvertMemory(PeakWorkingMemoryUsage.Average(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Peak Working Memory Usage(Max)", ConvertMemory(PeakWorkingMemoryUsage.Max(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Peak Virtual Memory Usage(Avg)", ConvertMemory(PeakVirtualMemoryUsage.Average(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Peak Virtual Memory Usage(Max)", ConvertMemory(PeakVirtualMemoryUsage.Max(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Average Private Memory", ConvertMemory(PrivateMemoryUsage.Average(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Average Working Memory", ConvertMemory(WorkingMemoryUsage.Average(), memFactor), memUnit));
        markdown.AppendLine(string.Format("| {0,-30} | {1,17:F2} {2} |", "Average Virtual Memory", ConvertMemory(VirtualMemoryUsage.Average(), memFactor), memUnit));
        markdown.AppendLine("|--------------------------------|----------------------|");


        // Detailed attempts
        markdown.AppendLine("\n## Detailed Attempts");
        markdown.AppendLine("|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|");
        markdown.AppendLine(string.Format("| {0,-7} | {1,19} | {2,22} | {3,22} | {4,22} | {5,26} | {6,26} |", "Attempt", "Execution Time (ms)", "Private Memory (" + memUnit + ")", "Working Memory (" + memUnit + ")", "Virtual Memory (" + memUnit + ")", "Peak Virtual Memory (" + memUnit + ")", "Peak Working Memory (" + memUnit + ")"));
        markdown.AppendLine("|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|");

        for (int i = 0; i < MilliSeconds.Length; i++)
        {
            markdown.AppendLine(string.Format("| {0,-7} | {1,19} | {2,22:F2} | {3,22:F2} | {4,22:F2} | {5,26:F2} | {6,26:F2} |",
                i + 1,
                MilliSeconds[i],
                ConvertMemory(PrivateMemoryUsage[i], memFactor),
                ConvertMemory(WorkingMemoryUsage[i], memFactor),
                ConvertMemory(VirtualMemoryUsage[i], memFactor),
                ConvertMemory(PeakVirtualMemoryUsage[i], memFactor),
                ConvertMemory(PeakWorkingMemoryUsage[i], memFactor)
                ));
        }
        markdown.AppendLine("|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|");

        return markdown.ToString();
    }

    private double GetMemoryConversionFactor(string memoryUnit)
    {
        switch (memoryUnit.ToUpper())
        {
            case "MB":
                return 1024 * 1024;
            case "KB":
            default:
                return 1024;
        }
    }

    private double ConvertMemory(double memory, double conversionFactor)
    {
        return memory / conversionFactor;
    }
}
