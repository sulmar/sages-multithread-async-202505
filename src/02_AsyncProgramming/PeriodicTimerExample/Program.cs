
Console.WriteLine("Hello, PeriodTimer!");


using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (sender, e) =>
{
    Console.WriteLine("Stopping...");
    e.Cancel = true;
    cts.Cancel();
};

try
{
    while (!cts.IsCancellationRequested)
    {
        Console.WriteLine($"Heartbeat: {DateTime.Now}");
        await DoSomethingAsync(); // 🕒 zajmuje 1 sekundę
        await Task.Delay(TimeSpan.FromSeconds(1), cts.Token); // ⏱ czeka 1 sekundy
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
