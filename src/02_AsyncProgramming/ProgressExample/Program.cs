// See https://aka.ms/new-console-template for more information
using ProgressExample;
using System;

Console.WriteLine("Hello, Progress!");


// Automatyczne anulowanie zadania po określonym czasie 
CancellationTokenSource cts = new CancellationTokenSource();

CancellationToken cancellationToken = cts.Token;

Printer printer = new Printer();

IProgress<int> consoleProgress = new Progress<int>(step => Console.WriteLine($"...printing chunk {step + 1}"));

IProgress<int> colorConsoleProgress = new ColorConsoleProgress();


Task task = printer.PrintAsync("Document #1", cancellationToken, colorConsoleProgress);

// Anulowanie zadania po określonym czasie po wywołaniu Cancel
// cts.CancelAfter(3500);

try
{
    await task;
}
catch (OperationCanceledException e)
{
    Console.WriteLine("Printing Cancelled.");
}

Console.ReadLine();
Console.WriteLine("Press Enter to exit.");


class Printer
{

    public async Task PrintAsync(string content, CancellationToken cancellationToken = default, IProgress<int>? progress = default)
    {
        Console.WriteLine($"Printing '{content}' on thread {Thread.CurrentThread.ManagedThreadId}...");

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"cancellationToken.IsCancellationRequested: {cancellationToken.IsCancellationRequested}");
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            // cancellationToken.ThrowIfCancellationRequested();

            // Console.WriteLine($"...printing chunk {i + 1}"); // Drukowanie fragmentu strony

            progress?.Report(i);


            await Task.Delay(1000, cancellationToken);

        }

        Console.WriteLine($"Print completed.");
    }
}