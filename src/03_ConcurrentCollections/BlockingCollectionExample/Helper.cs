namespace BlockingCollectionExample;


// Pobierz dni tygodnia
internal class Helper
{
    public static IEnumerable<string> GetWeekdays()
    {
        var weekdays = new List<string>(); // zachłanna kolekcja (eager)
        weekdays.Add("Pn");
        weekdays.Add("Wt");
        weekdays.Add("Sr");
        weekdays.Add("Cz");
        weekdays.Add("Pt");
        weekdays.Add("Sb");
        weekdays.Add("Nd");

        return weekdays;

    }
}