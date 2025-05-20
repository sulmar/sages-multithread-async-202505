// See https://aka.ms/new-console-template for more information
using AsyncAwaitExample;

Console.WriteLine("Hello, Tasks!");

//Printer printer = new Printer();

//printer.PrintAsync("Document #1");
//printer.PrintAsync("Document #2");
//printer.PrintAsync("Document #3");

//Downloader downloader = new Downloader();

//for (int i = 0; i < 50; i++)
//{
//    downloader.DownloadImage(i);
//}

CostCalculator calculator = new CostCalculator();
var cost = calculator.Calculate("Document #1");
Console.WriteLine($"Cost: {cost:C2}");

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


public class CostCalculator
{
    public decimal Calculate(string content)
    {
        Console.WriteLine($"Calculating on thread {Thread.CurrentThread.ManagedThreadId}...");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine("Calculated.");

        return content.Length * 0.05m;

    }
}