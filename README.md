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
-   `double Average`: The average execution time, in milliseconds.

### Methods

-   `ToString()`: Returns a string representation of the benchmark results, including the execution time of each attempt and the average execution time.

## Example Output

```plaintext
Trial 1: 10 ms
Trial 2: 9 ms
Trial 3: 11 ms
...
Average: 10 ms
```
