
Console.WriteLine("Hello, Cancellation Token!");

// Automatyczne anulowanie zadania po określonym czasie 
using var cts = new CancellationTokenSource(3500);

CancellationToken cancellationToken = cts.Token;

Console.WriteLine("Press Ctrl+C for cancel");
Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("Stopping...");
    e.Cancel = true;

    // Anulowanie zadania natychmiastowe
    // cts.Cancel();

    // Anulowanie zadania po określonym czasie
    cts.CancelAfter(1500);
};


Printer printer = new Printer();

Task task = printer.PrintAsync("Document #1", cancellationToken);


try
{
    await task;
}
catch (OperationCanceledException e)
{
    Console.WriteLine( "Printing was canceled.");
}

Console.ReadLine();
Console.WriteLine("Press Enter to exit.");

class Printer
{

    public async Task PrintAsync(string content, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Printing '{content}' on thread {Thread.CurrentThread.ManagedThreadId}...");

        for (int i = 0; i < 5; i++)
        {
            //Console.WriteLine($"cancellationToken.IsCancellationRequested: {cancellationToken.IsCancellationRequested}");
            //if (cancellationToken.IsCancellationRequested)
            //{
            //    throw new OperationCanceledException();
            //}

           cancellationToken.ThrowIfCancellationRequested();

            Console.WriteLine($"...printing chunk {i + 1}"); // Drukowanie fragmentu strony

            await Task.Delay(1000, cancellationToken);

        }

        Console.WriteLine($"Print completed.");
    }
}