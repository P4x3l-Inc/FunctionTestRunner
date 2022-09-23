using FunctionTestRunner.Exceptions;
using Microsoft.Extensions.Configuration;

namespace FunctionTestRunner.Utils;

public abstract class TestConfiguration : ITestConfiguration
{
    protected readonly IConfigurationRoot config;

    public TestConfiguration()
    {
        config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
    }

    public string GetEnvironment()
    {
        return GetAppSetting("environmentToTest");
    }

    public string GetApiKey()
    {
        return GetAppSetting("apiKey");
    }

    public string GetApiBaseUrl()
    {
        return GetAppSetting("apiBaseUrl");
    }

    public string GetUsername()
    {
        return GetAppSetting("username");
    }

    public string GetPassword()
    {
        return GetAppSetting("password");
    }

    // Add more internalAPi info

    public string GetInternalApiBaseUrl()
    {
        return GetAppSetting("internalApiBaseUrl");
    }

    public string GetLoginInternalApiUrl()
    {
        return GetAppSetting("loginInternalApiUrl");
    }

    protected string GetAppSetting(string appSetting)
    {

        var value = config[appSetting];

        if (value == null)
        {
            throw new AppSettingNotDefinedException($"AppSetting {appSetting} not defined");
        }

        return value;
    }
}
