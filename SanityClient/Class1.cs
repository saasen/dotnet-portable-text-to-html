using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SanityClient
{
    interface ISanityCdnClient
    {
        Task<T> Query<T>(string query);
        //HttpResponseMessage Query(string query);
        //Task<T> GetDocument<T>(string query);
    }

    interface ISanityClient : ISanityCdnClient
    {
    }

    public class SanityClient : ISanityClient
    {
        private readonly HttpClient httpClient;

        // To accept a httpclient or not to, or just httphandler
        public SanityClient(HttpClient httpClient, SanityClientOptions options = null)
        {
            Guard.AgainstNull(options, nameof(options));
            Guard.AgainstNull(options.ProjectId, nameof(options.ProjectId));

            // Or store directly in httpclient?
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri($"https://{options.ProjectId}.api.sanity.io/v2021-03-25/");
        }

        public Task<T> Query<T>(string query)
        {

        }
    }

    public class SanityClientOptions
    {
        public string ProjectId { get; set; }
        //public string Token { get; set; }
    }

    static class Guard
    {
        public static void AgainstNull(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AgainstNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}

