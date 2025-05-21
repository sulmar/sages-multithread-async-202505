Console.WriteLine("🔌 Stacja ładowania gotowa (bez ograniczeń).");

var station = new ChargingStation(maxConcurrentCharges: 3);

var vehicleTasks = Enumerable.Range(1, 10)
    .Select(id => Task.Run(() => station.ChargeVehicle(id)))
    .ToArray();

await Task.WhenAll(vehicleTasks);

Console.WriteLine("✅ Wszystkie pojazdy obsłużone.");

public class ChargingStation
{
    private readonly Semaphore _semaphore;

    public ChargingStation(int maxConcurrentCharges)
    {
        _semaphore = new Semaphore(maxConcurrentCharges, maxConcurrentCharges);
    }

    public void ChargeVehicle(int vehicleId)
    {
        _semaphore.WaitOne();

        Console.WriteLine($"🚗 Pojazd #{vehicleId} rozpoczął ładowanie (brak kontroli).");

        // Symulacja czasu ładowania
        Thread.Sleep(Random.Shared.Next(2000, 4000));

        Console.WriteLine($"✅ Pojazd #{vehicleId} zakończył ładowanie.");

        _semaphore.Release();
    }

    public async Task ChargeVehicleAsync(int vehicleId)
    {
        Console.WriteLine($"🚗 Pojazd #{vehicleId} rozpoczął ładowanie (brak kontroli).");

        // Symulacja czasu ładowania
        await Task.Delay(Random.Shared.Next(2000, 4000));

        Console.WriteLine($"✅ Pojazd #{vehicleId} zakończył ładowanie.");
    }
}