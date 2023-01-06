using FastEndpoints;
using Nanoid;
using UrlShortner.Api.GetUrl;

namespace UrlShortner.Api.CreateUrl
{
    public class CreateUrlEndpoint : Endpoint<CreateUrlRequest, CreateUrlResponse>
    {
        private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly IStoreShortUrl storeShortUrl;

        public CreateUrlEndpoint(IStoreShortUrl storeShortUrl)
        {
            this.storeShortUrl = storeShortUrl;
        }

        public override void Configure()
        {
            Post("api/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUrlRequest req, CancellationToken ct)
        {
            var shortcode = await Nanoid.Nanoid.GenerateAsync(Alphabet, 10);
            var shortUrl = $"{BaseURL}{shortcode}";

            var savedShortcode = await this.storeShortUrl.CreateShortUrl(new Domain.ShortUrl
            {
                Id = shortcode,
                OriginalUrl = shortUrl,
                Url = req.Url
            });

            var response = new CreateUrlResponse
            {
                Url = shortUrl
            };
            await SendCreatedAtAsync<GetUrlEndpoint>(new { shortcode = shortcode }, response, cancellation: ct);
        }
    }
}
