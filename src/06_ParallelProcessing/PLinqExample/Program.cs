
using System.Diagnostics;

Console.WriteLine("Hello, Linq!");


var numbers = Enumerable.Range(1, 100_000);

Stopwatch stopwatch = Stopwatch.StartNew();

var primes = numbers
    .AsParallel()
    .WithDegreeOfParallelism(Environment.ProcessorCount / 2)
    .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
    .Where(IsPrime)
    .AsSequential()
    .ToList();

stopwatch.Stop();

Console.WriteLine($"Executing time: {stopwatch.ElapsedMilliseconds} ms");
Console.WriteLine($"Znaleziono {primes.Count} liczb pierwszych.");
Console.WriteLine($"Pierwsze 10: {string.Join(", ", primes.Take(10))}");


static bool IsPrime(int number)
{
    if (number <= 1) return false;
    if (number == 2) return true;
    if (number % 2 == 0) return false;

    int boundary = (int)Math.Floor(Math.Sqrt(number));

    for (int i = 3; i <= boundary; i += 2)
    {
        if (number % i == 0) return false;
    }

    return true;
}
