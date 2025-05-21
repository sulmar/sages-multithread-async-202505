
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
    while (true)
    {
        Console.WriteLine($"Delayed: {DateTime.Now}");
        await DoSomethingAsync(); // 🕒 zajmuje 1 sekundę
        await Task.Delay(TimeSpan.FromSeconds(2), cts.Token); // ⏱ czeka 2 sekundy
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
    await Task.Delay(TimeSpan.FromSeconds(1));
}
