
using System.Collections.Concurrent;

Console.WriteLine("Hello, BlockingCollection!");

InvoiceProcessor processor = new InvoiceProcessor();

Task.Run(() => processor.Process());

// Symulacja generowania faktur do zaksięgowania
for (int i = 0; i < 10; i++)
{
    var invoice = new Invoice { Number = $"Invoice #{i}", TotalAmount = Math.Round(Random.Shared.NextDouble() * 1000 + 100, 2) };

    processor.Accounting(invoice);

    Thread.Sleep(2000);
}

processor.Complete();

Thread.Sleep(1000);

try
{
    processor.Accounting(new Invoice { Number = "001", TotalAmount = 1.99d });

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine("Finished.");
Console.ReadLine();


class InvoiceProcessor
{
    BlockingCollection<Invoice> invoices = new BlockingCollection<Invoice>();

    public void Process()
    {
        Console.WriteLine(invoices.Count);

        foreach (var invoice in invoices.GetConsumingEnumerable()) // To nie jest Pulling. To jest sygnał
        {
            Console.WriteLine($"{invoice} is accounting...");
            Thread.Sleep(5000);
            Console.WriteLine($"{invoice} is accounted.");
        }

        Console.WriteLine("Koniec przetwarzania faktur");
    }

    public void Accounting(Invoice invoice)
    {

        invoices.Add(invoice);
        Console.WriteLine($"Invoice {invoice} added");
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


}


