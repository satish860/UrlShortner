using Marten;
using UrlShortner.Api.Domain;

namespace UrlShortner.Api.CreateUrl
{
    public class StoreShortUrl : IStoreShortUrl
    {
        private readonly IDocumentStore documentStore;

        public StoreShortUrl(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public async Task<string> CreateShortUrl(ShortUrl shortUrl)
        {
            using var session = this.documentStore.LightweightSession();
            session.Store(shortUrl);
            await session.SaveChangesAsync();
            return shortUrl.Id;
        }
    }
}
