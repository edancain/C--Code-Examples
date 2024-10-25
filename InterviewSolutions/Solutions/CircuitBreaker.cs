public class CircuitBreaker
    {
        private readonly int _maxFailures;
        private readonly TimeSpan _resetTimeout;
        private int _failureCount;
        private DateTime _lastFailure;
        private bool _isOpen;

        public CircuitBreaker(int maxFailures, TimeSpan resetTimeout)
        {
            _maxFailures = maxFailures;
            _resetTimeout = resetTimeout;
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation)
        {
            if (_isOpen)
            {
                if (DateTime.UtcNow - _lastFailure > _resetTimeout)
                {
                    _isOpen = false;
                    _failureCount = 0;
                }
                else
                {
                    throw new CircuitBreakerOpenException();
                }
            }

            try
            {
                var result = await operation();
                _failureCount = 0;
                return result;
            }
            catch (Exception)
            {
                _failureCount++;
                _lastFailure = DateTime.UtcNow;
                if (_failureCount >= _maxFailures)
                {
                    _isOpen = true;
                }
                throw;
            }
        }
    }