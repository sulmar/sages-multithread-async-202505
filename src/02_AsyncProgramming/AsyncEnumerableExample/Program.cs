
using AsyncEnumerableExample;

Console.WriteLine("Hello, AsyncEnumerable!");

CancellationTokenSource cts = new CancellationTokenSource(3000);

try
{
    await foreach (var weekday in Helper.Infinity(cts.Token))
    {
        Console.WriteLine(weekday);
    }
}
catch(TaskCanceledException)
{
    Console.WriteLine("Canceled.");
}



Console.WriteLine("Press any key to exit.");
Console.ReadKey();