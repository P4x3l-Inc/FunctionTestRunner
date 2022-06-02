namespace FunctionTestRunner.Example.Models;

public class Post
{
    public Post(string id, string title, string body, bool hasBeenSent)
    {
        this.id = id;
        this.title = title;
        this.body = body;
        this.hasBeenSent = hasBeenSent;
    }

    public string id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
    public bool hasBeenSent { get; set; }
}
