
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, ManualResetEvent!");


var motionEvent = new ManualResetEvent(false);

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
{
services.AddSingleton(motionEvent);
services.AddSingleton<MotionDetectionService>();
services.AddSingleton<AlarmService>();
services.AddSingleton<LightService>();
services.AddHostedService<Worker>();
})
            .Build();

await host.RunAsync();


public class MotionDetectionService
{
    private readonly ManualResetEvent _motionEvent;

    public MotionDetectionService(ManualResetEvent motionEvent)
    {
        _motionEvent = motionEvent;
    }

    public void DetectMotion()
    {
        Console.WriteLine($"🎥 Motion detected at {DateTime.Now:HH:mm:ss}");
        _motionEvent.Set(); // Wysyła sygnał
    }
}

public class AlarmService
{
    private readonly ManualResetEvent motionEvent;

    public AlarmService(ManualResetEvent motionEvent)
    {
        this.motionEvent = motionEvent;
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.Factory.StartNew(() => Start(cancellationToken), TaskCreationOptions.LongRunning);

    private void Start(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            motionEvent.WaitOne(); // Czeka na sygnał (np. od detektora ruchu)

            Console.WriteLine($"🚨 Alarm activated at {DateTime.Now:HH:mm:ss}");
        }
    }
}


public class LightService
{
    private readonly ManualResetEvent _motionEvent;

    public LightService(ManualResetEvent motionEvent)
    {
        _motionEvent = motionEvent;
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.Factory.StartNew(() => Start(cancellationToken), TaskCreationOptions.LongRunning);

    public void Start(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _motionEvent.WaitOne(); // Czeka na sygnał (np. od detektora ruchu)

            Console.WriteLine($"💡 Light turned on at {DateTime.Now:HH:mm:ss}");
            
        }
    }
}




public class Worker : BackgroundService
{
    private readonly MotionDetectionService _motionService;
    private readonly AlarmService _alarmService;
    private readonly LightService _lightService;
    private readonly ManualResetEvent _motionEvent;

    public Worker(MotionDetectionService motionService, AlarmService alarmService, LightService lightService, ManualResetEvent motionEvent)
    {
        _motionService = motionService;
        _alarmService = alarmService;
        _lightService = lightService;
        _motionEvent = motionEvent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // ⏱️ Uruchamiamy nasłuchiwanie alarmu
        _ = _alarmService.StartAsync(stoppingToken);
        _ = _lightService.StartAsync(stoppingToken);

        var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _motionService.DetectMotion();

            _motionEvent.Reset();
        }
    }
}

// Idealne rozwiązanie do scenariuszy typu broadcast (wiele usług nasłuchuje jednego zdarzenia)
