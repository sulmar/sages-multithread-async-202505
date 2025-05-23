
// Producer-Consument

using System.Threading.Channels;

Console.WriteLine("Hello, Channel.CreateBounded!");

var options = new BoundedChannelOptions(2)
{
        FullMode = BoundedChannelFullMode.DropWrite , // TODO: Pytanie: czy producent moze otrzymac informację, że jego dokument został porzucony?    
    };


// Tworzymy kanał z ograniczoną pojemnością
var channel = Channel.CreateBounded<int>(options);

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

        await Task.Delay(100); // symulacja opóźnienia
    }

    writer.Complete(); // Zamyka kanał
}

static async Task ConsumerAsync(ChannelReader<int> reader, string name)
{
    await foreach (var item in reader.ReadAllAsync())
    {
        Console.WriteLine($"📦 Konsument-{name}: odebrałem {item}");
        await Task.Delay(2000); // symulacja przetwarzania
    }

    Console.WriteLine("Konsument zakończył odbieranie danych.");
}