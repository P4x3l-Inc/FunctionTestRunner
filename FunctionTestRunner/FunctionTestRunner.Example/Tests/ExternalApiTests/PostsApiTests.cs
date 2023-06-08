using FluentAssertions;
using FunctionTestRunner.Example.Api;
using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Example.Utils;
using System.Net;
using Xunit.Abstractions;

namespace FunctionTestRunner.Example.Tests.ExternalApiTests;

public class PostsApiTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PostsApi _api;

    public PostsApiTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _api = new PostsApi(testOutputHelper);
    }

    [Fact]
    public async Task PostsApi_ShouldHandleCrudOperations()
    {
        await new ExampleTestRunner(_testOutputHelper).RunAsync(async bag =>
        {
            var stamp = Guid.NewGuid();
            var postToCreate = new Post($"test title {stamp}", $"test body {stamp}");

            // Create Post
            var response = await _api.Create(postToCreate).ConfigureAwait(false);
            response.Id.Should().NotBeNullOrEmpty();

            // Get created Post
            var post = await _api.Get(response.Id).ConfigureAwait(false);

            post.Should().NotBeNull();
            post.Id.Should().Be(response.Id);
            post.HasBeenSent.Should().BeFalse();
            post.Title.Should().Be(postToCreate.Title);
            post.Body.Should().Be(postToCreate.Body);

            // Update Post
            stamp = Guid.NewGuid();
            post.Title = $"test title {stamp}";
            post.Body = $"test body {stamp}";

            var updatedPost = await _api.Update(post.Id, post).ConfigureAwait(false);
            updatedPost.Should().NotBeNull();
            updatedPost.Id.Should().Be(post.Id);
            updatedPost.Title.Should().Be(post.Title);
            updatedPost.Body.Should().Be(post.Body);

            // Delete
            await _api.Delete(updatedPost.Id).ConfigureAwait(false);

            var deletedResponse = await _api.GetWithHttpStatus(response.Id, HttpStatusCode.NotFound).ConfigureAwait(false);

            deletedResponse.Should().BeNull();

        }).ConfigureAwait(false);
    }
}
