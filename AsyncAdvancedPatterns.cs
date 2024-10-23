public class AdvancedAsyncPatterns
{
    // Implementing Retry Pattern with Exponential Backoff
    public async Task<T> RetryWithExponentialBackoff<T>(
        Func<Task<T>> operation,
        int maxAttempts = 3,
        int initialDelayMs = 100)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            try
            {
                return await operation();
            }
            catch (Exception ex) when (attempt < maxAttempts - 1)
            {
                int delayMs = initialDelayMs * (int)Math.Pow(2, attempt);
                await Task.Delay(delayMs);
            }
        }
        return await operation(); // Final attempt
    }