// NULLABLE TYPES IN C#

/*
Key Concepts:
1. ? operator makes value types nullable
2. Value types (like char, int, bool) normally can't be null
3. Nullable<T> is the underlying structure
4. Common in database and API interactions
*/

// Basic Example
public class NullableExample
{
    // This method can return either a char or null
    public char? FirstNonRepeatedChar(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null; // Can return null because it's char?

        var charCount = new Dictionary<char, int>();
        
        foreach (char c in input)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }
        
        foreach (char c in input)
        {
            if (charCount[c] == 1)
                return c;
        }
        
        return null; // No non-repeated char found
    }

    // Different ways to declare nullable types
    public void NullableDeclarations()
    {
        // These are equivalent:
        char? nullableChar1 = null;
        Nullable<char> nullableChar2 = null;

        // Common value types that can be nullable:
        int? nullableInt = null;
        bool? nullableBool = null;
        DateTime? nullableDate = null;
        double? nullableDouble = null;
    }

    // Working with nullable types
    public void NullableOperations()
    {
        int? nullable = 5;

        // HasValue property
        if (nullable.HasValue)
        {
            Console.WriteLine($"Value is: {nullable.Value}");
        }

        // Null coalescing operator
        int definiteValue = nullable ?? 0; // Use 0 if nullable is null

        // Null conditional operator
        int? maybeNull = null;
        int? result = maybeNull?.CompareTo(5); // Safe navigation

        // Compound null coalescing
        int finalValue = maybeNull ?? nullable ?? 0;
    }

    // Practical example with database interaction
    public class DatabaseExample
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime? LastLoginDate { get; set; }  // Nullable because user might never have logged in
            public int? Age { get; set; }  // Nullable because age might not be provided
        }

        public User GetUser(int id)
        {
            // Simulated database record
            return new User
            {
                Id = id,
                Name = "John",
                LastLoginDate = null,  // Never logged in
                Age = null   // Age not provided
            };
        }

        public void ProcessUser(User user)
        {
            // Safe way to work with nullable properties
            string ageStatus = user.Age switch
            {
                null => "Age not provided",
                < 18 => "Minor",
                >= 18 and <= 65 => "Adult",
                > 65 => "Senior"
            };

            string lastLogin = user.LastLoginDate?.ToString("yyyy-MM-dd") ?? "Never logged in";
        }
    }

    // Nullable reference types (C# 8.0+)
    public class NullableReferenceExample
    {
        #nullable enable
        public string RequiredName { get; set; } = ""; // Must not be null
        public string? OptionalName { get; set; }      // Can be null

        public void ProcessName(string? name)
        {
            if (name is null)
            {
                // Handle null case
                return;
            }

            // At this point, compiler knows name is not null
            Console.WriteLine(name.ToUpper());
        }
        #nullable restore
    }

    // Common patterns with nullable types
    public class NullablePatterns
    {
        // Pattern 1: Safe default value
        public int GetValueOrDefault(int? nullable, int defaultValue = 0)
        {
            return nullable ?? defaultValue;
        }

        // Pattern 2: Nullable to Optional pattern
        public class Optional<T>
        {
            private readonly T? _value;
            public bool HasValue => _value != null;
            public T Value => _value ?? throw new InvalidOperationException("No value present");

            public Optional(T? value)
            {
                _value = value;
            }

            public void IfPresent(Action<T> action)
            {
                if (HasValue)
                    action(Value);
            }
        }

        // Pattern 3: Null Object pattern
        public interface ILogger
        {
            void Log(string message);
        }

        public class NullLogger : ILogger
        {
            public void Log(string message) { } // Do nothing
        }

        // Pattern 4: Try-pattern with nullable
        public bool TryGetFirstNonRepeatedChar(string input, out char result)
        {
            char? nullable = FirstNonRepeatedChar(input);
            if (nullable.HasValue)
            {
                result = nullable.Value;
                return true;
            }
            result = default;
            return false;
        }
    }
}

// Best Practices:

/*
1. Use nullable types when:
   - Value might not exist (database nullable columns)
   - Optional parameters or return values
   - Representing absence of value is meaningful

2. Avoid nullable types when:
   - Value should always exist
   - Default value makes more sense than null
   - Performance is critical (nullable types have overhead)

3. Guidelines:
   - Always check HasValue or use null-coalescing operator
   - Consider using the Try-pattern for methods that might fail
   - Use C# 8.0+ nullable reference types for better null safety
   - Be consistent with null handling across your codebase
*/