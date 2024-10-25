/*
Break down the key attributes and their purposes:

1:  [MemoryDiagnoser]:

    - Enables memory allocation diagnostics
    - Measures:
        Memory allocated during method execution
        Garbage collection frequency
        Memory pressure on different GC generations
    - Helps identify:
        Memory leaks
        Inefficient memory usage
        Excessive object allocations
    - Adds overhead to benchmarks but provides valuable memory insights

2: [Benchmark]:
    - Marks a method for benchmarking
    - Key features:
        Measures execution time
        Can specify baseline for comparisons
        Supports parameters and arguments
        Can configure operations per invoke
    - Options include:
        Baseline = true (for comparison base)
        OperationsPerInvoke = N (for batch operations)


Additional Important Attributes:
1: [SimpleJob]:
    - Configures benchmark execution
    - Controls:
        Warmup iterations
        Target iterations
        Launch count
2: [Params]:
    - Tests method with different parameter values
    - Automatically runs benchmarks for each value

3: [ArgumentsSource]:
    - Provides test data from a method
    - More flexible than [Params]
4: Setup/Cleanup Attributes:
    - [GlobalSetup]: Runs once before all benchmarks
    - [GlobalCleanup]: Runs once after all benchmarks
    - [IterationSetup]: Runs before each iteration
    - [IterationCleanup]: Runs after each iteration

The output provides:
    - Mean execution time
    -  Error margins
    - Standard deviation
    - Memory allocation statistics
    - Garbage collection information
*/


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading;

[MemoryDiagnoser]                    // Enables memory allocation diagnostics
[SimpleJob(warmupCount: 3)]          // Configures the benchmark job
[RankColumn]                         // Adds a column for ranking results
public class BenchmarkDemo
{
    private readonly List<string> _items;
    
    public BenchmarkDemo()
    {
        _items = Enumerable.Range(1, 10000)
                          .Select(i => $"Item {i}")
                          .ToList();
    }

    [Benchmark(Baseline = true)]     // Marks this as the baseline for comparison
    [ArgumentsSource(nameof(Data))]  // Provides test data
    public string StandardLoop(int iterations)
    {
        var result = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            result += _items[i];
        }
        return result;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Data))]
    public string StringBuilder(int iterations)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            builder.Append(_items[i]);
        }
        return builder.ToString();
    }

    // Data source for benchmarks
    public IEnumerable<object[]> Data()
    {
        yield return new object[] { 100 };
        yield return new object[] { 1000 };
    }
}

// Example of custom attributes
public class BenchmarkExplanation
{
    /*
     * Common BenchmarkDotNet Attributes:
     * 
     * [MemoryDiagnoser] 
     * - Measures memory allocation
     * - Shows Gen 0/1/2 collections
     * - Displays allocated memory
     * - Helps identify memory leaks
     * 
     * [Benchmark]
     * - Marks method for benchmarking
     * - Can include parameters like:
     *   - Baseline = true (for comparison base)
     *   - OperationsPerInvoke = N (for batch operations)
     * 
     * Other Useful Attributes:
     */

    [SimpleJob(launchCount: 3,       // Number of times to launch process with benchmark
               warmupCount: 5,        // Number of warmup iterations
               targetCount: 10)]      // Number of actual benchmark iterations
    public void JobConfigExample() { }

    [RPlotExporter]                  // Generates R plot with results
    [CsvMeasurementsExporter]        // Exports raw results to CSV
    public void ExporterExample() { }

    [Params(100, 200, 300)]          // Test with different parameter values
    public int ParamExample;

    [IterationSetup]                 // Runs before each iteration
    public void IterationSetup() { }

    [IterationCleanup]               // Runs after each iteration
    public void IterationCleanup() { }

    [GlobalSetup]                    // Runs once before all benchmarks
    public void GlobalSetup() { }

    [GlobalCleanup]                  // Runs once after all benchmarks
    public void GlobalCleanup() { }
}

// Example usage and output interpretation
public class Program
{
    public static void Main()
    {
        var summary = BenchmarkRunner.Run<BenchmarkDemo>();
        
        /* Example Output Explanation:
         * 
         * Method        Mean      Error    StdDev    Gen 0   Allocated
         * StandardLoop  15.51 μs  0.31 μs  0.29 μs   7.2144    30 KB
         * StringBuilder  8.84 μs  0.17 μs  0.15 μs   4.1234    17 KB
         * 
         * Mean: Average execution time
         * Error: Statistical error in measurements
         * StdDev: Standard deviation
         * Gen 0: Garbage collections in generation 0
         * Allocated: Memory allocated during execution
         */
    }
}