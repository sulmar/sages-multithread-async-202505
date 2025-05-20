// See https://aka.ms/new-console-template for more information
using System.Threading.Tasks;

Console.WriteLine("Hello, Cancellation Token!");

Printer printer = new Printer();
await printer.PrintAsync("Document #1");

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();



class Printer
{

    public async Task PrintAsync(string content)
    {
        Console.WriteLine($"Printing '{content}' on thread {Thread.CurrentThread.ManagedThreadId}...");

        for (int i = 0; i < 5; i++)
        {
            await Task.Delay(200);

            Console.WriteLine($"...printing chunk {i + 1}"); // Drukowanie fragmentu strony

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        Console.WriteLine($"Print completed.");
    }
}