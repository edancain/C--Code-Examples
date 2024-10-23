// Implemention of a rate limiter
public class RateLimiter
{
    private readonly SemaphoreSlim _semaphore;
    private readonly Queue<DateTime> _requestTimestamps;
    private readonly int _maxRequests;
    private readonly TimeSpan _interval;

    public RateLimiter(int maxRequests, TimeSpan interval)
    {
        _semaphore = new SemaphoreSlim(1);
        _requestTimestamps = new Queue<DateTime>();
        _maxRequests = maxRequests;
        _interval = interval;
    }

    public async Task<bool> TryRequestAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            var now = DateTime.UtcNow;
            while (_requestTimestamps.Count > 0 &&
                   now - _requestTimestamps.Peek() > _interval)
            {
                _requestTimestamps.Dequeue();
            }

            if (_requestTimestamps.Count < _maxRequests)
            {
                _requestTimestamps.Enqueue(now);
                return true;
            }

            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
