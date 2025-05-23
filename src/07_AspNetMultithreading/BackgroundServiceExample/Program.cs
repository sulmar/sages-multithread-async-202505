using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<EmailQueue>();
builder.Services.AddHostedService<EmailSenderService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Producer
app.MapPost("send-email", async (EmailRequest request, EmailQueue queue) =>
{
    // Dodanie wiadomoœci do kana³u
    await queue.EnqueEmail(request);

    return Results.Accepted(); // 202
});

app.Run();

// Wzorzec projektowy Facade
public class EmailQueue
{
    private readonly Channel<EmailRequest> _channel = Channel.CreateUnbounded<EmailRequest>();
    public async ValueTask EnqueEmail(EmailRequest request) => await _channel.Writer.WriteAsync(request);
    public IAsyncEnumerable<EmailRequest> ReadAllAsync(CancellationToken cancellationToken) => _channel.Reader.ReadAllAsync(cancellationToken);
}

public record EmailRequest(string To, string Subject, string Body);

// Consument
public class EmailSenderService(ILogger<EmailSenderService> _logger, EmailQueue _queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var email in _queue.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation("Wysy³anie maila do {To}", email.To);
            await Task.Delay(TimeSpan.FromSeconds(10)); // Symulacja opóŸnienia
        }
    }
}