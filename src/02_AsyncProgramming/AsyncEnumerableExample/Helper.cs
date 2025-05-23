namespace AsyncEnumerableExample;

// Pobierz dni tygodnia
internal class Helper
{
    public static IEnumerable<string> GetWeekdays()
    {
        // Leniwa kolekcja (Lazy Collection)
        yield return "Pn";
        yield return "Wt";
        yield return "Sr";
        yield return "Cz";
        yield return "Pt";
        yield return "Sb";
        yield return "Nd";
    }

    public static async IAsyncEnumerable<double> Infinity()
    {
        // Leniwa kolekcja (Lazy Collection)
        while (true)
        {
            await Task.Delay(1000); // symulacja I/O np. odczyt temperatury

            yield return Random.Shared.NextDouble() * 100;
        }

    }
}