namespace PrismAndMauiApp1.Services.Implementation;

using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using PrismAndMauiApp1.Events;
using PrismAndMauiApp1.Exceptions;
using PrismAndMauiApp1.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class HttpRequestService : IHttpRequestService
{
    private readonly IEventAggregator _eventAggregator;
    private bool AppIsOnline = true;
    private readonly JsonSerializerOptions _serializerSettings;
    private AsyncRetryPolicy<HttpResponseMessage> pollyPolicy;    
    public string AuthenticationKey { set; get; }

    public HttpRequestService(IEventAggregator eventAggregator)
    {
        _serializerSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            //DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        _serializerSettings.Converters.Add(new JsonStringEnumConverter());
        _eventAggregator = eventAggregator;
        _eventAggregator.GetEvent<AppOnlineEvent>().Subscribe(AppOnline);
        pollyPolicy = HttpPolicyExtensions.HandleTransientHttpError().Or<TimeoutRejectedException>().WaitAndRetryAsync(3,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
          );
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(60);
    }
    public void AppOnline(bool Online)
    {
        AppIsOnline = Online;
    }

    public async Task<TResult> GetAsync<TResult>(string uri, string authenticationToken = null, int timeout = 30)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.GetAsync(uri, ct), CancellationToken.None);
            var responseData = await response.Content.ReadAsStringAsync();
            //SI !IsSuccessStatusCode lanza Exception
            HandleResponse(response, responseData);

            httpClient.Dispose();

            return JsonSerializer.Deserialize<TResult>(responseData, _serializerSettings);
        }
        else
        {
            return default;
        }
    }

    public async Task<TResult> PostAsync<TResult, T>(string uri, T data, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var serialized = JsonSerializer.Serialize(data, _serializerSettings);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            try
            {
                var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.PostAsync(uri, content, ct), CancellationToken.None); ;

                var responseData = await response.Content.ReadAsStringAsync();
                //SI !IsSuccessStatusCode lanza Exception
                HandleResponse(response, responseData);

                httpClient.Dispose();

                var result = JsonSerializer.Deserialize<TResult>(responseData, _serializerSettings);

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            return default;
        }
    }

    public async Task<Stream> GetStreamAsync(string uri, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.GetAsync(uri, ct), CancellationToken.None);
            var responseData = await response.Content.ReadAsStreamAsync();
            //SI !IsSuccessStatusCode lanza Exception
            HandleResponse(response, response.ReasonPhrase);

            httpClient.Dispose();

            return responseData;
        }
        else
        {
            return default;
        }
    }

    public async Task<Stream> PostStreamAsync<TResult, T>(string uri, T data, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var serialized = JsonSerializer.Serialize(data, _serializerSettings);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            try
            {
                var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.PostAsync(uri, content, ct), CancellationToken.None);

                var responseData = await response.Content.ReadAsStreamAsync();
                //SI !IsSuccessStatusCode lanza Exception
                HandleResponse(response, response.ReasonPhrase);

                httpClient.Dispose();

                return responseData;

            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            return default;
        }
    }

    public async Task<TResult> PostAsync<TResult>(string uri, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.PostAsync(uri, null, ct), CancellationToken.None);
            var responseData = await response.Content.ReadAsStringAsync();
            //SI !IsSuccessStatusCode lanza Exception
            HandleResponse(response, responseData);

            httpClient.Dispose();

            return JsonSerializer.Deserialize<TResult>(responseData, _serializerSettings);
        }
        else
        {
            return default;
        }
    }

    public async Task<TResult> PutAsync<TResult, T>(string uri, T data, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var serialized = JsonSerializer.Serialize(data, _serializerSettings);
            var response = await pollyPolicy.ExecuteAsync(
                async ct => await httpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"), ct), CancellationToken.None);
            var responseData = await response.Content.ReadAsStringAsync();
            //SI !IsSuccessStatusCode lanza Exception
            HandleResponse(response, responseData);

            httpClient.Dispose();

            return JsonSerializer.Deserialize<TResult>(responseData, _serializerSettings);
        }
        else
        {
            return default;
        }
    }

    public async Task<TResult> DeleteAsync<TResult>(string uri, string authenticationToken = null)
    {
        if (AppIsOnline)
        {
            var httpClient = HandleAuthorization(authenticationToken);

            var response = await httpClient.DeleteAsync(uri);
            var responseData = await response.Content.ReadAsStringAsync();
            //SI !IsSuccessStatusCode lanza Exception
            HandleResponse(response, responseData);

            httpClient.Dispose();

            return JsonSerializer.Deserialize<TResult>(responseData, _serializerSettings);
        }
        else
        {
            return default;
        }
    }

    private HttpClient CreateHttpClient()
    {
        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return httpClient;
    }

    private void HandleResponse(HttpResponseMessage response, string responseData)
    {
        if (!response.IsSuccessStatusCode)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Forbidden | HttpStatusCode.Unauthorized:
                    throw new ServiceAuthenticationException(responseData);

                case HttpStatusCode.InternalServerError:
                    throw new HttpRequestException(responseData);
            }

            try
            {
                throw JsonSerializer.Deserialize<ApiErrorResult>(responseData, _serializerSettings);
            }
            catch (JsonException)
            {
                throw new HttpRequestException(responseData);
            }
        }
    }

    private HttpClient HandleAuthorization(string authenticationToken)
    {
        var httpClient = CreateHttpClient();

        if (!string.IsNullOrEmpty(authenticationToken))
        {
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", authenticationToken);
        }
        if (!string.IsNullOrEmpty(AuthenticationKey))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", AuthenticationKey);
        }
        else
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        return httpClient;
    }
}
