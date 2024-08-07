# Benchmarker

The `Benchmarker` class provides a simple yet powerful utility for measuring the execution time of functions and actions in .NET applications. It allows developers to benchmark the performance of specific code segments by repeatedly executing them and collecting timing data.

## Features

- **Benchmarking Functions and Actions**: Supports benchmarking both functions that return a result (`Func<T>`) and actions that do not (`Action`).
- **Multiple Attempts**: Allows specifying the number of times to run the benchmark, providing more accurate and stable performance measurements.
- **Detailed Results**: Collects and returns detailed timing information for each run, along with the average execution time.

## Usage

The `Benchmarker` class includes two static methods, `CollectBenchmark`, which are used to benchmark functions and actions.

### Benchmarking a Function

```csharp
int attemptCount = 100;
Func<int> work = () => {
    // Your code here
    return 42;
};

RunResult result = Benchmarker.CollectBenchmark(attemptCount, work);
Console.WriteLine(result);

### Benchmarking an Action

```csharp
int attemptCount = 100;
Action work = () => {
    // Your code here
};

RunResult result = Benchmarker.CollectBenchmark(attemptCount, work);
Console.WriteLine(result);




