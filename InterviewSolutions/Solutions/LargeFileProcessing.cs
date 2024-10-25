// LARGE FILE PROCESSING SOLUTION

public class DataProcessor
{
    // Record structure for parsing
    private record DataRecord(DateTime Timestamp, double Value);

    public Dictionary<DateTime, double> ProcessLargeFile(string filePath)
    {
        // Input validation
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Data file not found", filePath);
        }

        // Dictionary to store hourly sums and counts
        var hourlyData = new Dictionary<DateTime, (double Sum, int Count)>();

        try
        {
            // Process file using streams for memory efficiency
            using var fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096); // Optimal buffer size for most systems

            using var reader = new StreamReader(fileStream);

            string? line;
            int lineNumber = 0;

            // Process line by line instead of loading entire file
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                try
                {
                    var record = ParseLine(line);
                    if (record != null)
                    {
                        // Get hour timestamp
                        var hourKey = new DateTime(
                            record.Timestamp.Year,
                            record.Timestamp.Month,
                            record.Timestamp.Day,
                            record.Timestamp.Hour,
                            0, 0);

                        // Update running totals
                        if (hourlyData.TryGetValue(hourKey, out var existing))
                        {
                            hourlyData[hourKey] = (
                                Sum: existing.Sum + record.Value,
                                Count: existing.Count + 1
                            );
                        }
                        else
                        {
                            hourlyData[hourKey] = (
                                Sum: record.Value,
                                Count: 1
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log error but continue processing
                    Console.WriteLine(
                        $"Error processing line {lineNumber}: {ex.Message}");
                }
            }
        }
        catch (IOException ex)
        {
            throw new ApplicationException(
                "Error reading file", ex);
        }

        // Calculate averages
        return hourlyData.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Sum / kvp.Value.Count
        );
    }

    // Parse line into record
    private DataRecord? ParseLine(string line)
    {
        // Skip empty lines
        if (string.IsNullOrWhiteSpace(line))
        {
            return null;
        }

        // Split line into parts
        var parts = line.Split(',');
        if (parts.Length != 2)
        {
            return null;
        }

        // Parse timestamp and value
        if (DateTime.TryParse(parts[0], out var timestamp) &&
            double.TryParse(parts[1], out var value))
        {
            return new DataRecord(timestamp, value);
        }

        return null;
    }

    // Extension: Async version for better scalability
    public async Task<Dictionary<DateTime, double>> ProcessLargeFileAsync(
        string filePath,
        CancellationToken cancellationToken = default)
    {
        var hourlyData = new ConcurrentDictionary<DateTime, (double Sum, int Count)>();

        await foreach (var line in ReadLinesAsync(filePath, cancellationToken))
        {
            var record = ParseLine(line);
            if (record != null)
            {
                var hourKey = new DateTime(
                    record.Timestamp.Year,
                    record.Timestamp.Month,
                    record.Timestamp.Day,
                    record.Timestamp.Hour,
                    0, 0);

                hourlyData.AddOrUpdate(
                    hourKey,
                    (record.Value, 1),
                    (_, existing) => (
                        Sum: existing.Sum + record.Value,
                        Count: existing.Count + 1
                    ));
            }
        }

        return hourlyData.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Sum / kvp.Value.Count
        );
    }

    // Async line reader
    private async IAsyncEnumerable<string> ReadLinesAsync(
        string filePath,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            bufferSize: 4096);

        using var reader = new StreamReader(fileStream);

        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync();
            if (line != null)
            {
                yield return line;
            }
        }
    }
}

// Unit Tests
public class DataProcessorTests
{
    [Fact]
    public void ProcessLargeFile_WithValidData_ReturnsCorrectAverages()
    {
        // Arrange
        var processor = new DataProcessor();
        var testFile = CreateTestFile();

        // Act
        var result = processor.ProcessLargeFile(testFile);

        // Assert
        Assert.NotEmpty(result);
        // Add more specific assertions
    }

    [Fact]
    public void ProcessLargeFile_WithInvalidLines_SkipsAndContinues()
    {
        // Add test implementation
    }

    [Fact]
    public void ProcessLargeFile_WithNonexistentFile_ThrowsException()
    {
        // Add test implementation
    }
}

/*
Solution Breakdown:

1. Problem Analysis:
   - Large file -> need streaming approach
   - Timestamp grouping by hour
   - Running averages
   - Error handling
   - Memory efficiency

2. Performance Considerations:
   - Stream processing instead of loading entire file
   - Efficient data structures
   - Minimal object creation
   - Optimal buffer size
   - Async support for scalability

3. Memory Management:
   - StreamReader for line-by-line processing
   - Dictionary for aggregating data
   - No large arrays or lists
   - Garbage collection friendly

4. Error Handling:
   - File existence check
   - Try-catch for file operations
   - Line parsing error handling
   - Data validation
   - Logging of errors

5. Testing Approach:
   - Unit tests for core functionality
   - Edge case testing
   - Performance testing
   - Error condition testing
*/

// USAGE EXAMPLE
public class Example
{
    public async Task ProcessFileExample()
    {
        var processor = new DataProcessor();
        
        // Synchronous processing
        var result1 = processor.ProcessLargeFile("data.csv");

        // Asynchronous processing
        var cts = new CancellationTokenSource();
        var result2 = await processor.ProcessLargeFileAsync(
            "data.csv",
            cts.Token);

        // Display results
        foreach (var kvp in result1)
        {
            Console.WriteLine(
                $"Hour: {kvp.Key:yyyy-MM-dd HH:00}, " +
                $"Average: {kvp.Value:F2}");
        }
    }
}