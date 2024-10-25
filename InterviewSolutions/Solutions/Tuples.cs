// Tuples in C# are lightweight data structures that group multiple values. 
// There are several ways to work with them:
// Value Tuple (modern syntax, C# 7.0+)
(string name, int age) person = ("John", 25);
Console.WriteLine($"{person.name} is {person.age}");

// Named tuple elements for clarity
var student = (name: "Alice", grade: "A");
Console.WriteLine(student.name);

// Tuple without names (less readable)
var point = ("Home", 42.3, 73.9);
Console.WriteLine(point.Item1); // "Home"


