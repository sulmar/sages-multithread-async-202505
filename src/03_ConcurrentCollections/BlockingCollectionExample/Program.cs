
using System.Collections.Concurrent;

Console.WriteLine("Hello, BlockingCollection!");

var invoice = new Invoice
{
    Number = "001",
    TotalAmount = 1.99d,
    Details = new List<Detail>
    { 
        new Detail("a", 1, 2.99),
        new Detail("b", 2, 1.99),
    }
};


foreach (var detail in invoice.Details)
{
    Console.WriteLine(detail.title);
}

return;


InvoiceProcessor processor = new InvoiceProcessor();

Task.Run(() => processor.Process("Kate"));
Task.Run(() => processor.Process("John"));
Task.Run(() => processor.Process("Bob"));


// Symulacja generowania faktur do zaksięgowania
Task.Run(() => Producer(processor, "Producer 1"));
Task.Run(() => Producer(processor, "Producer 2"));

processor.Complete();

Thread.Sleep(1000);

try
{
    processor.Accounting(invoice, name: "Producer 1");

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine("Finished.");
Console.ReadLine();

static void Producer(InvoiceProcessor processor, string name)
{
    for (int i = 0; i < 10; i++)
    {
        var invoice = new Invoice { Number = $"Invoice #{name}-{i}", TotalAmount = Math.Round(Random.Shared.NextDouble() * 1000 + 100, 2) };

        processor.Accounting(invoice, name);

        Thread.Sleep(2000);
    }
}

class InvoiceProcessor
{
    BlockingCollection<Invoice> invoices = new BlockingCollection<Invoice>();

    public void Process(string name)
    {
        Console.WriteLine(invoices.Count);

        foreach (var invoice in invoices.GetConsumingEnumerable()) // To nie jest Pulling. To jest sygnał
        {
            Console.WriteLine($"\t[{name}] {invoice} is accounting...");
            Thread.Sleep(5000);
            Console.WriteLine($"\t\t[{name}] {invoice} is accounted.");
        }

        Console.WriteLine("Koniec przetwarzania faktur");
    }

    public void Save(string name)
    {
        foreach (var invoice in invoices.GetConsumingEnumerable()) // To nie jest Pulling. To jest sygnał
        {
            Console.WriteLine($"\t[{name}] {invoice} is saving...");
            Thread.Sleep(5000);
            Console.WriteLine($"\t\t[{name}] {invoice} is saved.");
        }

        Console.WriteLine("Koniec przetwarzania faktur");


    }



    public void Accounting(Invoice invoice, string name)
    {

        invoices.Add(invoice);
        Console.WriteLine($"[{name}] Invoice {invoice} added");
    }

    public void Complete()
    {
        // invoices.CompleteAdding();
        Console.WriteLine("Koniec przyjmowania faktur");
    }
}



//class Invoice
//{    
//    public string Number { get; set; }

//    public override string ToString()
//    {
//        return Number;
//    }
//}

record Invoice
{
    public string Number { get; set; }
    public double TotalAmount { get; set; }

    public IEnumerable<Detail> Details { get; set; }
}


record Detail(string title, double quanity, double price);


