/*
Key Concepts:
- Delegates are type-safe function pointers
- Events are a wrapper around delegates that:
  * Prevent subscribers from being cleared
  * Can only be invoked from within the declaring class
  * Follow the Observer pattern
- Different types of delegates:
  * Action<T> - methods that return void
  * Func<T> - methods that return a value
  * Predicate<T> - methods that return bool
*/

public class DelegatesAndEventsDemo
{
    // Delegate declaration
    public delegate void CustomDelegate(string message);

    // Event based on custom delegate
    public event CustomDelegate OnCustomEvent;

    // Event based on EventHandler
    public event EventHandler<CustomEventArgs> OnStandardEvent;

    // Modern delegate usage with Action
    public Action<string> ModernDelegate { get; set; }

    // Example of event pattern
    private class CustomEventArgs : EventArgs
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public void RaiseEvents()
    {
        // Raise custom event
        OnCustomEvent?.Invoke("Custom event raised");

        // Raise standard event
        OnStandardEvent?.Invoke(this, new CustomEventArgs 
        { 
            Message = "Standard event raised",
            Timestamp = DateTime.UtcNow 
        });
    }

    // Example of multicast delegate
    public void DemonstrateMulticastDelegate()
    {
        CustomDelegate del = (msg) => Console.WriteLine($"First handler: {msg}");
        del += (msg) => Console.WriteLine($"Second handler: {msg}");
        del -= (msg) => Console.WriteLine($"First handler: {msg}");

        del("Hello"); // Only second handler executes
    }

    // Example of delegate with return value
    public Func<int, int, int> MathOperation { get; set; }

    public void UseMathOperation()
    {
        // Addition
        MathOperation = (a, b) => a + b;
        var sum = MathOperation(5, 3); // 8

        // Multiplication
        MathOperation = (a, b) => a * b;
        var product = MathOperation(5, 3); // 15
    }
}

// Practical Example: Event-driven architecture
public class OrderProcessor
{
    public event EventHandler<OrderEventArgs> OrderProcessed;
    public event EventHandler<OrderEventArgs> OrderFailed;

    public async Task ProcessOrder(Order order)
    {
        try
        {
            await ValidateOrder(order);
            await ProcessPayment(order);
            await UpdateInventory(order);
            
            OnOrderProcessed(new OrderEventArgs 
            { 
                OrderId = order.Id,
                ProcessedDate = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            OnOrderFailed(new OrderEventArgs
            {
                OrderId = order.Id,
                Error = ex.Message
            });
        }
    }

    protected virtual void OnOrderProcessed(OrderEventArgs e)
    {
        OrderProcessed?.Invoke(this, e);
    }

    protected virtual void OnOrderFailed(OrderEventArgs e)
    {
        OrderFailed?.Invoke(this, e);
    }
}

public class OrderEventArgs : EventArgs
{
    public int OrderId { get; set; }
    public DateTime ProcessedDate { get; set; }
    public string Error { get; set; }
}

// Example usage of the OrderProcessor
public class OrderSystem
{
    public async Task RunOrderSystem()
    {
        var processor = new OrderProcessor();

        // Subscribe to events
        processor.OrderProcessed += (sender, e) => 
        {
            Console.WriteLine($"Order {e.OrderId} processed at {e.ProcessedDate}");
        };

        processor.OrderFailed += (sender, e) => 
        {
            Console.WriteLine($"Order {e.OrderId} failed: {e.Error}");
        };

        // Process orders
        var order = new Order { Id = 1 };
        await processor.ProcessOrder(order);
    }
}