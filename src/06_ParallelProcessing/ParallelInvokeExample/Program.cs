Console.WriteLine("Hello, Parallel.Invoke!");
Console.WriteLine($"CPU: {Environment.ProcessorCount} rdzeni");

var items = Enumerable.Range(1, 20);

// Tworzymy listę akcji do wykonania po kolei
var actions = items
    .Select(item => (Action)(() => ProcessItem(item)))
    .ToList();

// Sekwencyjnie wykonujemy wszystkie akcje
foreach (var action in actions)
{
    action();
}

static void ProcessItem(int item)
{
    Console.WriteLine($"Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    Thread.SpinWait(1_000_000); // CPU-bound

    Thread.Sleep(500); // Symulacja I/O lub opóźnienia (blokująca)

    Console.WriteLine($"Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}