public class Test{
    public string ExampleCode(string? str)
    {
        // Single null check with pattern matching
        if (str is null)
        {
            throw new ArgumentNullException(nameof(str));
        }

        // Now we know str is not null
        return str.Length > 0 ? str : string.Empty;
    }
}



// ONE-HOUR C# TECHNICAL INTERVIEW GUIDE

/*
Interview Structure:
1. Fundamentals (10 minutes)
2. Object-Oriented Concepts (10 minutes)
3. C# Specific Features (10 minutes)
4. Problem Solving (15 minutes)
5. Architecture and Design (10 minutes)
6. Real-World Scenarios (5 minutes)
*/

// 1. FUNDAMENTALS ASSESSMENT

// Start with basics to establish baseline
public class FundamentalQuestions
{
    


    /*
    Q2: "Could you explain this code and its potential issues?"
    */
    public void ExampleCode()
    {
        string str = null;
        int? nullableInt = null;
        int regularInt = 0;
        
        if (str.Length > 0) // What's wrong here?
        {
            Console.WriteLine(str);
        }
    }
    
    /*
    Follow-up: "How would you fix this code?"
    Looking for:
    - Null checking knowledge
    - Understanding of nullable types
    - Error handling approaches
    */

/*Strong Answer:
"Null checking is crucial for robust code. In modern C#, we have several tools:

1. Null-conditional operator (?.)
2. Null-coalescing operator (??)
3. Nullable reference types (C# 8.0+)
4. Null-forgiving operator (!)

Here's a practical example:
*/

public class NullHandlingExample
{
    public string ProcessData(string? input)
    {
        // Multiple approaches to null handling
        
        // 1. Null-coalescing
        // If input is null, use "default"
        var result1 = input ?? "default";
        // Same as: return input != null ? input : "default";
        
        // 2. Null conditional with coalescing
        // If input is null, don't call ToUpper(), return "DEFAULT"
        // If input is not null, call ToUpper() and return the result
        var result2 = input?.ToUpper() ?? "DEFAULT";
        // Same as:
        // if (input == null) return "DEFAULT";
        // return input.ToUpper();
        
        // 3. Pattern matching
        // Modern C# way to check for null and length
        if (input is not null && input.Length > 0)
        {
            return input.ToUpper();
        }
        
        // 4. Throw with meaningful message
        // If input is null, throw exception
        // If input is not null, return it
        return input ?? throw new ArgumentNullException(nameof(input));
    }
}


// 2. OBJECT-ORIENTED CONCEPTS

// Test OOP understanding with practical examples
public class OOPAssessment
{
    /*
    Q: "Looking at this code, what potential issues do you see and how would you improve it?"
    */
    public class Employee
    {
        public string Name;
        public decimal Salary;

        public void CalculateBonus()
        {
            // Direct field manipulation
            Salary = Salary * 1.1m;
        }
    }
    
    /*
    Looking for:
    - Encapsulation understanding
    - Property usage
    - Immutability considerations
    - Single Responsibility Principle
    */

    /*Strong Answer:
"I'd improve it in several ways to follow OOP principles:
1. Encapsulation
2. Immutability where appropriate
3. Single Responsibility
4. Inheritance support*/

public class ImprovedEmployee
{
    // Private backing fields
    private readonly string _name;
    private decimal _salary;

    // Constructor ensures required data
    public ImprovedEmployee(string name, decimal salary)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _salary = salary > 0 ? salary : 
            throw new ArgumentException("Salary must be positive");
    }

    // Properties with appropriate access
    public string Name => _name; // Immutable, an object that can't be changed after creation.
    // There are several reasons why strings are immutable in C#. One of the main reasons is that 
    // immutability can improve performance and memory usage. Because the value of an immutable 
    // object cannot be changed, the runtime can optimize how it is stored and accessed in memory.

    public decimal Salary
    {
        get => _salary;
        protected set => _salary = value > 0 ? value : 
            throw new ArgumentException("Salary must be positive");
    }

    // Virtual method for inheritance
    public virtual decimal CalculateBonus()
    {
        return Salary * 0.1m;
    }

    // Method with clear responsibility
    public void ApplyBonus()
    {
        Salary += CalculateBonus();
    }
}

    // Follow-up: "How would you refactor this to support different types of employees?"
}

// 3. C# SPECIFIC FEATURES

/*
Q: "Can you explain what this code does and potential improvements?"
Answer:
Original Code Analysis:
1. Uses Task.Run for CPU-bound work
2. No exception handling
3. No cancellation support
4. No performance optimization

*/
public class ModernCSharpFeatures
{
    public async Task<string> ProcessDataAsync(string input)
    {
        var result = await Task.Run(() => input.ToUpper());
        return result;
    }
      /*
    Looking for:
    - Async/await understanding
    - Task vs Thread knowledge
    - Performance considerations
    */

