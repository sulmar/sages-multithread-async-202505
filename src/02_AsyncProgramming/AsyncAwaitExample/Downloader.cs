namespace AsyncAwaitExample;

internal class Downloader
{
    public async Task DownloadImage(object? index)
    {
        Console.WriteLine($"{Thread.CurrentThread.IsThreadPoolThread} - {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId} - Start");

        using var client = new HttpClient();
        var task = client.GetByteArrayAsync($@"https://picsum.photos/200");
        var bytes = await task;

        Directory.CreateDirectory("images");
        File.WriteAllBytes($"images\\image_{index}_{Guid.NewGuid()}.jpg", bytes);
        File.WriteAllBytes($"image_{index}.jpg", bytes);
        client.Dispose();

        Console.WriteLine($"{Thread.CurrentThread.IsThreadPoolThread} - {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId} - Koniec");
    }
}
