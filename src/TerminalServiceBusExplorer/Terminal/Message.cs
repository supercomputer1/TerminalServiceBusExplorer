namespace TerminalServiceBusExplorer.Terminal;

public class Message
{
    public Message(string id, string body, string contentType, string contentEncoding, DateTimeOffset enqueuedTime)
    {
        Id = id;
        Body = body;
        ContentType = contentType;
        ContentEncoding = contentEncoding;
        EnqueuedTime = enqueuedTime;
    }

    public string Id { get; set; }
    public string Body { get; set; }
    public string ContentType { get; set; }
    public string ContentEncoding { get; set; }
    public DateTimeOffset EnqueuedTime { get; }
}
