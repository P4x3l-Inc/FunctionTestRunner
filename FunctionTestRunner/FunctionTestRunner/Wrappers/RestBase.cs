using FluentAssertions;
using FunctionTestRunner.Models;
using FunctionTestRunner.Utils;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace FunctionTestRunner.Wrappers;

public abstract class RestBase
{
    protected ITestOutputHelper? TestOutputHelper { get; set; }
    protected abstract RestClient RestClient { get; }
    protected abstract ITestConfiguration Config { get; }

    /// <summary>
    /// Dont fail test, even if api-call fails.
    /// </summary>
    public static bool IgnoreApiStatusCodes { get; set; }

    protected async Task<T?> Get<T>(string path, CookieCollection? cookies = null, HttpStatusCode httpStatus = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);

        if (cookies != null)
        {
            request.AddHeader("Cookie", cookies.ToString());
        }

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

    protected async Task<T> PostWithBody<T>(string path, object body = null, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body ?? string.Empty);

        if (TestOutputHelper != null)
        {
            TestOutputHelper.WriteLine("test test");
        }

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
    /*
    protected T GetWithUrlSegment<T>(string path, Dictionary<string, object> segments,
        HttpStatusCode expectedResponse = HttpStatusCode.OK, bool ignoreResponse = false)
    {
        var request = new RestRequest(path, Method.Get);
        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        var response = Execute(request, expectedResponse);

        if (!ignoreResponse)
        {
            var result = DeserializeResponse<T>(response);
            return result;
        }

        return default(T);
    }

    protected T GetWithQueryString<T>(string path, IEnumerable<Parameter> requestParams)
    {
        var request = new RestRequest(path, Method.Get);
        foreach (var requestParam in requestParams)
        {
            request.AddParameter(requestParam.Name, requestParam.Value, requestParam.Type);
        }

        var response = Execute(request);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected T GetWithQueryParams<T>(string path, Dictionary<string, string> args, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Get);
        foreach (var a in args)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void PostWithSegmentAndQueryParams(string path, Dictionary<string, object> segments, Dictionary<string, string> queryParams)
    {
        var request = new RestRequest(path, Method.Post);
        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }
        foreach (var a in queryParams)
        {
            request.AddQueryParameter(a.Key, a.Value);
        }

        Execute(request, HttpStatusCode.OK);

    }

    protected T PostWithParams<T>(string path, Dictionary<string, object> param, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        foreach (var item in param)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
        }
        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void Post(string path)
    {
        var request = new RestRequest(path, Method.Post);
        Execute(request);
    }

    protected void PostWithParams(string path, Dictionary<string, object> param)
    {
        var request = new RestRequest(path, Method.Post);
        foreach (var item in param)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
        }

        Execute(request);
    }

    protected void PostWithArrayParams(string path, Dictionary<string, object> param)
    {
        var request = new RestRequest(path, Method.Post);
        foreach (var item in param)
        {
            var array = item.Value as Array;
            if (array != null)
            {
                foreach (var arrayItem in array)
                {
                    request.AddParameter(item.Key, arrayItem, ParameterType.GetOrPost);
                }
            }
            else
            {
                request.AddParameter(item.Key, item.Value, ParameterType.GetOrPost);
            }
        }
        Execute(request);
    }


    protected T PostWithUrlSegment<T>(string path, Dictionary<string, object> segments, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void PostWithUrlSegment(string path, Dictionary<string, object> segments, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        Execute(request, expectedResponse);
    }

    protected T PostWithBody<T>(string path, object body = null, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body ?? string.Empty);

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void PostWithCamelCaseJsonBody(string path, object body, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.Post);
        request.JsonSerializer = new RestSharp.Serializers.JsonNetSerializer(new JsonSerializer
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters =
            {
              NodaConverters.LocalDateConverter
            }
        });
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body);

        Execute(request, expectedResponse);
    }
    protected T PostWithUrlSegmentAndCamelCaseJsonBody<T>(string path, Dictionary<string, object> segments, object body)
    {
        var request = new RestRequest(path, Method.POST);
        request.JsonSerializer = new RestSharp.Serializers.JsonNetSerializer(new JsonSerializer
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters =
            {
                NodaConverters.LocalDateConverter
            }
        });
        request.RequestFormat = DataFormat.Json;
        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }
        request.AddJsonBody(body);

        var response = Execute(request);
        var result = DeserializeResponse<T>(response);
        return result;
    }
    protected void PostWithBody(string path, object body, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.POST);
        request.RequestFormat = DataFormat.Json;
        request.AddJsonBody(body);

        Execute(request, expectedResponse);
    }

    protected T Post<T>(string path, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.POST);

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected T PostWithUrlSegmentAndBody<T>(string path, Dictionary<string, object> segments, object body,
        HttpStatusCode expectedResponse = HttpStatusCode.OK, DateParseHandling? dateParseHandling = null)
    {
        var request = new RestRequest(path, Method.POST);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        request.AddJsonBody(body);

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response, dateParseHandling);
        return result;
    }
    protected void PostWithUrlSegmentAndBody(string path, Dictionary<string, object> segments, object body,
       HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.POST);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        request.AddJsonBody(body);

        Execute(request, expectedResponse);
    }
    protected T PutWithUrlSegmentAndBody<T>(string path, Dictionary<string, object> segments, object body)
    {
        var request = new RestRequest(path, Method.PUT);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        request.AddJsonBody(body);

        var response = Execute(request);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    

    protected T PostWithUrlSegmentAndQuery<T>(string path, Dictionary<string, object> segments, Dictionary<string, object> query)
    {
        var request = new RestRequest(path, Method.POST);

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        foreach (var item in query)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
        }

        var response = Execute(request);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void PostWithUrlSegmentAndQuery(string path, Dictionary<string, object> segments, Dictionary<string, object> query, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.POST);

        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        foreach (var item in query)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
        }

        Execute(request, expectedResponse);

    }


    protected T PostWithHeaderAndBody<T>(string path, Dictionary<string, string> header, object body, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.POST);
        request.RequestFormat = DataFormat.Json;

        foreach (var item in header)
        {
            request.AddHeader(item.Key, item.Value);
        }

        request.AddJsonBody(body);

        var response = Execute(request, expectedResponse);

        var result = DeserializeResponse<T>(response);
        return result;
    }

    protected void DeleteWithUrlSegment(string path, Dictionary<string, object> segments, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        var request = new RestRequest(path, Method.DELETE);
        foreach (var item in segments)
        {
            request.AddParameter(item.Key, item.Value, ParameterType.UrlSegment);
        }

        Execute(request, expectedResponse);
    }

    */
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
            throw new ApplicationException($"Could not deserialize response {response.Content.ToString()}, exception: {ex.Message}", ex);
        }

        return deserializedResponse;
    }

    private async Task<RestResponse> Execute(RestRequest request, HttpStatusCode expectedResponse = HttpStatusCode.OK)
    {
        request.AddHeader("Cookie", String.Join(";", this.RestClient.CookieContainer));
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
