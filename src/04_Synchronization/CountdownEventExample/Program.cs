
Console.WriteLine("Hello, CountdownEvent!");

var gate = new ParkingGate(maxVehicles: 3);
var vehicleCount = 10;

var tasks = Enumerable.Range(1, vehicleCount)
    .Select(id => Task.Run(async () =>
    {
        await Task.Delay(Random.Shared.Next(300, 1500)); // losowe opóźnienie przyjazdu
        await gate.TryEnterAsync(id);
    }))
    .ToArray();


Console.WriteLine("🛑 Parking pełny – bramka zamknięta.");

await Task.WhenAll(tasks);

public class ParkingGate
{
    private int _remainingSpots;
    private readonly object _lock = new();

    public ParkingGate(int maxVehicles)
    {
        _remainingSpots = maxVehicles;
    }


    public async Task TryEnterAsync(int vehicleId)
    {
        await Task.Yield();

        lock (_lock)
        {
            if (_remainingSpots > 0)
            {
                _remainingSpots--;
                Console.WriteLine($"🚗 Pojazd #{vehicleId} wpuszczony.");

            }
            else
            {
                Console.WriteLine($"❌ Pojazd #{vehicleId} odrzucony — brak miejsc.");
            }
        }
    }
}