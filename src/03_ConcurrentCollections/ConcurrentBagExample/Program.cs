
using System.Collections.Concurrent;

Console.WriteLine("Hello, ConcurrentBag!");

// Zbiór aktywnych urządzeń IoT
var registry = new DeviceRegistry();
registry.RegisterAllDevices(iterations: 1000); // Możesz zwiększyć do 10_000


public class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
}


public class DeviceRegistry
{
    private readonly ConcurrentBag<Device> _activeDevices = new(); // ❌ NIEBEZPIECZNA współbieżnie

    private readonly Device[] _discoveredDevices = new[]
    {
        new Device { Id = "dev01", Name = "TempSensor" },
        new Device { Id = "dev02", Name = "HumiditySensor" },
        new Device { Id = "dev03", Name = "LightSensor" }
    };

    public void RegisterAllDevices(int iterations)
    {
        Parallel.For(0, iterations, i =>
        {
            Parallel.ForEach(_discoveredDevices, RegisterDevice);
        });

        Console.WriteLine($"Zarejestrowano łącznie: {_activeDevices.Count} urządzeń");
    }

    private void RegisterDevice(Device device)
    {
        // 👇 Wymuszamy kolizję wątków
        Thread.Sleep(Random.Shared.Next(1, 5));

        _activeDevices.Add(device); 

        Console.WriteLine($"Dodano: {device.Name}");
    }
}