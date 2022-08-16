namespace PrismAndMauiApp1.Services.Interfaces;

using System.Threading.Tasks;

public interface IHttpRequestService
{
    string AuthenticationKey { set; get; }
    Task<TResult> GetAsync<TResult>(string uri, string authenticationToken = null, int timeout = 30);
    Task<TResult> PostAsync<TResult, T>(string uri, T data, string authenticationToken = null);
    Task<TResult> PostAsync<TResult>(string uri, string authenticationToken = null);
    Task<TResult> PutAsync<TResult, T>(string uri, T data, string authenticationToken = null);
    Task<TResult> DeleteAsync<TResult>(string uri, string authenticationToken = null);
    Task<Stream> GetStreamAsync(string uri, string authenticationToken = null);
    Task<Stream> PostStreamAsync<TResult, T>(string uri, T data, string authenticationToken = null);
}
