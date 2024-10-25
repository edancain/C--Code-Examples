// Reflection is a powerful feature that allows you to inspect types, methods, and properties at runtime:


using System;
using System.Reflection;

public class Person
{
    public string Name { get; set; }
    private int age;
    
    public void SayHello()
    {
        Console.WriteLine($"Hello from {Name}");
    }
}

// Reflection examples
Type type = typeof(Person);

// Get properties
PropertyInfo[] properties = type.GetProperties();
foreach(var prop in properties)
{
    Console.WriteLine($"Property: {prop.Name}, Type: {prop.PropertyType}");
}

// Get methods
MethodInfo[] methods = type.GetMethods();
foreach(var method in methods)
{
    Console.WriteLine($"Method: {method.Name}");
}

// Create instance using reflection
object person = Activator.CreateInstance(type);

// Set property using reflection
PropertyInfo nameProperty = type.GetProperty("Name");
nameProperty.SetValue(person, "John");

// Invoke method using reflection
MethodInfo sayHelloMethod = type.GetMethod("SayHello");
sayHelloMethod.Invoke(person, null);