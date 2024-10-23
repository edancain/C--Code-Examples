public class FizzBuzzExamples
{

     /*
        FizzBuzz Rules:
        1. Print numbers from 1 to n
        2. For multiples of 3, print "Fizz" instead of the number
        3. For multiples of 5, print "Buzz" instead of the number
        4. For multiples of both 3 and 5, print "FizzBuzz"
    */
    // 1. Basic Implementation
    public List<string> BasicFizzBuzz(int n)
    {
        var result = new List<string>();
        
        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                result.Add("FizzBuzz");
            else if (i % 3 == 0)
                result.Add("Fizz");
            else if (i % 5 == 0)
                result.Add("Buzz");
            else
                result.Add(i.ToString());
        }
        
        return result;
    }

    // 2. String Concatenation Implementation
    public List<string> StringConcatFizzBuzz(int n)
    {
        var result = new List<string>();
        
        for (int i = 1; i <= n; i++)
        {
            string current = "";
            
            if (i % 3 == 0) current += "Fizz";
            if (i % 5 == 0) current += "Buzz";
            
            if (string.IsNullOrEmpty(current))
                current = i.ToString();
                
            result.Add(current);
        }
        
        return result;
    }

    // 3. LINQ Implementation
    public List<string> LinqFizzBuzz(int n)
    {
        return Enumerable.Range(1, n)
            .Select(i => i % 3 == 0 && i % 5 == 0 ? "FizzBuzz" :
                        i % 3 == 0 ? "Fizz" :
                        i % 5 == 0 ? "Buzz" :
                        i.ToString())
            .ToList();
    }

    // 4. Dictionary-based Implementation
    public List<string> DictionaryFizzBuzz(int n)
    {
        var rules = new Dictionary<int, string>
        {
            {3, "Fizz"},
            {5, "Buzz"}
        };

        return Enumerable.Range(1, n)
            .Select(i =>
            {
                var result = string.Concat(
                    rules.Where(rule => i % rule.Key == 0)
                         .Select(rule => rule.Value)
                );
                
                return string.IsNullOrEmpty(result) ? i.ToString() : result;
            })
            .ToList();
    }

    // 5. Extensible Implementation
    public class ExtensibleFizzBuzz
    {
        private readonly List<(int divisor, string word)> _rules;

        public ExtensibleFizzBuzz()
        {
            _rules = new List<(int, string)>
            {
                (3, "Fizz"),
                (5, "Buzz")
            };
        }

        public void AddRule(int divisor, string word)
        {
            _rules.Add((divisor, word));
        }

        public List<string> Generate(int n)
        {
            return Enumerable.Range(1, n)
                .Select(i =>
                {
                    var result = string.Concat(
                        _rules.Where(rule => i % rule.divisor == 0)
                              .Select(rule => rule.word)
                    );
                    
                    return string.IsNullOrEmpty(result) ? i.ToString() : result;
                })
                .ToList();
        }
    }

    // 6. Performance-Optimized Implementation
    public List<string> OptimizedFizzBuzz(int n)
    {
        var result = new List<string>(n); // Pre-allocate capacity
        
        for (int i = 1; i <= n; i++)
        {
            bool divisibleBy3 = i % 3 == 0;
            bool divisibleBy5 = i % 5 == 0;
            
            if (divisibleBy3 && divisibleBy5)
                result.Add("FizzBuzz");
            else if (divisibleBy3)
                result.Add("Fizz");
            else if (divisibleBy5)
                result.Add("Buzz");
            else
                result.Add(i.ToString());
        }
        
        return result;
    }

    // 7. Async Implementation for Large Numbers
    public async Task<List<string>> AsyncFizzBuzz(int n)
    {
        const int batchSize = 10000;
        var result = new List<string>(n);
        
        var tasks = Enumerable.Range(0, (n + batchSize - 1) / batchSize)
            .Select(batch => Task.Run(() =>
            {
                var batchResults = new List<string>();
                int start = batch * batchSize + 1;
                int end = Math.Min(start + batchSize - 1, n);
                
                for (int i = start; i <= end; i++)
                {
                    if (i % 3 == 0 && i % 5 == 0)
                        batchResults.Add("FizzBuzz");
                    else if (i % 3 == 0)
                        batchResults.Add("Fizz");
                    else if (i % 5 == 0)
                        batchResults.Add("Buzz");
                    else
                        batchResults.Add(i.ToString());
                }
                
                return batchResults;
            }));

        var batches = await Task.WhenAll(tasks);
        return batches.SelectMany(b => b).ToList();
    }
}

// Usage Examples
public class FizzBuzzUsage
{
    public void DemonstrateUsage()
    {
        // Basic usage
        var fizzbuzz = new FizzBuzzExamples();
        var result = fizzbuzz.BasicFizzBuzz(15);
        
        // Expected output:
        // 1, 2, Fizz, 4, Buzz, Fizz, 7, 8, Fizz, Buzz, 11, Fizz, 13, 14, FizzBuzz

        // Extensible usage
        var extensible = new FizzBuzzExamples.ExtensibleFizzBuzz();
        extensible.AddRule(7, "Bazz"); // Add custom rule
        var customResult = extensible.Generate(21);
        
        // Performance comparison
        var sw = new Stopwatch();
        
        sw.Start();
        var basic = fizzbuzz.BasicFizzBuzz(1000000);
        var basicTime = sw.ElapsedMilliseconds;
        
        sw.Restart();
        var optimized = fizzbuzz.OptimizedFizzBuzz(1000000);
        var optimizedTime = sw.ElapsedMilliseconds;
        
        Console.WriteLine($"Basic: {basicTime}ms");
        Console.WriteLine($"Optimized: {optimizedTime}ms");
    }
}
