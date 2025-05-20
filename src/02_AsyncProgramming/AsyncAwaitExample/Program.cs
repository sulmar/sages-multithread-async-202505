// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Tasks!");

Printer printer = new Printer();

printer.Print("Document #1");
printer.Print("Document #2");
printer.Print("Document #3");

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
}