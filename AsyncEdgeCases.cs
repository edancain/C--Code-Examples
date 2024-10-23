// EDGE CASES AND GOTCHAS

public class AsyncEdgeCases
{
    // Example 1: Handling Task cancellation and timeouts properly
    public async Task<T> WithCancellationAndTimeout<T>(
        Func<CancellationToken, Task<T>> operation,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        using var timeoutCts = new CancellationTokenSource(timeout);
        using var linkedCts = CancellationTokenSource
            .CreateLinkedTokenSource(timeoutCts.Token, cancellationToken);

        try
        {
            return await operation(linkedCts.Token);
        }
        catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
        {
            throw new TimeoutException();
        }
    }

    // Example 2: Proper cleanup with async disposable
    public class AsyncResourceManager : IAsyncDisposable
    {
        private bool _disposed;
        private readonly SemaphoreSlim _semaphore = new(1);

        public async Task UseResourceAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(AsyncResourceManager));
                
                // Use resource
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            
            _disposed = true;
            await DisposeAsyncCore();
            
            _semaphore.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual async Task DisposeAsyncCore()
        {
            // Cleanup async resources
            await Task.CompletedTask;
        }
    }
}
