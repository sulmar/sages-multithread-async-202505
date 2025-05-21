
Console.WriteLine("Hello, CountdownEvent!");

var gate = new ParkingGate(maxVehicles: 3);
var vehicleCount = 10;

var tasks = Enumerable.Range(1, vehicleCount)
    .Select(id => Task.Run(async () =>
    {
        await Task.Delay(Random.Shared.Next(1300, 1500)); // losowe opóźnienie przyjazdu
        await gate.TryEnterAsync(id);
    }))
    .ToArray();

gate.WaitUntilFull();

Console.WriteLine("🛑 Parking pełny – bramka zamknięta.");

await Task.WhenAll(tasks);

public class ParkingGate
{
    private readonly object _lock = new();

    private readonly CountdownEvent _countdownEvent;

    public ParkingGate(int maxVehicles)
    {
        _countdownEvent = new CountdownEvent(maxVehicles);
    }

    public void WaitUntilFull()
    {
        _countdownEvent.Wait();

        // Zamykamy po określonym czasie
        // _countdownEvent.Wait(TimeSpan.FromSeconds(1));
    }


    public async Task TryEnterAsync(int vehicleId)
    {
        await Task.Yield();

        lock (_lock)
        {
            if (_countdownEvent.CurrentCount > 0)
            {
                Console.WriteLine($"🚗 Pojazd #{vehicleId} wpuszczony.");
                _countdownEvent.Signal();

            }
            else
            {
                Console.WriteLine($"❌ Pojazd #{vehicleId} odrzucony — brak miejsc.");
            }
        }
    }
}