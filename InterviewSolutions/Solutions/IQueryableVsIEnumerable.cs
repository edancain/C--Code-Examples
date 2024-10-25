/*
Detailed Comparison:
- IEnumerable<T> operates on in-memory data
- IQueryable<T> represents a query that hasn't been executed yet
- IQueryable<T> can translate LINQ expressions into other query languages (like SQL)
- Performance implications are significant when working with databases
*/

// Example showing the difference:
public class QueryableVsEnumerable
{
    private readonly DbContext _context;

    public async Task DemonstrateQueryableDifference()
    {
        // IQueryable example - generates SQL, executes at database
        var queryable = _context.Users
            .Where(u => u.Age > 18)
            .Select(u => new { u.Name, u.Email });
        // SQL generated: SELECT Name, Email FROM Users WHERE Age > 18

        // IEnumerable example - brings all data to memory first
        var enumerable = _context.Users.AsEnumerable()
            .Where(u => u.Age > 18)
            .Select(u => new { u.Name, u.Email });
        // SQL generated: SELECT * FROM Users
        // Filtering happens in memory

        // Performance comparison example
        await ComparePerformance();
    }

    private async Task ComparePerformance()
    {
        // Bad practice - loads entire table
        var enumerableResult = _context.Users
            .AsEnumerable()
            .Where(u => u.Age > 18)
            .Take(10)
            .ToList();

        // Good practice - translates to efficient SQL
        var queryableResult = await _context.Users
            .Where(u => u.Age > 18)
            .Take(10)
            .ToListAsync();
    }
}