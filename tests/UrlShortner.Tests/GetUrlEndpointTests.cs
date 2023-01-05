using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api.GetUrl;

namespace UrlShortner.Tests
{
    public class GetUrlEndpointTests : IClassFixture<ApiWebFactory>
    {
        private readonly ApiWebFactory apiWebFactory;
        private readonly HttpClient httpClient;

        public GetUrlEndpointTests(ApiWebFactory apiWebFactory)
        {
            this.apiWebFactory = apiWebFactory;
            this.httpClient = apiWebFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task Should_be_to_Model_Bind_Short_code()
        {
            var response = await this.httpClient.GetAsync("/p2psps");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Should_be_To_Get_Redirect_response()
        {
            var response = await this.httpClient.GetAsync("/dssfs");
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Moved, response.StatusCode);
        }
    }
}
