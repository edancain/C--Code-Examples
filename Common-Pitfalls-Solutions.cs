// COMMON PITFALLS AND SOLUTIONS

public class PitfallsAndSolutions
{
    // Pitfall 1: Deadlock in Async/Await
    public class DeadlockExample
    {
        // BAD: Can cause deadlock
        public void BadMethod()
        {
            var task = AsyncMethod();
            task.Wait(); // Potential deadlock
        }

        // GOOD: Proper async all the way
        public async Task GoodMethod()
        {
            await AsyncMethod();
        }
    }

    // Pitfall 2: Memory Leak in Event Handlers
    public class MemoryLeakExample
    {
        // BAD: Memory leak
        public class BadSubscriber
        {
            public BadSubscriber(Publisher publisher)
            {
                publisher.OnEvent += HandleEvent; // Never unsubscribed
            }
        }

        // GOOD: Proper cleanup
        public class GoodSubscriber : IDisposable
        {
            private readonly Publisher _publisher;

            public GoodSubscriber(Publisher publisher)
            {
                _publisher = publisher;
                _publisher.OnEvent += HandleEvent;
            }

            public void Dispose()
            {
                _publisher.OnEvent -= HandleEvent;
            }
        }
    }

    // Pitfall 3: Improper Exception Handling
    public class ExceptionHandlingExample
    {
        // BAD: Swallowing exceptions
        public async Task BadExceptionHandling()
        {
            try
            {
                await DoSomethingAsync();
            }
            catch (Exception)
            {
                // Swallowing exception
            }
        }

        // GOOD: Proper exception handling
        public async Task GoodExceptionHandling()
        {
            try
            {
                await DoSomethingAsync();
            }
            catch (SpecificException ex)
            {
                // Handle specific exception
                await HandleSpecificExceptionAsync(ex);
            }
            catch (Exception ex)
            {
                // Log and maybe rethrow
                _logger.LogError(ex, "Unexpected error");
                throw;
            }
        }
    }
}