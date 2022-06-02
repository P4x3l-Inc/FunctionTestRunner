using FunctionTestRunner.Utils;

namespace FunctionTestRunner.Scenarios;

public interface IScenarios
{
    string ScenarioType();
    string SetupScenarios(ScenarioPropertyBag bag);
    void CleanupScenarios(ScenarioPropertyBag bag);
}
