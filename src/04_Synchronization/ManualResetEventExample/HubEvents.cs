namespace ManualResetEventExample;

internal class HubEvents
{
    delegate void WorkDelegate(int arg);
    WorkDelegate del;


    public HubEvents()
    {
        del += DoWork1;
        del += DoWork2;
        del += DoWork3;
    }

    public void SendSignal(int value)
    {
        del.Invoke(value);
    }

    private void DoWork1(int arg)
    {
        Console.WriteLine($"DoWork #1 Thread #{Thread.CurrentThread.ManagedThreadId} starting...");
        Thread.Sleep(5000);
        Console.WriteLine($"DoWork #1 Thread #{Thread.CurrentThread.ManagedThreadId} completed.");
    }

    private void DoWork2(int arg)
    {
        Console.WriteLine($"DoWork #2 Thread #{Thread.CurrentThread.ManagedThreadId} starting...");
        Thread.Sleep(1000);
        Console.WriteLine($"DoWork #2 Thread #{Thread.CurrentThread.ManagedThreadId} completed.");
    }

    private void DoWork3(int arg)
    {
        Console.WriteLine($"DoWork #3 Thread #{Thread.CurrentThread.ManagedThreadId} starting...");
        Thread.Sleep(1000);
        Console.WriteLine($"DoWork #3 Thread #{Thread.CurrentThread.ManagedThreadId} completed.");
    }


}
