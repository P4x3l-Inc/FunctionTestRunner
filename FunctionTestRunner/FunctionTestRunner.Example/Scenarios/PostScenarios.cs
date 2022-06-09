using FunctionTestRunner.Example.Api;
using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Scenarios;
using FunctionTestRunner.Utils;

namespace FunctionTestRunner.Example.Scenarios;

public class PostScenarios : IScenarios
{
    public string ScenarioType() => "postsScenarios";

    public string SetupScenarios(ScenarioPropertyBag bag)
    {
        throw new NotImplementedException();
    }

    public void CleanupScenarios(ScenarioPropertyBag bag)
    {
        throw new NotImplementedException();
    }

    public async Task GenerateManyPosts(ScenarioPropertyBag bag, int numberOfPosts = 100)
    {
        var posts = new List<Post>();
        var api = new PostsApi();

        for (var i = 0; i < numberOfPosts; i++)
        {
            var post = new Post($"title_{Guid.NewGuid()}", $"body_{Guid.NewGuid()}");

            var createdPost = await api.Create(post).ConfigureAwait(false);
            posts.Add(createdPost);
        }

        bag.Set(DataBagKey.GeneratedPosts, posts);
    }
}
