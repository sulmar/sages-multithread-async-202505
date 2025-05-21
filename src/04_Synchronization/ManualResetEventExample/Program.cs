
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, ManualResetEvent!");

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddSingleton<MotionDetectionService>();
                services.AddSingleton<AlarmService>();
                services.AddHostedService<Worker>();
            })
            .Build();

await host.RunAsync();


public class MotionDetectionService
{
    public bool IsMotionDetected { get; private set; }

    public void DetectMotion()
    {
        Console.WriteLine($"🎥 Motion detected at {DateTime.Now:HH:mm:ss}");
        IsMotionDetected = true;
    }

    public void Reset()
    {
        IsMotionDetected = false;
    }
}

public class AlarmService
{
    private readonly MotionDetectionService _motionService;

    public AlarmService(MotionDetectionService motionService)
    {
        _motionService = motionService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (_motionService.IsMotionDetected)
            {
                Console.WriteLine($"🚨 Alarm activated at {DateTime.Now:HH:mm:ss}");
                _motionService.Reset();
            }

            await Task.Delay(100); // 👎 aktywne oczekiwanie (polling)
        }
    }
}



public class Worker : BackgroundService
{
    private readonly MotionDetectionService _motionService;
    private readonly AlarmService _alarmService;

    public Worker(MotionDetectionService motionService, AlarmService alarmService)
    {
        _motionService = motionService;
        _alarmService = alarmService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ⏱️ Uruchamiamy nasłuchiwanie alarmu
        _ = _alarmService.StartAsync(stoppingToken);

        var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _motionService.DetectMotion();
        }
    }
}

public class LightService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine($"💡 Light turned on at {DateTime.Now:HH:mm:ss}");

            }
        }, cancellationToken);
    }
}


//❗ Problemy:
// 1. Polling – AlarmService co 100ms pyta, czy coś się wydarzyło → marnuje CPU, nieefektywne
// 2. Można zgubić ruch, jeśli Reset() zostanie wywołany zanim AlarmService zdąży odczytać flagę.
// 3. Kod jest nieskalowalny – każde nowe urządzenie dodaje kolejne wątki sprawdzające.