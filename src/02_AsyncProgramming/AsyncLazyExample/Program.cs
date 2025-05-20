// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

PrinterService service = new PrinterService();
await service.PrintAndCalculateAsync();

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

// Podobnie jak Lazy<T>, ale działa z Task<T> - inicjalizacja wykonywana jest asynchronicznie tylko raz
// Użyteczne, gdy chcesz np. leniwie zainicjalizować połączenie do zdalnej bazy danych lub konfigurację pobieraną z sieci

public class AsyncLazy<T>
{
    private readonly Lazy<Task<T>> _lazyTask;

    public AsyncLazy(Func<Task<T>> factory)
    {
        _lazyTask = new Lazy<Task<T>>(factory);
    }

    public AsyncLazy(Lazy<Task<T>> lazyTask)
    {
        _lazyTask = lazyTask;
    }

    public Task<T> Value => _lazyTask.Value;
}


public class PrinterService
{
    private readonly AsyncLazy<Printer> lazyPrinter;

    public PrinterService()
    {
        this.lazyPrinter = new AsyncLazy<Printer>(CreateAsync);
    }

    private async Task<Printer> CreateAsync()
    {
        var printer = new Printer();
        await printer.InitAsync();

        return printer;
    }

    public async Task PrintAndCalculateAsync()
    {
        var printer = await GetPrinterAsync();

        await printer.PrintAsync("Document #1");

        await printer.PrintAsync("Document #2");

        await printer.PrintAsync("Document #3");
    }

    public Task<Printer> GetPrinterAsync() => lazyPrinter.Value;
}

public class Printer
{
    public Task InitAsync()
    {
        Console.WriteLine("Init Async called.");

        return Task.CompletedTask;
    }

    public Printer()
    {
        Console.WriteLine("Printer initalized.");
    }

    public void Print(string content)
    {
        Console.WriteLine($"Printing '{content}' on thread {Thread.CurrentThread.ManagedThreadId}...");
        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine($"Print completed.");
    }

    public Task PrintAsync(string content)
    {
        return Task.Run(() => Print(content));
    }
}