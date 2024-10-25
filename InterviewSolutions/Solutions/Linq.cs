// Supporting classes
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }
}

public class Department
{
    public string Name { get; set; }
    public string Location { get; set; }
}

// Sample data
var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
var people = new List<Person>
{
    new Person { Name = "Alice", Age = 25, Department = "IT" },
    new Person { Name = "Bob", Age = 30, Department = "HR" },
    new Person { Name = "Charlie", Age = 35, Department = "IT" },
    new Person { Name = "Diana", Age = 28, Department = "Finance" }
};

// 1. Where - Filtering
var evenNumbers = numbers.Where(n => n % 2 == 0);
var youngPeople = people.Where(p => p.Age < 30);

// 2. Select - Projection
var doubled = numbers.Select(n => n * 2);
var names = people.Select(p => p.Name);

// 3. OrderBy - Sorting
var orderedAges = people.OrderBy(p => p.Age);
var orderedByNameDesc = people.OrderByDescending(p => p.Name);

// 4. GroupBy - Grouping
var departmentGroups = people.GroupBy(p => p.Department);

// 5. Aggregate Functions
var sum = numbers.Sum();
var average = numbers.Average();
var count = people.Count();
var oldest = people.Max(p => p.Age);

// 6. First, Single, Last
var firstPerson = people.First();
var lastITPerson = people.Where(p => p.Department == "IT").Last();
var thirtyYearOld = people.SingleOrDefault(p => p.Age == 30);

// 7. Join Example
var departments = new List<Department>
{
    new Department { Name = "IT", Location = "Floor 1" },
    new Department { Name = "HR", Location = "Floor 2" }
};

var employeeLocations = people.Join(
    departments,
    person => person.Department,
    dept => dept.Name,
    (person, dept) => new { 
        person.Name, 
        person.Department, 
        Location = dept.Location 
    }
);

// 8. Method and Query Syntax comparison
// Method syntax
var itStaff = people.Where(p => p.Department == "IT")
                    .OrderBy(p => p.Name)
                    .Select(p => p.Name);

// Query syntax
var itStaffQuery = 
    from p in people
    where p.Department == "IT"
    orderby p.Name
    select p.Name;

/*
Key benefits of LINQ include:

    - Readable and Expressive Code
        Queries are written in a natural, readable way
        Reduces boilerplate code compared to traditional loops
        Can use either method syntax or query syntax

    - Type Safety
        Compile-time type checking prevents runtime errors
        IntelliSense support for better development experience
        Strong typing ensures query correctness

    - Consistent API
        Same syntax works across different data sources (arrays, lists, XML, databases)
        Easy to switch between data sources without changing query logic
        Works with any type implementing IEnumerable<T>

    - Deferred Execution
        Queries are only executed when results are needed
        Improves performance by avoiding unnecessary operations
        Allows for query composition without immediate execution

    - Built-in Query Operations
        Rich set of standard query operators (Where, Select, Join, GroupBy, etc.)
        Common operations like sorting, filtering, and aggregation
        Chainable methods for complex queries

    - Database Integration
        Seamless integration with Entity Framework
        LINQ to SQL for database operations
        Queries can be translated to SQL automatically

