namespace FunctionTestRunner.Utils
{
    public interface ITestConfiguration
    {
        string GetApiBaseUrl();
        string GetApiKey();
        string GetEnvironment();
        string GetInternalClientBaseUrl();
    }
}