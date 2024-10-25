/*
Tasks in C# represent asynchronous operations and are a core part of 
the Task Parallel Library (TPL). Here are the key concepts:
*/

// Creating a basic task
Task myTask = Task.Run(() => {
    // Some work here
    Console.WriteLine("Task running");
});

// Awaiting a task
await myTask;


// Task returning a value
Task<int> calculateTask = Task.Run(() => {
    // Some calculation
    return 42;
});

int result = await calculateTask; // Gets the result


// Running tasks in parallel
Task[] tasks = new[]
{
    Task.Run(() => DoWork1()),
    Task.Run(() => DoWork2())
};
await Task.WhenAll(tasks);

// Running tasks sequentially
await Task1();
await Task2();


try
{
    await riskyTask;
}
catch (Exception ex)
{
    // Handle exception
}

// Multiple tasks with exception handling
try
{
    await Task.WhenAll(tasks);
}
catch (AggregateException ae)
{
    foreach (var ex in ae.InnerExceptions)
    {
        // Handle each exception
    }
}

// Cancellation of a task
using var cts = new CancellationTokenSource();
var token = cts.Token;

var task = Task.Run(() => 
{
    while (!token.IsCancellationRequested)
    {
        // Do work
    }
}, token);

// Cancel the task
cts.Cancel();


Common Best Practices
- Always use async/await instead of Task.Result or Task.Wait()
- Handle exceptions appropriately
- Don't create tasks for synchronous work
- Use cancellation tokens for long-running operations
- Consider using TaskCompletionSource for custom async operations

The key thing to remember is that Tasks in C# are used for:
- Asynchronous programming
- Parallel execution
- Background operations
- I/O-bound operations
- CPU-bound operations
- Managing concurrent operations