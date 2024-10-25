/*
Method overloading allows multiple methods in the same class with the same name but different parameters.

Key points about Overloading:
- Must have different parameter types or number of parameters
- Can have different return types, but this alone is not enough to overload
- Happens in the same class
- Determined at compile time (static binding)
- Method signature must be different

Overloading:
- Same method name, different parameters
- In same class
- Compile-time polymorphism
- No inheritance required
- No virtual/override keywords needed
*/

public class Calculator
{
    // Method Overloading examples
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)   // Different number of parameters
    {
        return a + b + c;
    }

    public double Add(double a, double b)  // Different parameter types
    {
        return a + b;
    }

    public string Add(string a, string b)  // Different parameter types
    {
        return a + b;
    }
}