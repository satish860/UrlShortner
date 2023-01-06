namespace UrlShortner.Api.GetUrl
{
    public class GetUrlEndpoint : Endpoint<GetUrlRequest>
    {
        private readonly IGetShortCodeRepository getShortCodeRepository;

        public GetUrlEndpoint(IGetShortCodeRepository getShortCodeRepository)
        {
            this.getShortCodeRepository = getShortCodeRepository;
        }

        public override void Configure()
        {
            Get("/{shortcode}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetUrlRequest req, CancellationToken ct)
        {
            var shortUrlResult = await this.getShortCodeRepository.GetUrlFrom(req.ShortCode);
            if (shortUrlResult.IsSucess == false)
            {
                await SendNotFoundAsync(ct);
            }
            else
            await SendRedirectAsync(shortUrlResult.Data.OriginalUrl,isPermanant:false,ct);
        }
    }
}
