using FunctionTestRunner.Example.Configuration;
using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Utils;
using FunctionTestRunner.Wrappers.Api;
using System.Configuration;
using System.Net;

namespace FunctionTestRunner.Example.Api;

public class PostsApi : ApiBase
{
    private readonly string basePath;
    private readonly Settings settings;

    protected override ITestConfiguration Config => settings;

    public PostsApi()
    {
        basePath = "posts";
        settings = new Settings();

        DefaultHeaders = new Dictionary<string, string>
        {
            { "apikey", settings.GetApiKey() }
        };
    }

    public async Task<Post> Create(Post post)
    {
        var response = await PostWithBody<Post>(basePath, post, HttpStatusCode.Created).ConfigureAwait(false);

        return response;
    }

    public async Task<Post?> Get(string id)
    {
        var response = await Get<Post>($"{basePath}/{id}").ConfigureAwait(false);

        return response;
    }

    public async Task<Post?> GetWithHttpStatus(string id, HttpStatusCode httpStatus)
    {
        var response = await Get<Post>($"{basePath}/{id}", httpStatus).ConfigureAwait(false);

        return response;
    }

    public async Task<Post> Update(string id, Post post)
    {
        var response = await PutWithBody<Post>($"{basePath}/{id}", post).ConfigureAwait(false);

        return response;
    }

    public async Task Delete(string id)
    {
        await Delete<Post>($"{basePath}/{id}").ConfigureAwait(false);
    }
}
