using Models.BaseModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClientApp.Services
{
    public class BackEndHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BackEndHttpClient> _logger;

        public BackEndHttpClient(HttpClient httpClient, ILogger<BackEndHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(HttpMethod httpMethod, string method, object? data = null)
        {
            var message = new HttpRequestMessage(httpMethod, method);

            if (data != null)
            {
                var postContent = JsonConvert.SerializeObject(data);
                message.Content = new StringContent(postContent, Encoding.UTF8, "application/json");
            }

            return message;
        }

        private async Task<Result<T>> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken, string method, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var response = await _httpClient.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error calling API {method}: {response.StatusCode} - {response.ReasonPhrase} \r\n {errorContent}");
                }

                var returned = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result<T>>(returned);

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException(), $"Client base service call {method} exception: Method - {memberName} in {sourceFilePath} on {sourceLineNumber}");
                return Result<T>.Failure(ex.GetBaseException().Message);
            }
        }

        private async Task<Result> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken, string method, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var response = await _httpClient.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error calling API {method}: {response.StatusCode} - {response.ReasonPhrase} \r\n {errorContent}");
                }

                var returned = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Result>(returned);

                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException(), $"Client base service call {method} exception: Method - {memberName} in {sourceFilePath} on {sourceLineNumber}");
                return Result.Failure(ex.GetBaseException().Message);
            }
        }

        public async Task<Result<T>> GetDataAsync<T>(string method, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            var request = await CreateHttpRequestMessageAsync(HttpMethod.Get, method);
            return await SendAsync<T>(request, cancellationToken, method, memberName, sourceFilePath, sourceLineNumber);
        }

        public async Task<Result<T>> PostDataAsync<T>(string method, object data, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            var request = await CreateHttpRequestMessageAsync(HttpMethod.Post, method, data);
            return await SendAsync<T>(request, cancellationToken, method, memberName, sourceFilePath, sourceLineNumber);
        }

        public async Task<Result> GetDataAsync(string method, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            var request = await CreateHttpRequestMessageAsync(HttpMethod.Get, method);
            return await SendAsync(request, cancellationToken, method, memberName, sourceFilePath, sourceLineNumber);
        }

        public async Task<Result> PostDataAsync(string method, object data, CancellationToken cancellationToken = default, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            var request = await CreateHttpRequestMessageAsync(HttpMethod.Post, method, data);
            return await SendAsync(request, cancellationToken, method, memberName, sourceFilePath, sourceLineNumber);
        }
    }
}
