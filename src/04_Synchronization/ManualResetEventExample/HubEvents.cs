namespace ManualResetEventExample;


abstract class Event
{

}

internal class EventAggrerator
{
    Action<Event> del;

    public void Subscribe(Action<Event> del)
    {
        this.del += del;
    }

    public void SendEvent(Event value)
    {
        del.Invoke(value);
    }


}


internal class HubEvents
{
    public delegate Task WorkDelegate(int arg);
    public WorkDelegate del;

    public HubEvents()
    {
        del += DoWork1Async;
        del += DoWork2Async;
        del += DoWork3Async;
    }

    public void SendSignal(int value)
    {
        del.Invoke(value);
    }

    private Task DoWork1Async(int arg) => Task.Run(() => DoWork1(arg));
    private Task DoWork2Async(int arg) => Task.Run(() => DoWork2(arg));
    private Task DoWork3Async(int arg) => Task.Run(() => DoWork3(arg));

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
