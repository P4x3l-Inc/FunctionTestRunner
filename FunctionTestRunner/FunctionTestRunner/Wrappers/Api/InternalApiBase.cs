using RestSharp;
using System.Net;

namespace FunctionTestRunner.Wrappers.Api
{
    public abstract class InternalApiBase : RestBase
    {
        protected RestClient _restClient;
        protected static CookieContainer _cookieContainer;
        protected static readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
        protected static readonly object _syncRoot2 = new();

        protected override RestClient RestClient
        {
            get
            {
                if (_restClient == null)
                {
                    lock (_syncRoot2)
                    {
                        if (_restClient == null)
                        {
                            _restClient = new RestClient(Config.GetInternalApiBaseUrl());
                            foreach (var cookie  in _cookieContainer.GetAllCookies().AsEnumerable())
                            {
                                _restClient.AddCookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
                            }
                        }
                    }
                }

                return _restClient;
            }

        }

        public virtual async Task LoginIfNeeded()
        {
            if (_cookieContainer == null)
            {
                await _lock.WaitAsync();
                if (_cookieContainer == null)
                {
                    try
                    {
                        var loggedInRestClient = new RestClient(Config.GetLoginInternalApiUrl());

                        var request = new RestRequest("", Method.Post);
                        request.RequestFormat = DataFormat.Json;
                        var body = new { Email = Config.GetUsername(), Password = Config.GetPassword() };
                        request.AddJsonBody(body);

                        await loggedInRestClient.ExecuteAsync(request);

                        _cookieContainer = loggedInRestClient.CookieContainer;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        _lock.Release();
                    }
                }
            }
        }
    }
}
