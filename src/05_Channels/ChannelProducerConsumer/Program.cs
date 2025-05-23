
// Producer-Consument

using System.Threading.Channels;

Console.WriteLine("Hello, Channel.CreateUnbounded!");

// Tworzymy kanał z nieograniczoną pojemnością
var channel = Channel.CreateUnbounded<int>();

// Uruchamiamy producenta
var productTask = ProducerAsync(channel);

// Uruchamiamy konsumenta
var consumerATask = ConsumerAsync(channel, "A");

var consumerBTask = ConsumerAsync(channel, "B");

// Czekamy na zakończenie obu zadań
await Task.WhenAll(productTask, consumerATask, consumerBTask);


static async Task ProducerAsync(ChannelWriter<int> writer)
{
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine($"🛠 Producent: wysyłam {i}");

        // Dodaj element do kanału celem przetworzenia
        await writer.WriteAsync(i);

        await Task.Delay(500); // symulacja opóźnienia
    }

    writer.Complete(); // Zamyka kanał
}

static async Task ConsumerAsync(ChannelReader<int> reader, string name)
{
    await foreach (var item in reader.ReadAllAsync())
    {
        Console.WriteLine($"📦 Konsument-{name}: odebrałem {item}");
        await Task.Delay(1000); // symulacja przetwarzania
    }

    Console.WriteLine("Konsument zakończył odbieranie danych.");
}