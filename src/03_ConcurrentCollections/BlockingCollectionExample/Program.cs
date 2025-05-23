
using System.Collections.Concurrent;

Console.WriteLine("Hello, BlockingCollection!");

InvoiceProcessor processor = new InvoiceProcessor();

Task.Run(() => processor.Process());

// Symulacja generowania faktur do zaksięgowania
for (int i = 0; i < 10; i++)
{
    var invoice = new Invoice { Number = $"Invoice #{i}", TotalAmount = Math.Round( Random.Shared.NextDouble() * 1000 + 100, 2) };

    processor.Accounting(invoice);

    Thread.Sleep(2000);
}


class InvoiceProcessor
{
    List<Invoice> invoices = new List<Invoice>();

    public void Process()
    {
        foreach (var invoice in invoices)
        {
            Console.WriteLine($"{invoice} is accounting...");
            Thread.Sleep(5000);
            Console.WriteLine($"{invoice} is accounted.");
        }
    }

    public void Accounting(Invoice invoice)
    {
        
        invoices.Add(invoice);
        Console.WriteLine($"Invoice {invoice} added");
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


