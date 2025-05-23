
Console.WriteLine("Hello, PeriodTimer!");


using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("Stopping...");
    e.Cancel = true;
    cts.Cancel();
};



// Callback
System.Threading.Timer timer1 = new Timer( _ => Console.WriteLine("[System.Threading.Timer] Heartbeat"), null, 0, 2000);

// Event
System.Timers.Timer timer2 = new System.Timers.Timer(TimeSpan.FromSeconds(2));
timer2.Elapsed += (sender, e) => Console.WriteLine("[System.Timers.Timer ] Heartbeat");
timer2.Start();

// Task
var timer = new PeriodicTimer(TimeSpan.FromSeconds(2)); // ⏱ generuje tick co 2 sekundy

try
{
    while (await timer.WaitForNextTickAsync(cts.Token))  // ⏱ czeka 2 sekundy
    {
        Console.WriteLine($"Heartbeat: {DateTime.Now}");
        await DoSomethingAsync(); // 🕒 zajmuje 1 sekundę
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Delay was cancelled.");
}

// ⚠️ Odstępy między startami DoSomethingAsync wynoszą ok. 3 sekundy (2s + 1s) a nie zakładane 2 sekundy

async Task DoSomethingAsync()
{
    Console.WriteLine("Do something...");
    await Task.Delay(Random.Shared.Next(1000, 3000));
}
