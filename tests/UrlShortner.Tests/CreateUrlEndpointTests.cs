using FastEndpoints;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api.CreateUrl;

namespace UrlShortner.Tests
{
    public class CreateUrlEndpointTests : IClassFixture<ApiWebFactory>
    {
        private readonly ApiWebFactory _webFactory;
        private readonly HttpClient httpClient;

        public CreateUrlEndpointTests(ApiWebFactory apiWebFactory)
        {
            this._webFactory = apiWebFactory;
            this.httpClient=apiWebFactory.CreateClient();
        }

        [Fact]
        public async Task Should_be_able_to_Hit_The_Endpoint()
        {
            var request = new CreateUrlRequest
            {
                Url = "http://google.co.in"
            };

            var response = await this.httpClient
                .POSTAsync<CreateUrlEndpoint, CreateUrlRequest,CreateUrlResponse>(request);
            response.result.Url.Should().Be(request.Url);

        }
    }
}
