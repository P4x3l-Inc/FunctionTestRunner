using FunctionTestRunner.Utils;
using Xunit.Abstractions;

namespace FunctionTestRunner.Example.Utils
{
    public class ExampleTestRunner : TestRunner
    {
        public ExampleTestRunner(ITestOutputHelper? testOutputHelper = null) : base(testOutputHelper) { }

        protected override void CleanUpBag(ScenarioPropertyBag bag)
        {
            if (_testOutputHelper != null)
            {
                _testOutputHelper.WriteLine("CleanUpBag overrided in PostsTestRunner");
            }
        }

        protected override void BeforeRun()
        {
            if (_testOutputHelper != null)
            {
                _testOutputHelper.WriteLine("BeforeRun overrided in PostsTestRunner");
            }
        }
    }
}
