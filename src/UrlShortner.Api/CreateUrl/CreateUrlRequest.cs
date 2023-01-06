namespace UrlShortner.Api.CreateUrl
{
    public class CreateUrlRequest
    {
        public string Url { get; set; } = string.Empty;

        public string AlternativeKey { get; set; } = string.Empty;

        /// <summary>
        /// User Name is not Mandatory. If User Name is provided
        /// then store the Url with the same.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
