// Basic usage
int age = 20;
string message = age >= 18 ? "Adult" : "Minor";

// With method calls
string name = "John";
string greeting = name.Length > 0 ? $"Hello, {name}!" : "Hello, stranger!";

// Nested ternary (though use sparingly for readability)
int score = 85;
string grade = score >= 90 ? "A" : 
               score >= 80 ? "B" : 
               score >= 70 ? "C" : "F";

// Can be used in assignments, method arguments, or return statements
return isValid ? ProcessData() : HandleError();