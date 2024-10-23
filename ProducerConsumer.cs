// Implement a producer-consumer pattern using async/await
public class ProducerConsumer
{
    private readonly Channel<int> _channel;

    public ProducerConsumer()
    {
        _channel = Channel.CreateUnbounded<int>();
    }

    public async Task ProduceAsync(CancellationToken token)
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            await _channel.Writer.WriteAsync(random.Next(100), token);
            await Task.Delay(100, token);
        }
    }

    public async Task ConsumeAsync(CancellationToken token)
    {
        await foreach (var item in _channel.Reader.ReadAllAsync(token))
        {
            Console.WriteLine($"Consumed: {item}");
        }
    }
}