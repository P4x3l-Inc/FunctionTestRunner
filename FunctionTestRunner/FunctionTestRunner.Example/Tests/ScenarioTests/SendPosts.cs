using FunctionTestRunner.Example.Models;
using FunctionTestRunner.Example.Scenarios;
using FunctionTestRunner.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTestRunner.Example.Tests.ScenarioTests;

public class SendPosts
{
    private PostScenarios _scenario;

    public SendPosts()
    {
        _scenario = new PostScenarios();
    }

    [Fact]
    public async Task SendPosts_ShouldSendPosts()
    {
        await new TestRunner().RunAsync(async bag =>
        {

            // Arrange
            await _scenario.GenerateManyPosts(bag).ConfigureAwait(false);
            var posts = bag.Get<IEnumerable<Post>>(DataBagKey.GeneratedPosts);

            // Act

            // Assert
        });
    }
}
