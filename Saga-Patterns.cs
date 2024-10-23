// REAL-WORLD APPLICATION PATTERNS

public class RealWorldPatterns
{
    // Pattern 1: Saga Pattern for Distributed Transactions
    public class OrderSaga
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public async Task ProcessOrder(OrderRequest request)
        {
            var saga = new SagaBuilder()
                .AddStep("ValidateInventory", 
                    () => ValidateInventoryAsync(request),
                    () => RollbackInventoryAsync(request))
                .AddStep("ProcessPayment", 
                    () => ProcessPaymentAsync(request),
                    () => RefundPaymentAsync(request))
                .AddStep("UpdateOrderStatus", 
                    () => UpdateOrderStatusAsync(request),
                    () => CancelOrderAsync(request))
                .Build();

            await saga.ExecuteAsync();
        }
    }

    // Pattern 2: Event Sourcing
    public class EventSourcedAggregate
    {
        private readonly List<DomainEvent> _changes = new();
        private readonly List<DomainEvent> _history = new();

        public void Apply(DomainEvent @event)
        {
            _changes.Add(@event);
            When(@event);
        }

        public async Task SaveAsync(IEventStore eventStore)
        {
            await eventStore.SaveEventsAsync(_changes);
            _history.AddRange(_changes);
            _changes.Clear();
        }
    }
}