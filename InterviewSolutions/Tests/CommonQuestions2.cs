Detailed Solutions with Test Harness - Part 2 (Solutions 6-10)

// Continue demonstrator from previous section
public class InterviewSolutionsDemo
{
    private static async Task RunMoreDemonstrations()
    {
        await RunPairFinder();
        await RunSingletonDemo();
        await RunQuickSort();
        await RunStackWithMax();
        await RunLRUCache();
    }

    // 6. Find Pairs Demo
    private static async Task RunPairFinder()
    {
        Console.WriteLine("\n6. Find Pairs that Sum to Target");
        Console.WriteLine("--------------------------------");

        var solution = new ArrayOperations();
        var testArrays = new[]
        {
            (array: new[] {1, 5, 7, 2, 9, 3}, target: 10),
            (array: new[] {1, 2, 3, 4, 5}, target: 5),
            (array: new[] {1, 1, 1, 1}, target: 2),
            (array: Array.Empty<int>(), target: 0)
        };

        foreach (var test in testArrays)
        {
            Console.WriteLine($"\nArray: [{string.Join(", ", test.array)}]");
            Console.WriteLine($"Target Sum: {test.target}");
            var pairs = solution.FindPairs(test.array, test.target).ToList();
            Console.WriteLine("Found Pairs:");
            foreach (var (first, second) in pairs)
            {
                Console.WriteLine($"({first}, {second})");
            }
            Console.WriteLine("Press any key for next test...");
            Console.ReadKey();
        }
    }
}

/// <summary>
/// Demonstrates array operations commonly asked in interviews
/// </summary>
public class ArrayOperations
{
    /// <summary>
    /// Finds all pairs in array that sum to target value
    /// Time Complexity: O(n)
    /// Space Complexity: O(n)
    /// Uses HashSet for O(1) lookup
    /// </summary>
    public IEnumerable<(int, int)> FindPairs(int[] array, int target)
    {
        // HashSet to store numbers we've seen
        var seen = new HashSet<int>();

        // Process each number in array
        foreach (int num in array)
        {
            // Calculate complement needed to reach target
            int complement = target - num;

            // If we've seen the complement, we found a pair
            if (seen.Contains(complement))
            {
                yield return (complement, num);
            }

            // Add current number to seen set
            seen.Add(num);
        }
    }
}

/// <summary>
/// Thread-safe singleton implementation with detailed explanation
/// </summary>
public sealed class Singleton
{
    // Static holder for instance, initialized lazily
    private static readonly Lazy<Singleton> _instance = 
        new Lazy<Singleton>(() => new Singleton(), LazyThreadSafetyMode.ExecutionAndPublication);

    // Private constructor prevents direct instantiation
    private Singleton()
    {
        // Initialization code here
        Console.WriteLine("Singleton instance created");
    }

    // Public access point for instance
    public static Singleton Instance => _instance.Value;

    // Example operation to demonstrate usage
    public void DoSomething()
    {
        Console.WriteLine("Singleton operation executed");
    }
}

/// <summary>
/// QuickSort implementation with detailed explanations
/// </summary>
public class QuickSort
{
    /// <summary>
    /// Sorts array using QuickSort algorithm
    /// Time Complexity: Average O(n log n), Worst O(n²)
    /// Space Complexity: O(log n) due to recursion stack
    /// </summary>
    public void Sort<T>(T[] array) where T : IComparable<T>
    {
        // Validate input
        if (array == null || array.Length <= 1)
            return;

        // Start recursive sorting
        QuickSortRecursive(array, 0, array.Length - 1);
    }

    private void QuickSortRecursive<T>(T[] array, int left, int right) 
        where T : IComparable<T>
    {
        // Base case: if left >= right, subarray is sorted
        if (left >= right)
            return;

        // Get pivot position and recursively sort subarrays
        int pivot = Partition(array, left, right);
        
        // Sort left of pivot
        QuickSortRecursive(array, left, pivot - 1);
        
        // Sort right of pivot
        QuickSortRecursive(array, pivot + 1, right);
    }

