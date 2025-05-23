
Console.WriteLine("Hello, Parallel.ForEach!");
Console.WriteLine($"CPU: {Environment.ProcessorCount} rdzeni");

var items = Enumerable.Range(1, 20);

foreach (var item in items)
{
    ProcessItem(item);
}

static void ProcessItem(int item)
{
    Console.WriteLine($"Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    Thread.SpinWait(1_000_000); // CPU-bound

    Thread.Sleep(500); // Symulacja I/O lub opóźnienia (blokująca)

    Console.WriteLine($"Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}