namespace BlockingCollectionExample;


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

    public static IEnumerable<string> Infinity()
    {
        // Leniwa kolekcja (Lazy Collection)
        while (true)
        {
            Thread.Sleep(1000);
            yield return "Sb";
        }

    }
}