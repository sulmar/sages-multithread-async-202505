
using System.Collections.Concurrent;

Console.WriteLine("Hello, BlockingCollection!");


var ocrService = new OcrService();
var cts = new CancellationTokenSource();

// 🔁 Konsument (OCR worker)

var processor = Task.Run(() =>
{
    // 🔁 Konsument (OCR worker)
    
    Console.WriteLine("Konsument (OCR worker) nasłuchuje...");

    foreach (var doc in ocrService.GetJobs(cts.Token))
    {
        Console.WriteLine($"Przetwarzanie: {doc}");
        Thread.Sleep(200); // symulacja OCR
        Console.WriteLine($"Zakończono: {doc.FileName}");
    }
}, cts.Token);

await Task.Delay(TimeSpan.FromSeconds(10));

// 🔁 Producent (Symulacja źródła dokumentów)
var producer = Task.Run(() =>
{
    Console.WriteLine("Producent (OCR scanner) s...");

    var files = new[] { "invoice.pdf", "contract.pdf", "report.pdf" };

    for (int i = 0; i < 20; i++)
    {
        var doc = new OcrDocument
        {
            FileName = files[i % files.Length],
            SubmittedAt = DateTime.Now
        };

        ocrService.Submit(doc);
        Thread.Sleep(Random.Shared.Next(50, 150));
    }

    ocrService.Complete(); // sygnał końca pracy
});


// Poczekaj na zakończenie
Task.WaitAll(producer, processor);

Console.WriteLine("Wszystkie dokumenty przetworzone.");



public class OcrDocument
{
    public string FileName { get; set; }
    public DateTime SubmittedAt { get; set; }

    public override string ToString() => $"{FileName} ({SubmittedAt:HH:mm:ss.fff})";
}


public class OcrService
{
    private readonly BlockingCollection<OcrDocument> _queue = new();

    public void Submit(OcrDocument document)
    {
        _queue.Add(document); // zablokuje się jeśli bufor pełny
        Console.WriteLine($"Dodano: {document}");
    }


    public IEnumerable<OcrDocument> GetJobs(CancellationToken token) => _queue.GetConsumingEnumerable();

    public void Complete() => _queue.CompleteAdding();
}