/*Interfaces in C# are contracts that define a set of members (methods, properties, events) that implementing classes must provide. Think of them as a blueprint that classes must follow. They're particularly useful for:

Defining common behavior across different classes
Enabling polymorphism
Supporting loose coupling in your code

*/

// IEnumerable is an interface
public interface IEnumerable<T>
{
    IEnumerator<T> GetEnumerator();
}

// Usage example
public class MyCollection : IEnumerable<string>
{
    private List<string> items = new List<string>();

    public IEnumerator<string> GetEnumerator()
    {
        return items.GetEnumerator();
    }
}

/*Key differences:

IEnumerable is an interface, while Enumerable is a static class containing extension methods
IEnumerable defines the contract for iteration, while Enumerable provides LINQ methods like Where, Select, etc.
You implement IEnumerable in your classes, but you use Enumerable's methods on collections

Practical Example
*/

// Using IEnumerable as a type
IEnumerable<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

// Using Enumerable's extension methods
var evenNumbers = numbers.Where(n => n % 2 == 0);  // Where comes from Enumerable class
var doubled = numbers.Select(n => n * 2);          // Select comes from Enumerable class