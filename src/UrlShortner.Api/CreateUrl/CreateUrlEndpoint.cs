using FastEndpoints;
using Nanoid;

namespace UrlShortner.Api.CreateUrl
{
    public class CreateUrlEndpoint : Endpoint<CreateUrlRequest, CreateUrlResponse>
    {
        private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public override void Configure()
        {
            Post("api/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUrlRequest req, CancellationToken ct)
        {
            var shortcode = await Nanoid.Nanoid.GenerateAsync(Alphabet, 10);
            var shortUrl = $"{BaseURL}{shortcode}";
            var response = new CreateUrlResponse
            {
                Url = shortUrl
            };
            await SendAsync(response, 200, ct);
        }
    }
}
