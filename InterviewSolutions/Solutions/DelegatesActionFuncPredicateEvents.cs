/*
Delegates: These are type-safe function pointers. In the example, we see:

Custom delegate declaration (CustomMessageDelegate)
Multicast delegates (adding multiple methods to one delegate)
Built-in delegate types


Action<T>:

Represents a method that takes parameters but returns void
Examples show usage with no parameters, one parameter, and multiple parameters
Commonly used for callbacks and event handlers


Func<T>:

Represents a method that takes parameters and returns a value
Last type parameter is always the return type
Examples show different parameter combinations and return types


Predicate<T>:

Special case of Func<T, bool>
Used for methods that test a condition
Commonly used with collection methods like List<T>.FindAll()


Events:

Built on top of delegates
Implement the publisher-subscriber pattern
Show both custom and standard EventHandler patterns
Include practical examples with error handling
*/

using System;
using System.Collections.Generic;

namespace DelegatesAndEventsDemo
{
    // Custom delegate declaration
    public delegate void CustomMessageDelegate(string message);

    public class DelegatesDemo
    {
        // Event declaration using the custom delegate
        public event CustomMessageDelegate OnMessageReceived;

        // Event using built-in EventHandler delegate
        public event EventHandler<string> OnStandardMessage;

        public void RunDelegateExamples()
        {
            Console.WriteLine("=== Delegate Examples ===\n");

            // 1. Basic Delegate Example
            CustomMessageDelegate messageDelegate = PrintMessage;
            // Adding another method to the delegate chain (multicast)
            messageDelegate += PrintUpperMessage;
            
            // Invoking delegate
            messageDelegate("Hello from basic delegate!");

            // 2. Action<T> Examples
            Console.WriteLine("\n=== Action<T> Examples ===\n");
            
            // Action with no parameters
            Action simpleAction = () => Console.WriteLine("Simple action with no parameters");
            simpleAction();

            // Action with one parameter
            Action<string> printAction = (message) => Console.WriteLine($"Action prints: {message}");
            printAction("Hello from Action!");

            // Action with multiple parameters
            Action<string, int> complexAction = (message, number) => 
                Console.WriteLine($"Message: {message}, Number: {number}");
            complexAction("Complex action", 42);

            // 3. Func<T> Examples
            Console.WriteLine("\n=== Func<T> Examples ===\n");
            
            // Func returning string
            Func<string> getMessage = () => "Hello from Func!";
            Console.WriteLine(getMessage());

            // Func with input parameter and return value
            Func<string, string> transformMessage = (input) => $"Transformed: {input.ToUpper()}";
            Console.WriteLine(transformMessage("hello world"));

            // Func with multiple parameters
            Func<int, int, string> combineNumbers = (x, y) => $"Sum of {x} and {y} is {x + y}";
            Console.WriteLine(combineNumbers(10, 20));

            // 4. Predicate<T> Examples
            Console.WriteLine("\n=== Predicate<T> Examples ===\n");
            
            // Simple predicate
            Predicate<int> isEven = (number) => number % 2 == 0;
            Console.WriteLine($"Is 4 even? {isEven(4)}");
            Console.WriteLine($"Is 7 even? {isEven(7)}");

            // Predicate with complex type
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var evenNumbers = numbers.FindAll(isEven);
            Console.WriteLine($"Even numbers: {string.Join(", ", evenNumbers)}");
        }

        // Event raising method
        public void RaiseEvents()
        {
            Console.WriteLine("\n=== Event Examples ===\n");

            // Raising custom event
            OnMessageReceived?.Invoke("Custom event triggered!");

            // Raising standard event
            OnStandardMessage?.Invoke(this, "Standard event triggered!");
        }

        // Helper methods for delegate example
        private void PrintMessage(string message)
        {
            Console.WriteLine($"Regular print: {message}");
        }

        private void PrintUpperMessage(string message)
        {
            Console.WriteLine($"Upper print: {message.ToUpper()}");
        }
    }

    // Example class demonstrating practical usage
    public class PracticalExample
    {
        public void DemonstrateRealWorldUsage()
        {
            var processor = new DataProcessor();

            // Subscribe to events
            processor.OnDataProcessed += HandleProcessedData;
            processor.OnError += HandleError;

            // Process some data
            processor.ProcessData(new[] { 1, 2, 3, 4, 5 });
        }

        private void HandleProcessedData(object sender, ProcessedDataEventArgs e)
        {
            Console.WriteLine($"Processed Result: {e.Result}");
        }

        private void HandleError(object sender, string errorMessage)
        {
            Console.WriteLine($"Error: {errorMessage}");
        }
    }

    // Custom EventArgs class
    public class ProcessedDataEventArgs : EventArgs
    {
        public int Result { get; }
        public ProcessedDataEventArgs(int result) => Result = result;
    }

    // Data processor class showing practical event usage
    public class DataProcessor
    {
        // Event using custom EventArgs
        public event EventHandler<ProcessedDataEventArgs> OnDataProcessed;
        
        // Event using Action<T>
        public event Action<object, string> OnError;

        public void ProcessData(int[] data)
        {
            try
            {
                var result = data.Sum();
                // Raise success event
                OnDataProcessed?.Invoke(this, new ProcessedDataEventArgs(result));
            }
            catch (Exception ex)
            {
                // Raise error event
                OnError?.Invoke(this, ex.Message);
            }
        }
    }
}