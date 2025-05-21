
Console.WriteLine("Hello, Monitor!");

var gate = new ParkingGate(maxVehicles: 3);
var vehicleCount = 10;

var tasks = Enumerable.Range(1, vehicleCount)
    .Select(id => Task.Run(async () =>
    {
        await Task.Delay(Random.Shared.Next(1000, 1500)); // losowe opóźnienie przyjazdu - 👉 sprawdż co się stanie przy mniejszych czasach!
        await gate.TryEnterAsync(id);
    }))
    .ToArray();


await Task.WhenAll(tasks);

public class ParkingGate
{
    private int _remainingSpots;

    public ParkingGate(int maxVehicles)
    {
        _remainingSpots = maxVehicles;
    }


    public async Task TryEnterAsync(int vehicleId)
    {
        await Task.Yield();

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