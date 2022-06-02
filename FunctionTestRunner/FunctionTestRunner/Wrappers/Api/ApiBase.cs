using FunctionTestRunner.Utils;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace FunctionTestRunner.Wrappers.Api;

public abstract class ApiBase : RestBase
{
    private RestClient _restClient;
    private static readonly object _syncRoot = new Object();

    protected override RestClient RestClient
    {
        get
        {
            if (_restClient == null)
            {
                lock (_syncRoot)
                {
                    if (_restClient == null)
                    {
                        var apiBaseUrl = TestConfiguration.GetApiBaseUrl();
                        var apiKey = TestConfiguration.GetApiKey();

                        _restClient = new RestClient(apiBaseUrl);
                        if (apiKey != null)
                        {
                            _restClient.AddDefaultHeader("apikey", apiKey);
                        }

                        _restClient.UseNewtonsoftJson();
                    }
                }
            }

            return _restClient;
        }
    }
}
