using UrlShortner.Api.Domain;

namespace UrlShortner.Api.GetUrl
{
    public interface IGetShortCodeRepository
    {
        Task<Result<ShortUrl?>> GetUrlFrom(string shortCode);
    }
}