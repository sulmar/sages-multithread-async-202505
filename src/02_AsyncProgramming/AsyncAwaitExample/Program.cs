// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Tasks!");

Printer printer = new Printer();

printer.PrintAsync("Document #1");
printer.PrintAsync("Document #2");
printer.PrintAsync("Document #3");

Console.WriteLine("Finished.");
Console.ReadLine();


public class Printer
{
    public void Print(string content)
    {
        Console.WriteLine($"Printing '{content}' on thread {Thread.CurrentThread.ManagedThreadId}...");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine($"Print completed.");
    }


    public Task PrintAsync(string content)
    {
        return Task.Run(() => Print(content));               
    }
}