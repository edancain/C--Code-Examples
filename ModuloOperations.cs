// MODULO OPERATOR (%) EXPLANATION

/*
In the statement: if (i % 3 == 0 && i % 5 == 0)
This checks if a number is divisible by both 3 AND 5
- i % 3 == 0 checks if i is divisible by 3 (no remainder)
- i % 5 == 0 checks if i is divisible by 5 (no remainder)
*/

public class ModuloExamples
{
    // Basic examples of modulo operation
    public void BasicModuloOperations()
    {
        // Division vs Modulo
        Console.WriteLine("7 / 3 = " + 7 / 3);     // Output: 2 (quotient)
        Console.WriteLine("7 % 3 = " + 7 % 3);     // Output: 1 (remainder)

        // More examples
        Console.WriteLine("15 % 5 = " + 15 % 5);   // Output: 0 (divisible)
        Console.WriteLine("17 % 5 = " + 17 % 5);   // Output: 2 (remainder)
        Console.WriteLine("3 % 7 = " + 3 % 7);     // Output: 3 (number smaller than divisor)
    }

    // FizzBuzz implementation using modulo
    public void FizzBuzz(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                Console.WriteLine("FizzBuzz"); // Number is divisible by both 3 and 15
            else if (i % 3 == 0)
                Console.WriteLine("Fizz");     // Number is divisible by 3
            else if (i % 5 == 0)
                Console.WriteLine("Buzz");     // Number is divisible by 5
            else
                Console.WriteLine(i);          // Number isn't divisible by 3 or 5
        }
    }

    // Common uses of modulo operator
    public class ModuloApplications
    {
        // 1. Check if number is even or odd
        public bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        // 2. Wrap around array indices
        public T GetCircularArrayItem<T>(T[] array, int index)
        {
            return array[index % array.Length];
            // If array.Length is 5:
            // index 7 becomes 7 % 5 = 2
            // index 12 becomes 12 % 5 = 2
        }

        // 3. Time calculations (12-hour clock)
        public int ConvertTo12HourFormat(int hour)
        {
            return ((hour - 1) % 12) + 1;
            // 13 becomes 1
            // 24 becomes 12
        }

        // 4. Distribute items evenly
        public int AssignToGroup(int item, int numberOfGroups)
        {
            return item % numberOfGroups;
            // Distributes items 0 through n-1 among groups
        }

        // 5. Check if number is divisible by another
        public bool IsDivisibleBy(int number, int divisor)
        {
            return number % divisor == 0;
        }
    }

    // Real-world examples
    public class RealWorldExamples
    {
        // 1. Pagination
        public class PaginationExample
        {
            public int CalculateTotalPages(int totalItems, int itemsPerPage)
            {
                return (totalItems + itemsPerPage - 1) / itemsPerPage;
            }

            public bool IsLastPage(int currentPage, int totalItems, int itemsPerPage)
            {
                return totalItems % itemsPerPage == 0 ? 
                    currentPage == totalItems / itemsPerPage : 
                    currentPage == (totalItems / itemsPerPage) + 1;
            }
        }

        // 2. Circular Buffer
        public class CircularBuffer<T>
        {
            private T[] buffer;
            private int writePosition = 0;
            private int readPosition = 0;

            public CircularBuffer(int size)
            {
                buffer = new T[size];
            }

            public void Write(T item)
            {
                buffer[writePosition % buffer.Length] = item;
                writePosition++;
            }

            public T Read()
            {
                T item = buffer[readPosition % buffer.Length];
                readPosition++;
                return item;
            }
        }

        // 3. Color pattern generator
        public class ColorPattern
        {
            private readonly string[] colors = { "Red", "Green", "Blue" };

            public string GetColor(int position)
            {
                return colors[position % colors.Length];
            }
        }
    }

    // Common patterns and tricks with modulo
    public class ModuloPatterns
    {
        // 1. Ensure positive modulo (different from remainder)
        public int PositiveModulo(int n, int m)
        {
            return ((n % m) + m) % m;
            // Works with negative numbers:
            // -1 % 5 becomes 4
            // -3 % 5 becomes 2
        }

        // 2. Check if number is power of 2
        public bool IsPowerOfTwo(int n)
        {
            return n > 0 && (n & (n - 1)) == 0;
            // Alternative using modulo:
            // return n > 0 && (n & -n) == n;
        }

        // 3. Range constraint
        public int WrapValue(int value, int min, int max)
        {
            int range = max - min + 1;
            return min + ((value - min) % range + range) % range;
        }
    }
}

/*
Key Points about Modulo:

1. Basic Operation:
   - Returns remainder of division
   - a % b where a is divided by b
   - Result has same sign as dividend (in C#)

2. Common Uses:
   - Check divisibility
   - Create circular patterns
   - Wrap around numbers
   - Distribute items evenly
   - Time calculations
   - Array index wrapping

3. Performance Considerations:
   - Modulo is more expensive than addition/subtraction
   - For powers of 2, use bitwise AND instead
   - Can be optimized by compiler in simple cases

4. Common Mistakes:
   - Negative number handling
   - Assuming it always gives positive results
   - Using when bitwise operations would be faster
*/