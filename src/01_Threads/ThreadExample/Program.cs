// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Thread!");

List<Thread> threads = new List<Thread>();

for (int i = 0; i < 5; i++)
{
    var thread = new Thread(() => DownloadImage(i));
    threads.Add(thread);    
}

foreach (Thread thread in threads)
{
    thread.Start();
}

Console.WriteLine($"[Thread] Thread# {Thread.CurrentThread.ManagedThreadId} finished.");



static void DownloadImage(int index)
{
    Console.WriteLine($"[Thread] Thread# {Thread.CurrentThread.ManagedThreadId} image #{index}... downloading ");

    using var client = new HttpClient();
    var bytes = client.GetByteArrayAsync("https://picsum.photos/200").Result;
    File.WriteAllBytes($"image{index}.jpg", bytes);

    Console.WriteLine($"[Thread] Thread# {Thread.CurrentThread.ManagedThreadId} #{index} downloaded.");
}
