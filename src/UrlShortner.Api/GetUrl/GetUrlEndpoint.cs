namespace UrlShortner.Api.GetUrl
{
    public class GetUrlEndpoint : Endpoint<GetUrlRequest>
    {
        public override void Configure()
        {
            Get("/{shortcode}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetUrlRequest req, CancellationToken ct)
        {
            await SendAsync(req.ShortCode, 200, ct);
        }
    }
}
