
Console.WriteLine("Hello, Parallel.ForEachAsync!");

Console.WriteLine($"CPU: {Environment.ProcessorCount} rdzeni");


var items = Enumerable.Range(1, 20);

var tasks = items.Select(i => MyExpensiveAsync(i));
await Task.WhenAll(tasks); // 🔥 może uruchomić 1000 zadań na raz! 


static async Task MyExpensiveAsync(int item)
{
    Console.WriteLine($"Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    // Symulacja pracy CPU
    Thread.SpinWait(1_000_000);

    // Sztuczne opóźnienie — symulacja pracy I/O lub obliczeń
    await Task.Delay(500);

    Console.WriteLine($"Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}
