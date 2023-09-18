namespace Irtl.Bff.Links.Model;

public class UrlLink
{
    public string Url { get; set; }

    public DateTimeOffset DateCreated { get; set; }

    public string Topic { get; set; }

    public List<string> Tags { get; set; }

    public string Note { get; set; }
}