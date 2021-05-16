using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SanityClient;

namespace Tests
{
    public class SanityClientTests
    {
        public void ShouldThrowOnNullHttpClient
        {
            var client = new SanityClient.SanityClient();

        }

        //public async Task SimpleQuery()
        //{
        //    var client = new SanityClient.SanityClient(new System.Net.Http.HttpClient());
        //    var articles = await client.Query<Article>("*[_type == 'article']");
        //}

        //public async Task SimpleHttpResponseQuery()
        //{
        //    var client = new SanityClient.SanityClient(new System.Net.Http.HttpClient());
        //    var httpResponse = await client.Query("*[_type == 'article']");

        //    if (httpResponse.IsSuccessStatusCode())
        //    {
        //        // do something
        //    }
        //}

        //public async Task SimpleListener()
        //{
        //    var client = new SanityClient.SanityClient(new System.Net.Http.HttpClient());
        //    var listener = await client.Listen<T>("*[_type == 'article']");
        //    listener.OnEvent += HandleArticleListenerMessage;
        //    listener.OnError += HandleArticleListenerMessage;
        //    listener.Unsubscribe();
        //}

        //private Task HandleArticleListenerMessage<T>(ListenerEventArgs args)
        //{

        //}
    }

    class Article
    {
        public string Title { get; set; }
        public dynamic Description { get; set; }
    }
}
