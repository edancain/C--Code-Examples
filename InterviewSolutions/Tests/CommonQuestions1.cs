// Main test harness
public class InterviewSolutionsDemo
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("C# Interview Solutions Demonstration");
        Console.WriteLine("==================================");

        // Run demos one at a time with user interaction
        await RunStringReversal();
        await RunFirstNonRepeatedChar();
        await RunPalindrome();
        await RunBinarySearch();
        await RunFibonacci();
    }

    // 1. String Reversal Demo
    private static async Task RunStringReversal()
    {
        Console.WriteLine("\n1. String Reversal");
        Console.WriteLine("------------------");

        var solution = new StringOperations();
        var testCases = new[]
        {
            "hello",
            "OpenAI",
            "a",
            "",
            "12345"
        };

        foreach (var test in testCases)
        {
            Console.WriteLine($"\nInput: '{test}'");
            Console.WriteLine($"Output: '{solution.ReverseString(test)}'");
            Console.WriteLine("Press any key for next test...");
            Console.ReadKey();
        }
    }
}

// Solutions with detailed comments

/// <summary>
/// Collection of string manipulation operations commonly asked in interviews
/// </summary>
public class StringOperations
{
    /// <summary>
    /// Reverses a string without using built-in functions
    /// Time Complexity: O(n)
    /// Space Complexity: O(n)
    /// </summary>
    public string ReverseString(string input)
    {
        // Handle null/empty cases first - good practice for interviews
        if (string.IsNullOrEmpty(input))
            return input;

        // Create character array of same length as input
        // We need an array because strings in C# are immutable
        char[] chars = new char[input.Length];

        // Fill the array from end to start
        // Example: "hello" -> ['o','l','l','e','h']
        for (int i = 0; i < input.Length; i++)
        {
            // Calculate reverse position:
            // For length 5, when i=0, we want position 4
            // when i=1, we want position 3, etc.
            chars[i] = input[input.Length - 1 - i];
        }

        // Create new string from char array
        return new string(chars);
    }

    /// <summary>
    /// Alternative implementation using pointer-like operations
    /// Shows knowledge of multiple approaches
    /// </summary>
    public string ReverseStringAlternative(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        char[] chars = input.ToCharArray();
        int left = 0;
        int right = chars.Length - 1;

        // Swap characters from outside moving in
        while (left < right)
        {
            // Swap using tuple deconstruction (C# 7.0+)
            (chars[left], chars[right]) = (chars[right], chars[left]);
            left++;
            right--;
        }

        return new string(chars);
    }
}

/// <summary>
/// Collection of searching and analysis operations on strings
/// </summary>
public class CharacterAnalyzer
{
    /// <summary>
    /// Finds first non-repeated character in a string
    /// Time Complexity: O(n)
    /// Space Complexity: O(k) where k is the size of the character set
    /// </summary>
    public char? FirstNonRepeated(string str)
    {
        if (string.IsNullOrEmpty(str))
            return null;

        // Dictionary to store character frequencies
        // Using Dictionary instead of array for clarity and handling larger character sets
        var charCount = new Dictionary<char, int>();

        // First pass: Count frequencies
        foreach (char c in str)
        {
            charCount[c] = charCount.GetValueOrDefault(c) + 1;
        }

        // Second pass: Find first character with count 1
        // We maintain string order by checking in original sequence
        foreach (char c in str)
        {
            if (charCount[c] == 1)
                return c;
        }

        return null;
    }

    /// <summary>
    /// LINQ-based implementation - shows knowledge of C# features
    /// Note: Less efficient but more concise
    /// </summary>
    public char? FirstNonRepeatedLinq(string str)
    {
        return str.GroupBy(c => c)
                 .Where(g => g.Count() == 1)
                 .Select(g => g.Key)
                 .FirstOrDefault();
    }
}

// Test harness for string operations
public class StringOperationsTests
{
    private readonly StringOperations _stringOps = new();
    private readonly CharacterAnalyzer _charAnalyzer = new();

    public void RunAllTests()
    {
        TestStringReversal();
        TestFirstNonRepeated();
        // Add more tests...
    }

    private void TestStringReversal()
    {
        Console.WriteLine("\nTesting String Reversal");
        Console.WriteLine("=======================");

        var testCases = new Dictionary<string, string>
        {
            { "hello", "olleh" },
            { "OpenAI", "IAnepO" },
            { "a", "a" },
            { "", "" },
            { null, null }
        };

        foreach (var test in testCases)
        {
            string result = _stringOps.ReverseString(test.Key);
            bool passed = result == test.Value;
            Console.WriteLine($"Input: '{test.Key ?? "null"}'");
            Console.WriteLine($"Expected: '{test.Value ?? "null"}'");
            Console.WriteLine($"Got: '{result ?? "null"}'");
            Console.WriteLine($"Test {(passed ? "PASSED" : "FAILED")}");
            Console.WriteLine();
        }
    }

    private void TestFirstNonRepeated()
    {
        Console.WriteLine("\nTesting First Non-Repeated Character");
        Console.WriteLine("===================================");

        var testCases = new Dictionary<string, char?>
        {
            { "stress", 't' },
            { "aabbcc", null },
            { "aabbccd", 'd' },
            { "", null },
            { "aaa", null }
        };

        foreach (var test in testCases)
        {
            char? result = _charAnalyzer.FirstNonRepeated(test.Key);
            bool passed = result == test.Value;
            Console.WriteLine($"Input: '{test.Key}'");
            Console.WriteLine($"Expected: '{test.Value}'");
            Console.WriteLine($"Got: '{result}'");
            Console.WriteLine($"Test {(passed ? "PASSED" : "FAILED")}");
            Console.WriteLine();
        }
    }
}