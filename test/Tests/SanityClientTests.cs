using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentAssertions;
using RichardSzalay.MockHttp;
using SanityClient;
using Snapshooter.Xunit;
using Xunit;

namespace Tests
{
    public class SanityClientTests
    {
        [Fact]
        public async Task ShouldThrowOnNullHttpClient()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .When("https://123456.api.sanity.io/v2021-03-25/data/query/production?query=*[_type == 'article']")
                .Respond("application/json", @"[{""_id"": ""1234"", ""_type"" : ""article"", ""title"": ""title1""}]");

            var client = new SanityClient.SanityClient(mockHttp.ToHttpClient(), new SanityClientOptions
            {
                ProjectId = "123456",
                Dataset = "production"
            });

            var result = await client.Query<Article[]>("*[_type == 'article']");
            result.Should().HaveCount(1);
            result[0].Id.Should().Be("1234");
            result[0].Type.Should().Be("article");
            result[0].Title.Should().Be("title1");
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

    class SanityDocument
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("_type")]
        public string Type { get; set; }
    }

    class Article : SanityDocument
    {
        public string Title { get; set; }
    }
}
