// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Thread Pool!");

Console.WriteLine($"ProcessorCount {Environment.ProcessorCount}");

var result = ThreadPool.SetMaxThreads(12, 12);

Console.WriteLine($"SetMaxThreads: {result}");

// Ważne! Ustaw <UseWindowsThreadPool>false</UseWindowsThreadPool> w pliku projektu csproj
for (int i = 0; i <100; i++)
{
    ThreadPool.QueueUserWorkItem(_ => DownloadImage(Random.Shared.Next(1, 1000)));
}

Console.WriteLine($"[Thread Pool] Thread# {Thread.CurrentThread.ManagedThreadId} finished.");

Console.WriteLine("Press any key");
Console.ReadLine();

static void DownloadImage(int index)
{
    Console.WriteLine($"[Thread Pool] Thread# {Thread.CurrentThread.ManagedThreadId} image #{index}... downloading ");

    // Symulacja dłużej trwającej operacji
    Thread.CurrentThread.Join(Random.Shared.Next(100, 1000));

    // using var client = new WebClient();    
    /// var bytes = client.DownloadData("https://picsum.photos/700");
    // File.WriteAllBytes($"image{index}.jpg", bytes);

    Console.WriteLine($"[Thread Pool] Thread# {Thread.CurrentThread.ManagedThreadId} #{index} downloaded.");
}


