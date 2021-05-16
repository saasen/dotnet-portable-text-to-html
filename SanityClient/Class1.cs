using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SanityClient
{
    interface ISanityCdnClient
    {
        Task<T> Query<T>(string query, CancellationToken cancellationToken);
        //HttpResponseMessage Query(string query);
        //Task<T> GetDocument<T>(string query);
    }

    interface ISanityClient : ISanityCdnClient
    {
    }

    public class SanityClient : ISanityClient
    {
        private readonly HttpClient _httpClient;

        // To accept a httpclient or not to, or just httphandler
        // its nice to let the user pass httpclient so they can control how it is instantiated
        public SanityClient(HttpClient httpClient, SanityClientOptions options = null)
        {
            Guard.AgainstNull(options, nameof(options));
            Guard.AgainstNull(options.ProjectId, nameof(options.ProjectId));
            Guard.AgainstNull(options.Dataset, nameof(options.Dataset));

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri($"https://{options.ProjectId}.api.sanity.io/v2021-03-25/");
        }

        public async Task<T> Query<T>(string query, CancellationToken cancellationToken = default(CancellationToken))
        {
            // TODO: HTTP encode
            var response = await _httpClient.GetAsync($"data/query/production?query={query}", HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            var typedContent = await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(cancellationToken), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }, cancellationToken: cancellationToken);
            return typedContent;
        }
    }

    public class SanityClientOptions
    {

        public string ProjectId { get; set; }
        public string Dataset { get; set; }
        //public string Token { get; set; }
    }

    static class Guard
    {
        public static void AgainstNull(string value, string paramName, string message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void AgainstNull<T>(T value, string paramName, string message = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}