    // Improved version with best practices
    public async Task<string> ImprovedProcessDataAsync(
        string input,
        CancellationToken cancellationToken = default)
    {
        // Input validation
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        try
        {
            // Consider if Task.Run is really needed
            // ToUpper() is so fast it doesn't need to be offloaded
            if (input.Length > 1000) // Only offload if string is large
            {
                return await Task.Run(
                    () => input.ToUpper(),
                    cancellationToken
                ).ConfigureAwait(false);
            }

            // For small strings, do it synchronously
            return input.ToUpper();
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation
            throw; // Rethrow cancellation
        }
        catch (Exception ex)
        {
            // Log exception details
            throw new ApplicationException(
                "Error processing data", ex);
        }
    }
}

    
  

    // Follow-up: "How would you handle exceptions in this async method?"
}




// 4. PROBLEM SOLVING

/*
Present a problem and observe thought process:

"We need to process a large file containing millions of records, each with a timestamp 
and value. We need to find the average value for each hour. How would you approach this?"
*/

// LOOK TO LARGEFILEPROCESSING for an answer. 

public class ProblemSolvingAssessment
{
    public class DataProcessor
    {
        // Let candidate write implementation
        public Dictionary<DateTime, double> ProcessLargeFile(string filePath)
        {
            // Their solution here
            return null;
        }
    }
    
    /*
    Looking for:
    - Problem breakdown
    - Performance consideration
    - Memory management
    - Error handling
    - Testing approach
    */
}

// 5. ARCHITECTURE AND DESIGN

/*
Q: "How would you design a logging system that needs to support multiple output 
targets (console, file, database) and different log levels?"
*/

public interface ILogger
{
    // Let candidate design the interface and implementation
}

/*
Looking for:
- Interface design
- Design patterns knowledge
- Extensibility consideration
- SOLID principles understanding
*/

// 6. REAL-WORLD SCENARIOS

/*
Final questions to assess experience:

Q1: "Tell me about a challenging performance issue you've encountered in C# 
and how you solved it."

Q2: "What's your approach to unit testing in C#? Can you give an example?"

Q3: "How do you handle database connections in a web application?"
*/

// EVALUATION CRITERIA

/*
Technical Depth:
1. Basic Understanding (Junior Level)
   - Value vs Reference types
   - Basic OOP concepts
   - Simple async/await usage
   - Basic LINQ

2. Intermediate Knowledge (Mid-Level)
   - Memory management
   - Design patterns
   - Performance considerations
   - Exception handling strategies
   - Testing approaches

3. Advanced Expertise (Senior Level)
   - Architectural patterns
   - Performance optimization
   - Advanced C# features
   - System design
   - Best practices and standards

Problem-Solving Ability:
1. Code Organization
2. Performance Consideration
3. Error Handling
4. Edge Cases
5. Scalability Thinking

Professional Judgment:
1. Best Practices Awareness
2. Trade-off Considerations
3. Maintenance Mindset
4. Team Collaboration Approach
*/

// EXAMPLE CODING CHALLENGE

/*
Give this problem if time permits:

"Implement a thread-safe cache with the following requirements:
1. Generic key-value storage
2. Automatic cleanup of old items
3. Size limit
4. Thread-safe operations"
*/

public class CacheImplementation<TKey, TValue>
{
    // Let candidate implement
    public bool TryGet(TKey key, out TValue value)
    {
        // Their implementation
        value = default;
        return false;
    }

    public void Add(TKey key, TValue value)
    {
        // Their implementation
    }
}

/*
This challenges tests:
1. Generic type usage
2. Threading knowledge
3. Memory management
4. Performance consideration
5. Error handling
*/

// RED FLAGS IN RESPONSES

/*
1. No consideration for:
   - Null checking
   - Exception handling
   - Thread safety
   - Memory management
   - Performance implications

2. Poor understanding of:
   - Basic C# concepts
   - OOP principles
   - Async programming
   - Memory allocation

3. Inability to:
   - Explain their thinking
   - Consider edge cases
   - Discuss alternatives
   - Admit knowledge gaps
*/

// GREEN FLAGS IN RESPONSES

/*
1. Demonstrates:
   - Clear thinking process
   - Consideration of edge cases
   - Performance awareness
   - Best practices knowledge
   - Testing mindset

2. Shows understanding of:
   - Trade-offs
   - Real-world implications
   - Maintenance concerns
   - Team collaboration

3. Ability to:
   - Communicate clearly
   - Admit knowledge gaps
   - Discuss alternatives
   - Learn from discussion
*/