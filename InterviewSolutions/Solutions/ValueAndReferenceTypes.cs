/*
    Q1: "What's the difference between value types and reference types in C#?"
    - Looking for understanding of stack vs heap
    - Memory allocation knowledge
    - Performance implications
    */

 /*   Strong Answer:
"Value types and reference types differ in how they're stored and passed:

Value types (struct, enum, primitive types):
- Stored directly on the stack
- Contain the actual value
- Each variable has its own copy
- Passed by copy by default
- Cannot be null unless declared as nullable
- Faster for small types due to stack allocation
- Good for small, immutable data

Reference types (class, interface, delegate, array):
- Stored on the heap with a reference on the stack
- Contains a reference to the data
- Multiple variables can reference same data
- Passed by reference
- Can be null
- Subject to garbage collection
- Better for larger objects and inheritance

*/

public class TypeBehaviorDemo
{
    public void DemonstrateTypes()
    {
        // Value type example
        int x = 10;
        int y = x; // Creates a copy
        y = 20; // x is still 10

        // Reference type example
        var list1 = new List<int> { 1, 2, 3 };
        var list2 = list1; // Both reference same object
        list2.Add(4); // Both lists now have 4
    }
}

// 1. ref parameter example
    // ref requires the variable to be initialized before passing
    public static void ModifyWithRef(ref int number)
    {
        number *= 2; // Modifies the original variable
    }

// 2. out parameter example
    // out parameters must be assigned within the method
    public static bool TryDivide(int dividend, int divisor, out int result)
    {
        result = 0; // Must assign a value
        if (divisor == 0)
            return false;

        result = dividend / divisor;
        return true;
    }

// 3. 'in' parameter example (readonly reference)
    // Prevents modification of the parameter while still avoiding copying
    public static double CalculateDistance(in Point point)
    {
        // point.X = 20; // This would cause a compilation error
        return Math.Sqrt(point.X * point.X + point.Y * point.Y);
    }