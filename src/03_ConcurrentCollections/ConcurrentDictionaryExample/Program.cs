
using System.Collections.Concurrent;

Console.WriteLine("Hello, ConcurrentDictionary!");


// Mapa urządzeń po IMEI

// Szybki dostęp do urządzenia po jego identyfikatorze (np. IMEI). Przydatne do aktualizacji stanu lub lokalizacji konkretnego urządzenia.

var tracker = new DeviceTracker();
tracker.SimulateParallelUpdates();

if (tracker.TryGetDeviceStatus("351111111111111", out var status))
{
    Console.WriteLine($"Znaleziono: {status}");
}

public class DeviceStatus
{
    public string DeviceId { get; set; }
    public double BatteryLevel { get; set; }

    public override string ToString() => $"(ID: {DeviceId}) {BatteryLevel / 100:P2} ";

    public DeviceStatus MergeWith(DeviceStatus other)
    {
        return new DeviceStatus
        {
            DeviceId = this.DeviceId,
            BatteryLevel = Math.Max(this.BatteryLevel, other.BatteryLevel)
        };
    }
}


public class DeviceTracker
{
    private readonly ConcurrentDictionary<string, DeviceStatus> _devicesByImei = new(); // ❌ niebezpieczny słownik

    public bool TryGetDeviceStatus(string imei, out DeviceStatus status)
    {
        // ❗ Odczyt w czasie zapisu = potencjalny wyjątek
        return _devicesByImei.TryGetValue(imei, out status);
    }

    public void UpdateDeviceStatus(string imei, DeviceStatus status)
    {
        // równoczesne wywołania mogą spowodować wyjątek
        if (_devicesByImei.ContainsKey(imei))
        {
            _devicesByImei[imei] = status;
        }
        else
        {
            _devicesByImei.AddOrUpdate(imei, status, 
                ( key, existing) => status.MergeWith(existing)); // funkcja aktualizacji

        }

        Console.WriteLine($"Zaktualizowano: {imei} → {status}");

    }

    public void SimulateParallelUpdates()
    {
        string[] imeis = { "351111111111111", "352222222222222", "353333333333333" };

        Parallel.ForEach(imeis, imei =>
        {
            for (int i = 0; i < 10; i++)
            {
                var normalized = Random.Shared.NextDouble();         // 0.0 – 1.0
                var batteryLevel = 50 + normalized * 50;             // 50.0 – 100.0

                var status = new DeviceStatus
                {
                    DeviceId = $"dev-{imei[^3..]}",
                    BatteryLevel = batteryLevel
                };

                UpdateDeviceStatus(imei, status);

                Thread.Sleep(5); // sztuczne opóźnienie
            }
        });

        Console.WriteLine($"\nFinalnie zarejestrowano: {_devicesByImei.Count} urządzeń");
    }
}