
using AsyncEnumerableExample;

Console.WriteLine("Hello, AsyncEnumerable!");


await foreach (var weekday in Helper.Infinity())
{
    Console.WriteLine(weekday);
}



Console.WriteLine("Press any key to exit.");
Console.ReadKey();