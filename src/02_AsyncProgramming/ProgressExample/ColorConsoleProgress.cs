namespace ProgressExample;

internal class ColorConsoleProgress : IProgress<int>
{
    public void Report(int value)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"...printing chunk {value + 1}");
        Console.ResetColor();
    }
}
