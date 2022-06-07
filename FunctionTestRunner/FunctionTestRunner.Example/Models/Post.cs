namespace FunctionTestRunner.Example.Models;

public class Post
{
    public Post(string title, string body)
    {
        Title = title;
        Body = body;
    }

    public string? Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public bool? HasBeenSent { get; set; }
}
