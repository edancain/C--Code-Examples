
public class QueryOptimizationExamples
{
    private readonly DbContext _context;

    // Example 1: Efficient Paging with Complex Queries
    public async Task<(List<OrderDto> Orders, int TotalCount)> GetPagedOrdersEfficient(
        int pageSize, 
        int pageNumber,
        DateTime startDate)
    {
        var query = _context.Orders
            .Where(o => o.OrderDate >= startDate)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .AsNoTracking(); // Performance optimization for read-only data

        var totalCount = await query.CountAsync();

        var orders = await query
            .OrderByDescending(o => o.OrderDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerName = o.Customer.Name,
                TotalAmount = o.OrderItems.Sum(oi => oi.Price * oi.Quantity),
                ItemCount = o.OrderItems.Count
            })
            .ToListAsync();

        return (orders, totalCount);
    }

    // Example 2: Complex Group and Aggregate Operations
    public async Task<List<SalesReport>> GetSalesReport(int year)
    {
        return await _context.Orders
            .Where(o => o.OrderDate.Year == year)
            .GroupBy(o => new 
            { 
                Month = o.OrderDate.Month,
                Category = o.Category
            })
            .Select(g => new SalesReport
            {
                Month = g.Key.Month,
                Category = g.Key.Category,
                TotalSales = g.Sum(o => o.Amount),
                OrderCount = g.Count(),
                AverageOrderValue = g.Average(o => o.Amount),
                MaxOrder = g.Max(o => o.Amount)
            })
            .OrderBy(r => r.Month)
            .ThenBy(r => r.Category)
            .ToListAsync();
    }
}