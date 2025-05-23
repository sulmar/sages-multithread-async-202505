Console.WriteLine("Hello, Parallel.Invoke!");
Console.WriteLine($"CPU: {Environment.ProcessorCount} rdzeni");

Parallel.Invoke(
    () => ProcessItem(1), 
    () => DoWork(1),
    () => ThirdAction(199));



static void ProcessItem(int item)
{
    Console.WriteLine($"ProcessItem Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    Thread.SpinWait(1_000_000); // CPU-bound

    Thread.Sleep(500); // Symulacja I/O lub opóźnienia (blokująca)

    Console.WriteLine($"ProcessItem Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}


static void DoWork(int item)
{
    Console.WriteLine($"DoWork Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    Thread.SpinWait(1_000_000); // CPU-bound

    Thread.Sleep(500); // Symulacja I/O lub opóźnienia (blokująca)

    Console.WriteLine($"DoWork Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}

static void ThirdAction(int item)
{
    Console.WriteLine($"ThirdAction Start {item} na wątku {Thread.CurrentThread.ManagedThreadId}");

    Thread.SpinWait(1_000_000); // CPU-bound

    Thread.Sleep(500); // Symulacja I/O lub opóźnienia (blokująca)

    Console.WriteLine($"ThirdAction Zakończono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}