using Marten;
using Marten.Services;
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

        public async Task<Result<string>> CreateShortUrl(ShortUrl shortUrl)
        {
            using var session = this.documentStore.LightweightSession();
            var valueExist = session.Query<ShortUrl>().Where(p => p.Id == shortUrl.Id).FirstOrDefault();
            if (valueExist == null)
            {
                session.Store(shortUrl);
                await session.SaveChangesAsync();
            }
            else
            {
                return new Result<string> { IsSucess = false, Error = "Key Already Exist. Try again" };
            }
            return new Result<string> { Data=shortUrl.Id};
        }
    }
}
