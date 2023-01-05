using FluentAssertions;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api.CreateUrl;

namespace UrlShortner.Tests
{
    public class StoreUrlTests : IClassFixture<ApiWebFactory>
    {
        private readonly ApiWebFactory apiWebFactory;
        private readonly IDocumentStore store;

        public StoreUrlTests(ApiWebFactory apiWebFactory)
        {
            this.apiWebFactory = apiWebFactory;
            this.store = apiWebFactory.Services.GetRequiredService<IDocumentStore>();

        }

        [Fact]
        public async Task Should_be_Able_to_Store_the_Value()
        {
            StoreShortUrl storeShortUrl = new StoreShortUrl(this.store);
           var result= await storeShortUrl.CreateShortUrl(new Api.Domain.ShortUrl
            {
                Url = "http://localhost/pqrst",
                Id = "pqrst",
                OriginalUrl = "http://google/.co.in"
            });
            result.IsSucess.Should().BeTrue();
            result.Data.Should().Be("pqrst");
            
        }

        [Fact]
        public async Task Should_Get_a_Validation_error_if_Trying_to_Store_A_Same_Key()
        {
            StoreShortUrl storeShortUrl = new StoreShortUrl(this.store);
            await storeShortUrl.CreateShortUrl(new Api.Domain.ShortUrl
            {
                Url = "http://localhost/pqrst",
                Id = "pqrst1",
                OriginalUrl = "http://google/.co.in"
            });

            var result = await storeShortUrl.CreateShortUrl(new Api.Domain.ShortUrl
            {
                Url = "http://localhost/pqrst",
                Id = "pqrst1",
                OriginalUrl = "http://google/.co.in"
            });
            result.IsSucess.Should().BeFalse();
        }
    }
}
