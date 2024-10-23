// Implement a generic method that combines multiple async operations with timeout
public static class AsyncUtilities
{
    public static async Task<IEnumerable<T>> WaitAllWithTimeout<T>(
        IEnumerable<Task<T>> tasks,
        TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource();
        var timeoutTask = Task.Delay(timeout, cts.Token);
        
        var allTasks = Task.WhenAll(tasks);
        var completedTask = await Task.WhenAny(allTasks, timeoutTask);
        
        if (completedTask == timeoutTask)
        {
            cts.Cancel();
            throw new TimeoutException();
        }
        
        cts.Cancel();
        return await allTasks;
    }
}