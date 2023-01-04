using UrlShortner.Api.Domain;

namespace UrlShortner.Api.CreateUrl
{
    public interface IStoreShortUrl
    {
        Task<string> CreateShortUrl(ShortUrl shortUrl);
    }
}
