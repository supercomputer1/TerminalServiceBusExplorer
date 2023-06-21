using System.Text.Json;

namespace TerminalServiceBusExplorer.Terminal;

public class Message
{
    public Message(string id, string body, string contentType, DateTimeOffset enqueuedTime)
    {
        Id = id;
        Body = body;
        ContentType = contentType;
        EnqueuedTime = enqueuedTime;
    }

    public string Id { get; set; }
    public string Body { get; set; }
    public string ContentType { get; set; }
    public DateTimeOffset EnqueuedTime { get; }

    //var encoding = peekMessage.ApplicationProperties.GetValue<string>("X-Content-Encoding");

    public void Print()
    {
        Console.Clear();
        Console.WriteLine($"Id: {Id}, EnqueuedTime: {EnqueuedTime}.");
        Console.WriteLine("Body:");
        Console.WriteLine();

        var test = JsonDocument.Parse(Body);

        Console.WriteLine(JsonSerializer.Serialize(test, new JsonSerializerOptions() { WriteIndented = true }));
    }

    public void PrintWithoutBody()
    {
        Console.WriteLine($"Id: {Id}, EnqueuedTime: {EnqueuedTime}, ContentType: {ContentType}.");
    }
}
