// Main demonstrator continuation
public class InterviewSolutionsDemo
{
    private static async Task RunDemonstrations11To15()
    {
        await RunProducerConsumer();
        await RunLongestCommonSubsequence();
        await RunObserverPattern();
        await RunRateLimiter();
        await RunPermutations();
    }

    // 11. Producer-Consumer Pattern Demo
    private static async Task RunProducerConsumer()
    {
        Console.WriteLine("\n11. Producer-Consumer Pattern");
        Console.WriteLine("----------------------------");

        var buffer = new ProducerConsumer<int>(capacity: 3);
        var cts = new CancellationTokenSource();

        // Producer task
        var producerTask = Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Producing: {i}");
                buffer.Produce(i);
                await Task.Delay(100);  // Simulate work
            }
        });

        // Consumer task
        var consumerTask = Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                var item = buffer.Consume();
                Console.WriteLine($"Consumed: {item}");
                await Task.Delay(200);  // Simulate slower consumer
            }
        });

        await Task.WhenAll(producerTask, consumerTask);
    }
}

/// <summary>
/// Thread-safe producer-consumer implementation with bounded buffer
/// Demonstrates synchronization and thread safety concepts
/// </summary>
public class ProducerConsumer<T>
{
    private readonly Queue<T> _queue = new();
    private readonly int _capacity;
    private readonly object _lock = new();
    
    public ProducerConsumer(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive", nameof(capacity));
        _capacity = capacity;
    }

    /// <summary>
    /// Produces an item, waiting if buffer is full
    /// Uses Monitor for thread synchronization
    /// </summary>
    public void Produce(T item)
    {
        lock (_lock)
        {
            // Wait while queue is full
            while (_queue.Count >= _capacity)
            {
                Console.WriteLine("Buffer full, producer waiting...");
                Monitor.Wait(_lock);
            }

            _queue.Enqueue(item);
            Console.WriteLine($"Produced: {item}, Buffer size: {_queue.Count}");
            
            // Signal consumers
            Monitor.Pulse(_lock);
        }
    }

    /// <summary>
    /// Consumes an item, waiting if buffer is empty
    /// </summary>
    public T Consume()
    {
        lock (_lock)
        {
            // Wait while queue is empty
            while (_queue.Count == 0)
            {
                Console.WriteLine("Buffer empty, consumer waiting...");
                Monitor.Wait(_lock);
            }

            var item = _queue.Dequeue();
            Console.WriteLine($"Consumed: {item}, Buffer size: {_queue.Count}");
            
            // Signal producers
            Monitor.Pulse(_lock);
            return item;
        }
    }
}

/// <summary>
/// Longest Common Subsequence implementation with detailed tracking
/// Demonstrates dynamic programming approach
/// </summary>
public class LongestCommonSubsequence
{
    /// <summary>
    /// Finds the longest common subsequence of two strings
    /// Time Complexity: O(mn)
    /// Space Complexity: O(mn)
    /// </summary>
    public string FindLCS(string text1, string text2)
    {
        if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
            return string.Empty;

        // Create DP table with dimensions (m+1) x (n+1)
        int[,] dp = new int[text1.Length + 1, text2.Length + 1];
        
        // Fill the DP table
        for (int i = 1; i <= text1.Length; i++)
        {
            for (int j = 1; j <= text2.Length; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        // Reconstruct the LCS from the DP table
        return ReconstructLCS(dp, text1, text2);
    }

    private string ReconstructLCS(int[,] dp, string text1, string text2)
    {
        var result = new StringBuilder();
        int i = text1.Length, j = text2.Length;

        while (i > 0 && j > 0)
        {
            if (text1[i - 1] == text2[j - 1])
            {
                result.Insert(0, text1[i - 1]);
                i--; j--;
            }
            else if (dp[i - 1, j] > dp[i, j - 1])
            {
                i--;
            }
            else
            {
                j--;
            }
        }

        return result.ToString();
    }
}

/// <summary>
/// Observer Pattern implementation with generic type support
/// Demonstrates design pattern implementation in C#
/// </summary>
public interface IObserver<T>
{
    void Update(T data);
}

public class Observable<T>
{
    private readonly HashSet<IObserver<T>> _observers = new();
    
    // Thread-safe observer management
    private readonly object _lock = new();

    public void Attach(IObserver<T> observer)
    {
        lock (_lock)
        {
            _observers.Add(observer);
        }
    }

    public void Detach(IObserver<T> observer)
    {
        lock (_lock)
        {
            _observers.Remove(observer);
        }
    }

    public void Notify(T data)
    {
        IObserver<T>[] observersSnapshot;
        lock (_lock)
        {
            observersSnapshot = _observers.ToArray();
        }

        foreach (var observer in observersSnapshot)
        {
            try
            {
                observer.Update(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error notifying observer: {ex.Message}");
                // Continue notifying other observers
            }
        }
    }
}

/// <summary>
/// Rate Limiter implementation with sliding window
/// Demonstrates concurrency control and time-based operations
/// </summary>
public class RateLimiter
{
    private readonly Queue<DateTime> _requests = new();
    private readonly int _limit;
    private readonly TimeSpan _window;
    private readonly object _lock = new();

    public RateLimiter(int limit, TimeSpan window)
    {
        _limit = limit > 0 ? limit : throw new ArgumentException("Limit must be positive");
        _window = window > TimeSpan.Zero ? window : throw new ArgumentException("Window must be positive");
    }

    /// <summary>
    /// Checks if request should be allowed based on rate limit
    /// Thread-safe implementation
    /// </summary>
    public bool ShouldAllowRequest()
    {
        lock (_lock)
        {
            var now = DateTime.UtcNow;
            
            // Remove expired requests
            while (_requests.Count > 0 && now - _requests.Peek() > _window)
            {
                _requests.Dequeue();
            }

            // Check if we're at the limit
            if (_requests.Count >= _limit)
                return false;

            // Add new request
            _requests.Enqueue(now);
            return true;
        }
    }
}

/// <summary>
/// String permutation generator with yield return
/// Demonstrates recursive algorithm and IEnumerable usage
/// </summary>
public class PermutationGenerator
{
    /// <summary>
    /// Generates all permutations of input string
    /// Uses yield return for memory efficiency
    /// </summary>
    public IEnumerable<string> GetPermutations(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            yield return string.Empty;
            yield break;
        }

        for (int i = 0; i < str.Length; i++)
        {
            // Remove current character and generate permutations of remaining
            var remaining = str.Remove(i, 1);
            
            // Recursively get permutations of remaining string
            foreach (var permutation in GetPermutations(remaining))
            {
                // Add current character to each permutation
                yield return str[i] + permutation;
            }
        }
    }
}

// Test harness for solutions 11-15
public class TestHarness11To15
{
    public async Task RunAllTests()
    {
        await TestProducerConsumer();
        TestLCS();
        TestObserverPattern();
        TestRateLimiter();
        TestPermutations();
    }

    private async Task TestProducerConsumer()
    {
        Console.WriteLine("\nTesting Producer-Consumer Pattern");
        var buffer = new ProducerConsumer<int>(3);

        var producer = Task.Run(async () =>
        {
            for (int i = 0; i < 5; i++)
            {
                buffer.Produce(i);
                await Task.Delay(100);
            }
        });

        var consumer = Task.Run(async () =>
        {
            for (int i = 0; i < 5; i++)
            {
                var item = buffer.Consume();
                Console.WriteLine($"Consumed: {item}");
                await Task.Delay(200);
            }
        });

        await Task.WhenAll(producer, consumer);
    }

    // Additional test methods...
}