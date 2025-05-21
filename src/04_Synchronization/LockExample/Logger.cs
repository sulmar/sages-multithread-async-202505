namespace LockExample;

// Wzorzec projektowy Singleton
internal class Logger
{
    private static object _lock = new();

    private static Logger _instance;
    public static Logger Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                    _instance = new Logger();

                return _instance;
            }
        }

    }

    protected Logger()
    {
        Console.WriteLine("ctor");

        Thread.Sleep(500);
    }



    public void Log(string message)
    {
        Console.WriteLine($"[Log] {DateTime.UtcNow}: {message}");
    }
}
