
using System.Diagnostics;
using System.Threading.Tasks;

Console.WriteLine("Hello, Parallel.For!");

const int count = 20;

MeasureForExecutionTime(count);
MeasureForExecutionTimeTask(count);

Console.ReadKey();

async Task MeasureForExecutionTimeTask(int count)
{
    Console.WriteLine("Wykonanie asynchroniczne...");
    
    var stopwatch = Stopwatch.StartNew();

    var items = Enumerable.Range(0, count);

    var tasks = items.Select(i => DoWorkAsync(i));
    await Task.WhenAll(tasks);

    stopwatch.Stop();


    Console.WriteLine($"Czas wykonania (taski): {stopwatch.ElapsedMilliseconds} ms");
}



void MeasureForExecutionTime(int count)
{
    Console.WriteLine("Wykonanie sekwencyjne...");

    var stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < count; i++)
    {
        DoWork(i);
    }

    stopwatch.Stop();


    Console.WriteLine($"Czas wykonania (sekwencyjnie): {stopwatch.ElapsedMilliseconds} ms");
}

static async Task DoWorkAsync(int item)
{
    Console.WriteLine($"Przetwarzanie {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
    Thread.SpinWait(1_000_000); // Obciążenie CPU
    
    await Task.Delay(100); // Symulacja opoźnienia
    Console.WriteLine($"Zakonczono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}
// Symulacja operacji CPU-bound czasochłonnej
static void DoWork(int item)
{
    Console.WriteLine($"Przetwarzanie {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
    Thread.SpinWait(1_000_000); // Obciążenie CPU
    Thread.Sleep(100); // Symulacja opoźnienia
    Console.WriteLine($"Zakonczono {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
}
