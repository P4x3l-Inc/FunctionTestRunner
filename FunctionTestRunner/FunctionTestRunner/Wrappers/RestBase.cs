using FluentAssertions;
using FunctionTestRunner.Models;
using FunctionTestRunner.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Xunit.Abstractions;

namespace FunctionTestRunner.Wrappers;

public abstract class RestBase
{
    protected ITestOutputHelper? TestOutputHelper { get; set; }
    protected CookieCollection? RequestCookieCollection { get; set; }
    protected string? BasePath { get; set; }
    protected abstract RestClient RestClient { get; }
    protected abstract ITestConfiguration Config { get; }

    /// <summary>
    /// Dont fail test, even if api-call fails.
    /// </summary>
    public static bool IgnoreApiStatusCodes { get; set; }

    protected async Task<RestResponse> Get(string path, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        var response = await Execute(request, httpStatus).ConfigureAwait(false);
        
        return response;
    }

    protected async Task<RestResponse> GetWithQueryParams(string path, Dictionary<string, string> args, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);
        foreach (var a in args)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        var response = await Execute(request, expectedResponse);

        return response;
    }

    protected async Task<RestResponse> GetWithHeaders(string path, Dictionary<string, string> header, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        var response = await Execute(request, httpStatus).ConfigureAwait(false);

        return response;
    }

    protected async Task<RestResponse> GetWithHeadersAndQueryParams(string path, Dictionary<string, string> header, Dictionary<string, string> args, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        foreach (var a in args)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        var response = await Execute(request, httpStatus).ConfigureAwait(false);

        return response;
    }

    protected async Task<T?> Get<T>(string path, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        var response = await Execute(request, httpStatus).ConfigureAwait(false);
        T? result = default;
        if (response.IsSuccessful)
            result = DeserializeResponse<T>(response);

        return result;
    }

    protected async Task<T?> GetWithQueryParams<T>(string path, Dictionary<string, string> args, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);
        foreach (var a in args)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        var response = await Execute(request, expectedResponse);

        T? result = default;
        if (response.IsSuccessful)
            result = DeserializeResponse<T>(response);

        return result;
    }

    protected async Task<T?> GetWithHeaders<T>(string path, Dictionary<string, string> header, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        var response = await Execute(request, httpStatus).ConfigureAwait(false);
        T? result = default;
        if (response.IsSuccessful)
            result = DeserializeResponse<T>(response);

        return result;
    }

    protected async Task<T?> GetWithHeadersAndQueryParams<T>(string path, Dictionary<string, string> header, Dictionary<string, string> args, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        foreach (var a in args)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        var response = await Execute(request, httpStatus).ConfigureAwait(false);
        T? result = default;
        if (response.IsSuccessful)
            result = DeserializeResponse<T>(response);

        return result;
    }

    protected async Task<RestResponse> Post(string path, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        return response;
    }

    protected async Task<RestResponse> PostWithHeader(string path, Dictionary<string, string> header, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        return response;
    }

    protected async Task<RestResponse> PostWithBody(string path, object? body = null, ApiFile? file = null, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body ?? string.Empty);

        if (file != null)
        {
            request.AddFile(file.FieldName, file.Content, file.FileName, file.ContentType);
        }

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        return response;
    }

    protected async Task<RestResponse> PostWithHeaderAndBody(string path, Dictionary<string, string> header, object body, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        request.AddJsonBody(body);

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        return response;
    }

    protected async Task<T> Post<T>(string path, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected async Task<T> PostWithHeaderAndBody<T>(string path, Dictionary<string, string> header, object body, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        request.AddJsonBody(body);

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected async Task<T> PostWithHeader<T>(string path, Dictionary<string, string> header, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected async Task<T> PostWithBody<T>(string path, object? body = null, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body ?? string.Empty);

        var response = await Execute(request, expectedResponse).ConfigureAwait(false);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected async Task<T> PutWithBody<T>(string path, object body)
    {
        var request = new RestRequest(path, Method.Put);
        request.RequestFormat = DataFormat.Json;

        request.AddJsonBody(body.ToString());

        var response = await Execute(request).ConfigureAwait(false);
        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected async Task Delete<T>(string path, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Delete);

        await Execute(request, expectedResponse).ConfigureAwait(false);
    }

    private static T DeserializeResponse<T>(RestResponse response, DateParseHandling? dateParseHandling = null)
    {
        T? deserializedResponse;
        try
        {
            if (dateParseHandling.HasValue)
            {
                deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings { DateParseHandling = dateParseHandling.Value });
            }
            else
            {
                deserializedResponse = JsonConvert.DeserializeObject<T>(response.Content);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Could not deserialize response {response.Content}, exception: {ex.Message}", ex);
        }

        return deserializedResponse;
    }

    private async Task<RestResponse> Execute(RestRequest request, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        //request.AddHeader("Cookie", string.Join(";", RequestCookieCollection != null ? RequestCookieCollection.GetAllCookies() : RestClient.CookieContainer));
        var response = await RestClient.ExecuteAsync(request).ConfigureAwait(false);
        if (!IgnoreApiStatusCodes)
        {
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                if (TestOutputHelper != null)
                {
                    TestOutputHelper.WriteLine("Received reponse status error, making one more attempt...");
                }
                Wait.ForSeconds(15);
                response = await RestClient.ExecuteAsync(request).ConfigureAwait(false);
            }


            if (TestOutputHelper != null)
            {
                TestOutputHelper.WriteLine(JsonConvert.SerializeObject(response));
            }

            response.ResponseStatus.Should().Be(ResponseStatus.Completed,
                $"Failed to complete API request: {response.ErrorMessage}.");
            response.StatusCode.Should().Be(expectedResponse,
                $"Failed to receive expected status code, error message: {response.ErrorMessage} content: {response.Content}.");
        }

        return response;
    }
    
}
