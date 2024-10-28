// Implemention a producer-consumer pattern using async/await
// You can see a much better, more complete message bus system
// in my GolangMessageBus example located here: https://github.com/edancain/GolangMessageBus

using System.Threading.Channels;

public class PublisherSubscriber
{
    private readonly Channel<int> _channel;

    public PublisherSubscriber()
    {
        _channel = Channel.CreateUnbounded<int>();
    }

    public async Task PublishAsync(CancellationToken token)
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            await _channel.Writer.WriteAsync(random.Next(100), token);
            await Task.Delay(100, token);
        }
    }

    public async Task SubscribeAsync(CancellationToken token)
    {
        await foreach (var item in _channel.Reader.ReadAllAsync(token))
        {
            Console.WriteLine($"Consumed: {item}");
        }
    }
}