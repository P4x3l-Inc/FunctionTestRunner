using FunctionTestRunner.Utils;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace FunctionTestRunner.Wrappers.Api;

public abstract class ApiBase : RestBase
{
    private RestClient _restClient;

    private static readonly object _syncRoot = new Object();

    protected void AddDefaultHeaders(Dictionary<string, string> headers)
    {
        _restClient.AddDefaultHeaders(headers);
    }

    protected Dictionary<string, string>? DefaultHeaders { get; set; }

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
                        var apiBaseUrl = new Uri(Config.GetApiBaseUrl(), UriKind.Absolute);

                        _restClient = new RestClient(apiBaseUrl);

                        if (DefaultHeaders != null && DefaultHeaders.Count > 0)
                        {
                            _restClient.AddDefaultHeaders(DefaultHeaders);
                        }

                        _restClient.UseNewtonsoftJson();
                    }
                }
            }

            return _restClient;
        }
    }
}
