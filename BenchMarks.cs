using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class QueryPerformanceBenchmarks
{
    private readonly List<int> _numbers = Enumerable.Range(1, 1000000).ToList();
    private readonly DbContext _context;

    [Benchmark]
    public async Task QueryableWithProperFiltering()
    {
        var result = await _context.Users
            .Where(u => u.Age > 18)
            .Take(100)
            .ToListAsync();
    }

    [Benchmark]
    public async Task EnumerableWithImproperFiltering()
    {
        var result = _context.Users
            .AsEnumerable()
            .Where(u => u.Age > 18)
            .Take(100)
            .ToList();
    }

    [Benchmark]
    public List<int> LinqWhereToList()
    {
        return _numbers.Where(n => n % 2 == 0).ToList();
    }

    [Benchmark]
    public List<int> ForEachWithList()
    {
        var result = new List<int>();
        foreach (var n in _numbers)
        {
            if (n % 2 == 0)
                result.Add(n);
        }
        return result;
    }
}