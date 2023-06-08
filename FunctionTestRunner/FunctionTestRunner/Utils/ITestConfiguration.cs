namespace FunctionTestRunner.Utils;

public interface ITestConfiguration
{
    string GetApiBaseUrl();
    string GetApiKey();
    string GetEnvironment();
    string GetInternalApiBaseUrl();
    string GetUsername();
    string GetPassword();
    string GetLoginInternalApiUrl();
}