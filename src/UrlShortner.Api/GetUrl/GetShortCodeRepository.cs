using Marten;
using UrlShortner.Api.Domain;

namespace UrlShortner.Api.GetUrl
{
    public class GetShortCodeRepository : IGetShortCodeRepository
    {
        private readonly IDocumentStore documentStore;

        public GetShortCodeRepository(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public async Task<Result<ShortUrl?>> GetUrlFrom(string shortCode)
        {
            using var session = this.documentStore.LightweightSession();
            var url = await session.Query<ShortUrl>()
                                   .Where(p => p.Id == shortCode)
                                   .FirstOrDefaultAsync();
            if(url == null)
            {
                return new Result<ShortUrl?>
                {
                    IsSucess = false,
                    Error = "URL Does not exist",
                    ErrorCode = 52
                };
            }
            return new Result<ShortUrl?>
            {
                Data = url
            };
        }
    }
}
