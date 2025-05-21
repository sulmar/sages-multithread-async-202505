// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Console.WriteLine("Hello, Barier!");

Console.WriteLine("🚗 Startujemy analizę...");

// Brak synchronizacji – faza weryfikacji dostępu może się zacząć zanim inne komponenty zakończą detekcję.

var gateService = new GateAccessService();
await gateService.RunAsync();

Console.WriteLine("🏁 Operacja zakończona.");

public class GateAccessService
{
    public GateAccessService()
    {
    }

    public async Task RunAsync()
    {
        var camera = new CameraService();
        var rfid = new RfidService();
        var sensor = new VehicleSensorService();

        var tasks = new[]
        {
            camera.RunAsync(),
            rfid.RunAsync(),
            sensor.RunAsync()
        };

        await Task.WhenAll(tasks);
    }
}


public class CameraService
{
    public async Task RunAsync()
    {
        Console.WriteLine("📸 [Kamera] Faza 1: Analiza...");
        await Task.Delay(1000 + Random.Shared.Next(1000));
        Console.WriteLine("📸 [Kamera] Faza 1: Rozpoznano tablicę.");

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("📸 [Kamera] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }
}

public class RfidService
{
    public async Task RunAsync()
    {
        Console.WriteLine("📡 [RFID] Faza 1: Skanowanie...");
        await Task.Delay(800 + Random.Shared.Next(500));
        Console.WriteLine("📡 [RFID] Faza 1: Znaleziono token.");

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("📡 [RFID] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }
}

public class VehicleSensorService
{
    public async Task RunAsync()
    {
        Console.WriteLine("🛑 [Sensor] Faza 1: Detekcja pojazdu...");
        await Task.Delay(500 + Random.Shared.Next(700));
        Console.WriteLine("🛑 [Sensor] Faza 1: Pojazd obecny.");

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("🛑 [Sensor] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }



}