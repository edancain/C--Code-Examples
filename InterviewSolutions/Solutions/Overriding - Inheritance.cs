/*Method Overriding
Method overriding allows a derived class to provide a different 
implementation of a method defined in its base class.

Overriding:
- Same method signature and return type
- In derived class
- Runtime polymorphism
- Requires inheritance
- Requires virtual/override keywords
- Can use 'base' keyword to call parent method
*/
public abstract class Animal
{
    public string Name { get; set; }
    protected int Age { get; set; }

    protected Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Virtual method that can be overridden
    public virtual string MakeSound()
    {
        return "Some generic animal sound";
    }

    // Abstract method that must be implemented by derived classes
    public abstract string Move();

    // Method that will be inherited as-is
    public string GetInfo()
    {
        return $"{Name} is {Age} years old";
    }
}

public class Dog : Animal
{
    public string Breed { get; set; }

    public Dog(string name, int age, string breed) : base(name, age)
    {
        Breed = breed;
    }

    // Override keyword indicates this method overrides the base class method
    public override void MakeSound()
    {
        Console.WriteLine("Woof!");
    }

    // Implement abstract method
    public override string Move()
    {
        return "Running on four legs";
    }

    // Method specific to Dog class
    public string Fetch()
    {
        return $"{Name} is fetching the ball";
    }
}

// Another derived class
public class Bird : Animal
{
    public double WingSpan { get; set; }

    public Bird(string name, int age, double wingSpan) : base(name, age)
    {
        WingSpan = wingSpan;
    }

    // Override virtual method
    public override string MakeSound()
    {
        return "Chirp!";
    }

    // Implement abstract method
    public override string Move()
    {
        return "Flying through the air";
    }
}


// Abstract Classes and Override
public abstract class Shape
{
    // Abstract method must be implemented by derived classes
    public abstract double CalculateArea();

    // Virtual method can be optionally overridden
    public virtual void Display()
    {
        Console.WriteLine("This is a shape");
    }
}

public class Circle : Shape
{
    private double radius;

    public Circle(double r)
    {
        radius = r;
    }

    // Must override abstract method
    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }

    // Optionally override virtual method
    public override void Display()
    {
        Console.WriteLine($"This is a circle with radius {radius}");
    }
}
