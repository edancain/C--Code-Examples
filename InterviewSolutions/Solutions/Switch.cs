switch (number)
{
    case int n when n > 0:
        Console.WriteLine("Positive");
        break;
    case int n when n < 0:
        Console.WriteLine("Negative");
        break;
    case 0:
        Console.WriteLine("Zero");
        break;
}


switch (number)
{
    case int n when n > 0:
        Console.WriteLine("Positive");
        break;
    case int n when n < 0:
        Console.WriteLine("Negative");
        break;
    case 0:
        Console.WriteLine("Zero");
        break;
}

switch (person)
{
    case Student s when s.GradeAverage > 90:
        Console.WriteLine("Honor Student");
        break;
    case Student s when s.GradeAverage < 70:
        Console.WriteLine("Needs Help");
        break;
    case Teacher t when t.YearsExperience > 10:
        Console.WriteLine("Senior Teacher");
        break;
}