// SYNCHRONIZATION PRIMITIVES AND ATOMIC OPERATIONS IN C#

/*
Key Concepts:
1. Mutex - system-wide synchronization
2. Interlocked - atomic operations
3. VolatileRead/Write - memory barriers
4. SpinLock - lightweight synchronization
5. SemaphoreSlim - throttling and resource control
*/

// 1. MUTEX EXAMPLES AND PATTERNS

public class MutexExamples
{
    // Example 1: Using Mutex for Single-Instance Application
    public class SingleInstanceApp
    {
        private static Mutex _mutex;

        public static bool IsAlreadyRunning()
        {
            const string mutexName = "Global\\MyUniqueApplicationName";
            bool createdNew;

            try
            {
                _mutex = new Mutex(true, mutexName, out createdNew);
                return !createdNew;
            }
            catch (UnauthorizedAccessException)
            {
                return true; // Another instance is running with elevated privileges
            }
        }

        public static void RunApplication()
        {
            if (IsAlreadyRunning())
            {
                Console.WriteLine("Application is already running!");
                return;
            }

            try
            {
                // Application code here
                Console.WriteLine("Application is running...");
                Console.ReadLine();
            }
            finally
            {
                _mutex?.ReleaseMutex();
                _mutex?.Dispose();
            }
        }
    }

    // Example 2: Cross-Process Resource Synchronization
    public class SharedResourceManager
    {
        private const string MutexName = "Global\\SharedResourceAccess";
        private readonly Mutex _mutex;

        public SharedResourceManager()
        {
            _mutex = new Mutex(false, MutexName);
        }

        public async Task AccessSharedResourceAsync(CancellationToken token)
        {
            try
            {
                await Task.Run(() =>
                {
                    if (!_mutex.WaitOne(TimeSpan.FromSeconds(30)))
                        throw new TimeoutException("Failed to acquire mutex");

                    try
                    {
                        // Access shared resource
                        ProcessSharedResource();
                    }
                    finally
                    {
                        _mutex.ReleaseMutex();
                    }
                }, token);
            }
            catch (AbandonedMutexException)
            {
                // Handle case where previous holder terminated without releasing
                _mutex.ReleaseMutex();
                throw;
            }
        }

        private void ProcessSharedResource()
        {
            // Process shared resource
        }
    }
}

// 2. ATOMIC OPERATIONS AND INTERLOCKED

public class AtomicOperationsExample
{
    // Example 1: Basic Atomic Operations
    public class AtomicCounter
    {
        private long _value;

        public long Increment()
        {
            return Interlocked.Increment(ref _value);
        }

        public long Decrement()
        {
            return Interlocked.Decrement(ref _value);
        }

        public long Add(long value)
        {
            return Interlocked.Add(ref _value, value);
        }

        public long Read()
        {
            return Interlocked.Read(ref _value);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
        }
    }

    // Example 2: Compare and Exchange Pattern
    public class LockFreeStack<T>
    {
        private class Node
        {
            public T Value;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node _head;

        public void Push(T value)
        {
            var newNode = new Node(value);
            while (true)
            {
                var currentHead = _head;
                newNode.Next = currentHead;
                if (Interlocked.CompareExchange(ref _head, newNode, currentHead) == currentHead)
                    break;
            }
        }

        public bool TryPop(out T value)
        {
            while (true)
            {
                var currentHead = _head;
                if (currentHead == null)
                {
                    value = default;
                    return false;
                }

                if (Interlocked.CompareExchange(ref _head, currentHead.Next, currentHead) == currentHead)
                {
                    value = currentHead.Value;
                    return true;
                }
            }
        }
    }
}

// 3. VOLATILE AND MEMORY BARRIERS

public class VolatileExample
{
    private volatile bool _shouldStop;
    private volatile int _counter;

    public void WorkerThread()
    {
        while (!_shouldStop)
        {
            // Do work
            Interlocked.Increment(ref _counter);
        }
    }

    public void StopWork()
    {
        _shouldStop = true;
    }

    // Example of memory barrier usage
    public class MemoryBarrierExample
    {
        private int _flag;
        private int _value;

        public void Writer()
        {
            _value = 42;
            Thread.MemoryBarrier(); // Ensure _value is written before _flag
            _flag = 1;
        }

        public void Reader()
        {
            if (_flag == 1)
            {
                Thread.MemoryBarrier(); // Ensure _flag is read before _value
                Console.WriteLine(_value); // Will always print 42
            }
        }
    }
}

// 4. SPINLOCK USAGE

public class SpinLockExample
{
    private SpinLock _spinLock = new SpinLock();

    public void ProcessWithSpinLock()
    {
        bool lockTaken = false;
        try
        {
            _spinLock.Enter(ref lockTaken);
            // Critical section
        }
        finally
        {
            if (lockTaken) _spinLock.Exit();
        }
    }

    // Example of custom spin-wait
    public class CustomSpinWait
    {
        private int _isLocked = 0;

        public void Lock()
        {
            var spinWait = new SpinWait();
            while (Interlocked.CompareExchange(ref _isLocked, 1, 0) != 0)
            {
                spinWait.SpinOnce();
            }
        }

        public void Unlock()
        {
            Interlocked.Exchange(ref _isLocked, 0);
        }
    }
}

// 5. PERFORMANCE COMPARISON

public class SynchronizationBenchmarks
{
    private const int IterationCount = 1000000;
    private readonly object _lockObject = new object();
    private readonly Mutex _mutex = new Mutex();
    private readonly SpinLock _spinLock = new SpinLock();
    private long _counter;

    public void ComparePerformance()
    {
        // Lock performance
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < IterationCount; i++)
        {
            lock (_lockObject)
            {
                _counter++;
            }
        }
        Console.WriteLine($"Lock time: {sw.ElapsedMilliseconds}ms");

        // Interlocked performance
        sw.Restart();
        for (int i = 0; i < IterationCount; i++)
        {
            Interlocked.Increment(ref _counter);
        }
        Console.WriteLine($"Interlocked time: {sw.ElapsedMilliseconds}ms");

        // SpinLock performance
        sw.Restart();
        for (int i = 0; i < IterationCount; i++)
        {
            bool lockTaken = false;
            try
            {
                _spinLock.Enter(ref lockTaken);
                _counter++;
            }
            finally
            {
                if (lockTaken) _spinLock.Exit();
            }
        }
        Console.WriteLine($"SpinLock time: {sw.ElapsedMilliseconds}ms");
    }
}

// 6. BEST PRACTICES AND RECOMMENDATIONS

/*
1. Choose the right synchronization primitive:
   - Mutex: Cross-process synchronization
   - Lock: Simple in-process synchronization
   - SpinLock: Very short-duration locks
   - Interlocked: Simple atomic operations
   - Volatile: Lightweight memory barriers

2. Performance considerations:
   - Mutex is slowest but works across processes
   - Lock is faster but only works in-process
   - SpinLock is fastest for very short operations
   - Interlocked operations are optimal for simple atomic operations

3. Common pitfalls to avoid:
   - Don't hold locks/mutexes for long periods
   - Always release locks in finally blocks
   - Be aware of deadlock possibilities
   - Don't use lock(string) or lock on type objects
*/


