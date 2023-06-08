using Xunit.Abstractions;

namespace FunctionTestRunner.Utils;

public class TestRunner
{
    protected readonly ITestOutputHelper? _testOutputHelper;

    public TestRunner(ITestOutputHelper? testOutputHelper = null)
    {
        _testOutputHelper = testOutputHelper;
    }

    public void Run(Action<ScenarioPropertyBag> testMethod)
    {
        var bag = ScenarioPropertyBag.Create();
        try
        {
            BeforeRun();
            testMethod(bag);
        }
        finally
        {
            CleanUpBag(bag);
        }
    }

    public async Task RunAsync(Func<ScenarioPropertyBag, Task> testMethod)
    {
        var bag = ScenarioPropertyBag.Create();
        try
        {
            BeforeRun();
            await testMethod(bag);
        }
        finally
        {
            CleanUpBag(bag);
        }
    }

    protected virtual void CleanUpBag(ScenarioPropertyBag bag)
    {
        if (_testOutputHelper != null)
        {
            _testOutputHelper.WriteLine("CleanUpBag not implemented. Override this method to perform cleunup");
        }
    }

    protected virtual void BeforeRun()
    {
        if (_testOutputHelper != null)
        {
            _testOutputHelper.WriteLine("BeforeRun not implemented. Override this method to perform actions before testrun");
        }
    }
}
