// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Mutex!");

var printerService = new LabelPrinterService();

for (int i = 1; i <= 5; i++)
{
    int deviceId = i;
    new Thread(() => printerService.PrintLabel(deviceId)).Start();
}

public class LabelPrinterService
{
    public void PrintLabel(int deviceId)
    {
        Console.WriteLine($"📦 Device {deviceId} is starting print job...");

        // BRAK synchronizacji – możliwa kolizja!
        Console.WriteLine($"🖨️ Device {deviceId} is printing label...");
        Thread.Sleep(3000); // symulacja drukowania
        Console.WriteLine($"✅ Device {deviceId} done printing.");
    }
}