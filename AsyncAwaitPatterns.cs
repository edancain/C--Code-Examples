/*
Key Concepts:
- async/await is syntactic sugar for working with Task-based operations
- Allows writing asynchronous code that looks synchronous
- Best practices include:
  * Always use ConfigureAwait(false) in library code
  * Avoid mixing async and sync code
  * Use Task.WhenAll for parallel operations
  * Handle exceptions properly in async context
*/

public class AsyncAwaitPatterns
{
    // Basic async pattern
    public async Task<string> BasicAsyncExample()
    {
        await Task.Delay(1000); // Simulate work
        return "Complete";
    }

    // Parallel execution
    public async Task ParallelAsyncExample()
    {
        var tasks = new List<Task<string>>();
        for (int i = 0; i < 3; i++)
        {
            tasks.Add(BasicAsyncExample());
        }
        
        var results = await Task.WhenAll(tasks);
        // All tasks complete in parallel
    }

    // Exception handling in async context
    public async Task AsyncExceptionHandling()
    {
        try
        {
            await ThrowingAsyncMethod();
        }
        catch (Exception ex) when (LogException(ex))
        {
            // Handle exception
            throw; // Re-throw if needed
        }
    }

    private bool LogException(Exception ex)
    {
        Console.WriteLine($"Exception logged: {ex.Message}");
        return true;
    }

    // Async stream example (.NET Core 3.0+)
    public async IAsyncEnumerable<int> GenerateNumbersAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(100);
            yield return i;
        }
    }

    // Progress reporting
    public async Task ProcessWithProgress(IProgress<int> progress)
    {
        for (int i = 0; i <= 100; i += 10)
        {
            await Task.Delay(100);
            progress.Report(i);
        }
    }
}