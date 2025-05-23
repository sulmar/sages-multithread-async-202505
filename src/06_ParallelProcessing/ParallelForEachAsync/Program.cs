
Console.WriteLine("Hello, Parallel.ForEachAsync!");

Console.WriteLine($"CPU: {Environment.ProcessorCount} rdzeni");


//CuttingStockProblemTests cuttingStockProblemTests = new CuttingStockProblemTests();
//cuttingStockProblemTests.Solve_WhenBruteForceParallelStrategy_ShouldReturnsSolutions();

var items = Enumerable.Range(1, 20);

var options = new ParallelOptions 
{ 
    MaxDegreeOfParallelism = 4 // Tylko 4 zadania na raz
};

await Parallel.ForEachAsync(items, options, async (item, cancellationToken)  =>
{
    await MyExpensiveAsync(item);
});

// var tasks = items.Select(i => MyExpensiveAsync(i));
// await Task.WhenAll(tasks); // 🔥 może uruchomić 1000 zadań na raz! 


static async Task MyExpensiveAsync(int item)
{
    Console.WriteLine($"Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    // Symulacja pracy CPU
    Thread.SpinWait(1_000_000);

    // Sztuczne opóźnienie — symulacja pracy I/O lub obliczeń
    await Task.Delay(500);

    Console.WriteLine($"Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}
