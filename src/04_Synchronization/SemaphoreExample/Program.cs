Console.WriteLine("🔌 Stacja ładowania gotowa (bez ograniczeń).");

var station = new ChargingStation();

var vehicleTasks = Enumerable.Range(1, 10)
    .Select(id => Task.Run(() => station.ChargeVehicleAsync(id)))
    .ToArray();

await Task.WhenAll(vehicleTasks);

Console.WriteLine("✅ Wszystkie pojazdy obsłużone.");

public class ChargingStation
{
    public async Task ChargeVehicleAsync(int vehicleId)
    {
        Console.WriteLine($"🚗 Pojazd #{vehicleId} rozpoczął ładowanie (brak kontroli).");

        // Symulacja czasu ładowania
        await Task.Delay(Random.Shared.Next(2000, 4000));

        Console.WriteLine($"✅ Pojazd #{vehicleId} zakończył ładowanie.");
    }
}