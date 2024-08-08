# Benchmarker

The `Benchmarker` class provides a simple yet powerful utility for measuring the execution time of functions and actions in .NET applications. It allows developers to benchmark the performance of specific code segments by repeatedly executing them and collecting timing data.

## Features

- **Benchmarking Functions and Actions**: Supports benchmarking both functions that return a result (`Func<T>`) and actions that do not (`Action`).
- **Multiple Attempts**: Allows specifying the number of times to run the benchmark, providing more accurate and stable performance measurements.
- **Detailed Results**: Collects and returns detailed timing information for each run, along with the average execution time.

## Usage

The `Benchmarker` class includes two static methods, `CollectResult`, which are used to benchmark functions and actions.

### Benchmarking a Function

```csharp
int attemptCount = 100;
Func<int> work = () => {
    // Your code here
    return 42;
};

BenchmarkResult result = Benchmarker.CollectResult(attemptCount, work);
Console.WriteLine(result);
```

### Benchmarking an Action

```csharp
int attemptCount = 100;
Action work = () => {
    // Your code here
};

BenchmarkResult result = Benchmarker.CollectResult(attemptCount, work);
Console.WriteLine(result);
```
## Methods

### `CollectResult<T>(int attemptCount, Func<T> work)`

Benchmarks a function by running it the specified number of times.

-   `attemptCount`: The number of times to run the function.
-   `work`: The function to benchmark.

Returns a `BenchmarkResult` object containing the timing data.

### `CollectResult(int attemptCount, Action work)`

Benchmarks an action by running it the specified number of times.

-   `attemptCount`: The number of times to run the action.
-   `work`: The action to benchmark.

Returns a `BenchmarkResult` object containing the timing data.

## BenchmarkResult Class

The `BenchmarkResult` class encapsulates the results of a benchmark run.

### Properties

-   `long[] MilliSeconds`: An array of execution times for each attempt, in milliseconds.
-   `long[] PrivateMemoryUsage`: An array of private memory usage data for each attempt, in bytes.
-   `long[] WorkingMemoryUsage`: An array of working memory usage data for each attempt, in bytes.
-   `long[] VirtualMemoryUsage`: An array of virtual memory usage data for each attempt, in bytes.
-   `long[] PeakWorkingMemoryUsage`: An array of peak working memory usage for each attempt, in bytes.
-   `long[] PeakVirtualMemoryUsage`: An array of peak virtual memory usage for each attempt, in bytes.

### Methods

-   `ToString(string memoryUnit = "KB")`: Returns a string representation of the benchmark results, including the execution time and memory usage of each attempt, with memory units specified as either "KB" or "MB".

## Sample Output
```
## Summary
|--------------------------------|----------------------|
| Metric                         |                Value |
|--------------------------------|----------------------|
| Average Execution Time         |            548.30 ms |
| Peak Working Memory Usage(Avg) |          46297.60 KB |
| Peak Working Memory Usage(Max) |          47244.00 KB |
| Peak Virtual Memory Usage(Avg) |         204772.00 KB |
| Peak Virtual Memory Usage(Max) |         204772.00 KB |
| Average Private Memory         |          10114.40 KB |
| Average Working Memory         |          10154.80 KB |
| Average Virtual Memory         |           3698.80 KB |
|--------------------------------|----------------------|

## Detailed Attempts
|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|
| Attempt | Execution Time (ms) |    Private Memory (KB) |    Working Memory (KB) |    Virtual Memory (KB) |   Peak Virtual Memory (KB) |   Peak Working Memory (KB) |
|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|
| 1       |                 837 |               19656.00 |               23292.00 |               36988.00 |                  204772.00 |                   41400.00 |
| 2       |                 596 |               10052.00 |                9440.00 |                   0.00 |                  204772.00 |                   45288.00 |
| 3       |                 473 |                7848.00 |                8516.00 |                   0.00 |                  204772.00 |                   46472.00 |
| 4       |                 451 |                9964.00 |                9264.00 |                   0.00 |                  204772.00 |                   46472.00 |
| 5       |                 515 |                7848.00 |                7772.00 |                   0.00 |                  204772.00 |                   47208.00 |
| 6       |                 543 |                9964.00 |                9236.00 |                   0.00 |                  204772.00 |                   47208.00 |
| 7       |                 590 |                7848.00 |                7776.00 |                   0.00 |                  204772.00 |                   47220.00 |
| 8       |                 536 |               10028.00 |                9248.00 |                   0.00 |                  204772.00 |                   47220.00 |
| 9       |                 484 |                7912.00 |                7776.00 |                   0.00 |                  204772.00 |                   47244.00 |
| 10      |                 458 |               10024.00 |                9228.00 |                   0.00 |                  204772.00 |                   47244.00 |
|---------|---------------------|------------------------|------------------------|------------------------|----------------------------|----------------------------|
```
