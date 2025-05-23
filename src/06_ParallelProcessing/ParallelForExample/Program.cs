
using System.Diagnostics;

Console.WriteLine("Hello, Parallel.For!");

const int items = 20;

MeasureForExecutionTime(items);

void MeasureForExecutionTime(int items)
{
    Console.WriteLine("Start Executing...");

    var stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < items; i++)
    {
        DoWork(i);
    }

    stopwatch.Stop();


    Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
}

// Symulacja operacji CPU-bound czasochłonnej
static void DoWork(int i)
{
    Thread.SpinWait(1_000_000); // Obciążenie CPU
    Thread.Sleep(100); // Symulacja opoźnienia
}
