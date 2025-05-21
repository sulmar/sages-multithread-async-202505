
using System.Collections.Concurrent;

Console.WriteLine("Hello, Concurrent Queue!");

var ocrService = new OcrService();
var fileNames = new[] { "invoice1.pdf", "contract2.pdf", "scan3.pdf" };
bool running = true;

// Konsument
var consumer = Task.Run(() =>
{
    while (running)
    {
        ocrService.ProcessNext();
        Thread.Sleep(10);
    }
});

// Producenci – wielu równoległych "skanerów"
Parallel.For(0, 30, i =>
{
    var document = new OcrDocument
    {
        FileName = fileNames[i % fileNames.Length],
        SubmittedAt = DateTime.Now
    };

    ocrService.Submit(document);
    Thread.Sleep(Random.Shared.Next(1, 10)); // symulacja różnego tempa
});

Thread.Sleep(2000);
running = false;
consumer.Wait();



public class OcrDocument
{
    public string FileName { get; set; }
    public DateTime SubmittedAt { get; set; }

    public override string ToString() => $"📄 {FileName} ({SubmittedAt:HH:mm:ss.fff})";
}




public class OcrService
{
    private readonly ConcurrentQueue<OcrDocument> _queue = new(); // ❌ NIEBEZPIECZNA

    public void Submit(OcrDocument document)
    {
        _queue.Enqueue(document); // ❗ Brak synchronizacji
        Console.WriteLine($"Dodano do OCR: {document}");
    }

    public void ProcessNext()
    {
        if (_queue.TryDequeue(out var document))
        {
            Console.WriteLine($"Rozpoczęto przetwarzanie: {document}");
            Thread.Sleep(100); // symulacja OCR
            Console.WriteLine($"Zakończono: {document.FileName}");
        }
    }
}