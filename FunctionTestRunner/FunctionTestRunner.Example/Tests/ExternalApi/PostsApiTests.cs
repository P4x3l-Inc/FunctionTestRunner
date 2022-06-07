using FluentAssertions;
using FunctionTestRunner.Example.Api;
using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Utils;

namespace FunctionTestRunner.Example.Tests.ExternalApi;

public class PostsApiTests
{
    private readonly PostsApi _api;

    public PostsApiTests()
    {
        _api = new PostsApi();
    }

    [Fact]
    public async Task Test1()
    {
        await TestRunner.RunAsync(async bag =>
        {
            var postToCreate = new Post("test title", "test body");

            var response = await _api.Create(postToCreate);

            var post = await _api.Get(response.Id).ConfigureAwait(false);

            post.Should().NotBeNull();
            post.Id.Should().NotBeNullOrEmpty();
            post.HasBeenSent.Should().BeFalse();
            post.Title.Should().Be(postToCreate.Title);
            post.Body.Should().Be(postToCreate.Body);
        });
    }
}
