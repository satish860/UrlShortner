using FluentAssertions;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api.CreateUrl;
using UrlShortner.Api.GetUrl;

namespace UrlShortner.Tests
{
    public class GetUrlRepositoryTests:IClassFixture<ApiWebFactory>
    {
        private readonly ApiWebFactory apiWebFactory;
        private readonly IDocumentStore store;  

        public GetUrlRepositoryTests(ApiWebFactory apiWebFactory)
        {
            this.apiWebFactory = apiWebFactory;
            this.store = apiWebFactory.Services.GetRequiredService<IDocumentStore>();
        }

        [Fact]
        public async Task Should_Throw_Validation_error_if_Shortcode_Does_not_exist()
        {
            GetShortCodeRepository getShortCodeRepository = new GetShortCodeRepository(this.store);
            var result = await getShortCodeRepository.GetUrlFrom("sda");
            result.ErrorCode.Should().Be(52);
        }

        [Fact]
        public async Task Should_be_able_to_Get_ShortCode()
        {
            StoreShortUrl storeShortUrl = new StoreShortUrl(this.store);
            await storeShortUrl.CreateShortUrl(new Api.Domain.ShortUrl
            {
                Id = "psqrt",
                OriginalUrl = "https://google.co.in",
                Url = "http://localhost/psqrt"
            });
            GetShortCodeRepository getShortCodeRepository = new GetShortCodeRepository(this.store);
            var result = await getShortCodeRepository.GetUrlFrom("psqrt");
            result!.Data!.OriginalUrl.Should().Be("https://google.co.in");
        }
    }
}
