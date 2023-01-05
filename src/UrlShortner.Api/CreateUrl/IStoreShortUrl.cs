using UrlShortner.Api.Domain;

namespace UrlShortner.Api.CreateUrl
{
    public interface IStoreShortUrl
    {
        Task<Result<string>> CreateShortUrl(ShortUrl shortUrl);
    }
}
