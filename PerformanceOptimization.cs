// COMPLEX PERFORMANCE OPTIMIZATION SCENARIOS

public class PerformanceOptimizations
{
    // Scenario 1: Memory-efficient large data processing
    public class StreamProcessor
    {
        public async IAsyncEnumerable<ProcessedData> ProcessLargeDataset(
            IAsyncEnumerable<RawData> source)
        {
            await foreach (var item in source)
            {
                if (ShouldProcess(item))
                {
                    yield return await ProcessItemAsync(item);
                }
            }
        }

        private bool ShouldProcess(RawData data)
        {
            // Filtering logic
            return true;
        }

        private async Task<ProcessedData> ProcessItemAsync(RawData data)
        {
            // Processing logic
            return new ProcessedData();
        }
    }
}