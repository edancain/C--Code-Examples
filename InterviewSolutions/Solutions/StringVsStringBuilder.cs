// String Operations
string str1 = "Hello";
string str2 = " World";
string combined = str1 + str2;              // Concatenation
string substring = str1.Substring(0, 3);     // Substring
string replaced = str1.Replace('l', 'w');    // Replace
string upper = str1.ToUpper();              // Convert to upper case

// StringBuilder Operations
StringBuilder sb = new StringBuilder("Hello");
sb.Append(" World");           // Append
sb.Insert(5, " Beautiful");    // Insert
sb.Replace('l', 'w');         // Replace
sb.Remove(5, 3);              // Remove
sb.ToString().ToUpper();      // Convert to string and upper case
sb.Clear();                      // Clear contents
sb.Length = 0;                   // Another way to clear


//Capacity Management

// Initialize with specific capacity
StringBuilder sb = new StringBuilder(32);

// Set maximum capacity
StringBuilder sbMax = new StringBuilder(16, 128); // Initial 16, max 128

// Auto-growing capacity
StringBuilder sbAuto = new StringBuilder();
// Capacity will grow automatically as needed

/*
Use String when:
    1. Working with constant strings
    2. Doing simple string operations
    3. Using string manipulation methods

Use StringBuilder when:
    1. Building strings in loops
*/
StringBuilder loopBuilder = new StringBuilder();
for (int i = 0; i < 100; i++)
{
    loopBuilder.Append(i);
}

// 2. Multiple string manipulations
StringBuilder manipulator = new StringBuilder();
manipulator.Append("Hello")
          .Replace('H', 'h')
          .Insert(0, "Say: ")
          .Remove(5, 2);

// 3. Building large strings
StringBuilder largeBuilder = new StringBuilder(1000);
// ... multiple append operations


// - StringBuilder is mutable

// - Strings are immutable
// When we say strings are immutable, we mean that when you "modify" a string, 
// you're actually creating a new string object in memory - the original string remains unchanged.

// Step 1
string name = "John";
// Memory: "John" (Object 1)

// Step 2
string greeting = "Hello " + name;
// Memory: "John" (Object 1)
//         "Hello John" (Object 2)

// Step 3
greeting = greeting + ", Nice to meet you";
// Memory: "John" (Object 1)
//         "Hello John" (Object 2)
//         "Hello John, Nice to meet you" (Object 3)