    private int Partition<T>(T[] array, int left, int right) 
        where T : IComparable<T>
    {
        // Choose rightmost element as pivot
        var pivot = array[right];
        
        // Index for smaller element
        int i = left - 1;

        // Compare each element with pivot
        for (int j = left; j < right; j++)
        {
            if (array[j].CompareTo(pivot) <= 0)
            {
                i++; // Increment index of smaller element
                Swap(array, i, j);
            }
        }

        // Place pivot in correct position
        Swap(array, i + 1, right);
        return i + 1;
    }

    private void Swap<T>(T[] array, int i, int j)
    {
        (array[i], array[j]) = (array[j], array[i]);
    }
}

/// <summary>
/// Stack implementation that tracks maximum value
/// Uses additional stack to maintain maximum values
/// </summary>
public class StackWithMax<T> where T : IComparable<T>
{
    // Main stack for elements
    private readonly Stack<T> _stack = new();
    
    // Additional stack to track maximum values
    private readonly Stack<T> _maxStack = new();

    /// <summary>
    /// Pushes item onto stack and updates maximum
    /// Time Complexity: O(1)
    /// </summary>
    public void Push(T item)
    {
        _stack.Push(item);
        
        // Update max stack if new item is greater or equal
        if (!_maxStack.Any() || item.CompareTo(_maxStack.Peek()) >= 0)
            _maxStack.Push(item);
    }

    /// <summary>
    /// Removes and returns top item
    /// Time Complexity: O(1)
    /// </summary>
    public T Pop()
    {
        if (!_stack.Any())
            throw new InvalidOperationException("Stack is empty");

        var item = _stack.Pop();
        
        // If we're removing current maximum, update max stack
        if (item.CompareTo(_maxStack.Peek()) == 0)
            _maxStack.Pop();
            
        return item;
    }

    /// <summary>
    /// Returns current maximum value
    /// Time Complexity: O(1)
    /// </summary>
    public T Max()
    {
        if (!_maxStack.Any())
            throw new InvalidOperationException("Stack is empty");
            
        return _maxStack.Peek();
    }
}

/// <summary>
/// LRU Cache implementation with capacity limit
/// Uses Dictionary and LinkedList for O(1) operations
/// </summary>
public class LRUCache<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<(TKey key, TValue value)>> _cache;
    private readonly LinkedList<(TKey key, TValue value)> _lruList;

    public LRUCache(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive", nameof(capacity));
            
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<(TKey key, TValue value)>>();
        _lruList = new LinkedList<(TKey key, TValue value)>();
    }

    /// <summary>
    /// Gets value and moves it to most recently used position
    /// Time Complexity: O(1)
    /// </summary>
    public TValue Get(TKey key)
    {
        if (!_cache.ContainsKey(key))
            throw new KeyNotFoundException($"Key {key} not found in cache");

        var node = _cache[key];
        _lruList.Remove(node);
        _lruList.AddFirst(node);
        return node.Value.value;
    }

    /// <summary>
    /// Adds or updates value and maintains capacity limit
    /// Time Complexity: O(1)
    /// </summary>
    public void Put(TKey key, TValue value)
    {
        if (_cache.ContainsKey(key))
        {
            // Update existing item
            _lruList.Remove(_cache[key]);
        }
        else if (_cache.Count >= _capacity)
        {
            // Remove least recently used item
            var last = _lruList.Last;
            _cache.Remove(last.Value.key);
            _lruList.RemoveLast();
        }

        // Add new item
        var node = _lruList.AddFirst((key, value));
        _cache[key] = node;
    }
}

// Test harness for these implementations
public class AdvancedDataStructureTests
{
    public void RunAllTests()
    {
        TestPairFinder();
        TestSingleton();
        TestQuickSort();
        TestStackWithMax();
        TestLRUCache();
    }

    private void TestPairFinder()
    {
        var arrayOps = new ArrayOperations();
        Console.WriteLine("\nTesting Pair Finder");
        
        var testCases = new[]
        {
            (array: new[] {1, 5, 7, 2, 9, 3}, target: 10,
             expected: new[] {(1, 9), (7, 3)}),
            // Add more test cases...
        };

        foreach (var test in testCases)
        {
            var result = arrayOps.FindPairs(test.array, test.target).ToList();
            Console.WriteLine($"Input: [{string.Join(", ", test.array)}], Target: {test.target}");
            Console.WriteLine($"Found pairs: {string.Join(", ", result.Select(p => $"({p.Item1},{p.Item2})"))}");
        }
    }

    // Additional test methods...
}