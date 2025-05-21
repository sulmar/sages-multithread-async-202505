// See https://aka.ms/new-console-template for more information
using System.Threading;

Console.WriteLine("Hello, World!");

Console.WriteLine("Hello, Barier!");

Console.WriteLine("🚗 Startujemy analizę...");

// Brak synchronizacji – faza weryfikacji dostępu może się zacząć zanim inne komponenty zakończą detekcję.



var gateService = new GateAccessService();
await gateService.RunAsync();

Console.WriteLine("🏁 Operacja zakończona.");

public class GateAccessService
{
    private readonly Barrier _barrier;

    public GateAccessService()
    {
        _barrier = new Barrier(3, b =>
        {
            Console.WriteLine($"[Barier] Faza {b.CurrentPhaseNumber} zakończona. Przechodzimy do kolejnej fazy");
            
        });
    }

    public async Task RunAsync()
    {
        var camera = new CameraService(_barrier);
        var rfid = new RfidService(_barrier);
        var sensor = new VehicleSensorService(_barrier);

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
    private readonly Barrier _barrier;

    public CameraService(Barrier barrier)
    {
        this._barrier = barrier;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("📸 [Kamera] Faza 1: Analiza...");
        await Task.Delay(1000 + Random.Shared.Next(1000));
        Console.WriteLine("📸 [Kamera] Faza 1: Rozpoznano tablicę.");

        _barrier.SignalAndWait(); // Synchronizacja

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("📸 [Kamera] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }
}

public class RfidService
{
    private readonly Barrier _barrier;

    public RfidService(Barrier barrier)
    {
        this._barrier = barrier;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("📡 [RFID] Faza 1: Skanowanie...");
        await Task.Delay(800 + Random.Shared.Next(500));
        Console.WriteLine("📡 [RFID] Faza 1: Znaleziono token.");

        _barrier.SignalAndWait(); // Synchronizacja

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("📡 [RFID] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }
}

public class VehicleSensorService
{
    private readonly Barrier _barrier;

    public VehicleSensorService(Barrier barrier)
    {
        this._barrier = barrier;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("🛑 [Sensor] Faza 1: Detekcja pojazdu...");
        await Task.Delay(500 + Random.Shared.Next(700));
        Console.WriteLine("🛑 [Sensor] Faza 1: Pojazd obecny.");

        _barrier.SignalAndWait(); // Synchronizacja

        // Faza 2 – może się wykonać zbyt wcześnie
        Console.WriteLine("🛑 [Sensor] Faza 2: Weryfikacja dostępu...");
        await Task.Delay(500);
    }



}