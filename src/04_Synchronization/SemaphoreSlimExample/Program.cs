Console.WriteLine("🔌 Stacja ładowania gotowa (bez ograniczeń).");

var station = new ChargingStation(maxConcurrentCharges: 3);

var vehicleTasks = Enumerable.Range(1, 10)
    .Select(id => station.ChargeVehicleAsync(id))
    .ToArray();

await Task.WhenAll(vehicleTasks);

Console.WriteLine("✅ Wszystkie pojazdy obsłużone.");

public class ChargingStation
{
    private readonly SemaphoreSlim _semaphore;

    public ChargingStation(int maxConcurrentCharges)
    {
        _semaphore = new SemaphoreSlim(maxConcurrentCharges, maxConcurrentCharges);
    }

    public async Task ChargeVehicleAsync(int vehicleId)
    {        
        await _semaphore.WaitAsync();

        Console.WriteLine($"🚗 Pojazd #{vehicleId} rozpoczął ładowanie (brak kontroli).");

        // Symulacja czasu ładowania
        await Task.Delay(Random.Shared.Next(2000, 4000));

        Console.WriteLine($"✅ Pojazd #{vehicleId} zakończył ładowanie.");

        _semaphore.Release();
    }
}