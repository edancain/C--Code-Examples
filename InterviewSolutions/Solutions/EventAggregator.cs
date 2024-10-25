// How to implement a custom event aggregator
public class EventAggregator
{
    private readonly Dictionary<Type, List<object>> _handlers = new();
    private readonly object _lock = new();

    public void Subscribe<T>(Action<T> handler)
    {
        lock (_lock)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<object>();
            _handlers[type].Add(handler);
        }
    }

    public void Publish<T>(T eventData)
    {
        lock (_lock)
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type)) return;

            foreach (var handler in _handlers[type].OfType<Action<T>>())
            {
                handler(eventData);
            }
        }
    }
}