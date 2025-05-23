
using System.Diagnostics;

Console.WriteLine("Hello, Parallel.For!");

const int items = 20;

MeasureForExecutionTime(items);

void MeasureForExecutionTime(int items)
{
    Console.WriteLine("Wykonanie sekwencyjne......");

    var stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < items; i++)
    {
        DoWork(i);
    }

    stopwatch.Stop();


    Console.WriteLine($"Czas wykonania (sekwencyjnie): {stopwatch.ElapsedMilliseconds} ms");
}

// Symulacja operacji CPU-bound czasochłonnej
static void DoWork(int item)
{
    Console.WriteLine($"Przetwarzanie {item} na wątku {Thread.CurrentThread.ManagedThreadId}");
    Thread.SpinWait(1_000_000); // Obciążenie CPU
    Thread.Sleep(100); // Symulacja opoźnienia
}
