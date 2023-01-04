using FastEndpoints;

namespace UrlShortner.Api.CreateUrl
{
    public class CreateUrlEndpoint : Endpoint<CreateUrlRequest, CreateUrlResponse>
    {
        public override void Configure()
        {
            Post("api/create");
            AllowAnonymous();
        }

        public override Task HandleAsync(CreateUrlRequest req, CancellationToken ct)
        {
            var response = new CreateUrlResponse
            {
                Url = req.Url
            };
            return SendAsync(response,200,ct);
        }
    }
}
