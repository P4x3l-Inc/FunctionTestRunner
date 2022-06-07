using FunctionTestRunner.Exceptions;
using System.Configuration;

namespace FunctionTestRunner.Utils;

public static class TestConfiguration
{
    public static string GetEnvironment()
    {
        return GetAppSetting("environmentToTest");
    }

    public static string GetApiKey()
    {
        return GetAppSetting("apiKey");
    }

    public static string GetApiBaseUrl()
    {
        return GetAppSetting("apiBaseUrl");
    }

    // Add more internalAPi info

    public static string GetInternalClientBaseUrl()
    {
        return GetAppSetting("internalApiBaseUrl");
    }

    private static string GetAppSetting(string appSetting)
    {
        
        var value = ConfigurationManager.AppSettings[appSetting];

        if (value == null)
        {
            throw new AppSettingNotDefinedException($"AppSetting {appSetting} not defined");
        }

        return value;
    }
}
