
Console.WriteLine("Hello, Parallel.ForAsync!");

// await RunWithAsync();

var options = new ParallelOptions
{
    MaxDegreeOfParallelism = 4 // Tylko 4 zadania na raz
};


await Parallel.ForAsync(0, 20, options, async (i, cancellationToken) => await DoWorkAsync(i));



static async Task RunWithAsync()
{
    var tasks = new List<Task>();

    // TODO: dodaj ograniczenie ilości wątków za pomocą SemaphoreSlim

    for (int i = 0; i < 20; i++)
    {
        int taskId = i; // zabezpieczenie przed modyfikacją zmiennej w pętli
        tasks.Add(DoWorkAsync(taskId));
    }

    await Task.WhenAll(tasks);
}



static async Task DoWorkAsync(int id)
{
    Console.WriteLine($"🔧 Start {id} na wątku {Thread.CurrentThread.ManagedThreadId}");
    await Task.Delay(300); // I/O
    Console.WriteLine($"✅ Koniec {id} na wątku {Thread.CurrentThread.ManagedThreadId}");
}