using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Wrappers.Api;

namespace FunctionTestRunner.Example.Api;

public class PostsApi : ApiBase
{
    private string basePath = "posts";
    public async Task<Post> Create(Post post)
    {
        var response = await PostWithBody<Post>(basePath, post);

        return response;
    }

    public async Task<Post> Get(string id)
    {
        var response = await Get<Post>($"{basePath}/{id}");

        return response;
    }

    public async Task<Post> Update(string id, Post post)
    {
        var response = await PutWithBody<Post>($"{basePath}/{id}", post);

        return response;
    }

    public async Task Delete(string id)
    {
        await Delete($"{basePath}/{id}");
    }
}
