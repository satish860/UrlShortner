namespace UrlShortner.Api.Domain
{
    public class Result<T>
    {
        public T? Data { get; set; } = default(T);

        public bool IsSucess { get; set; } = true;

        public string Error { get; set; } = string.Empty;

    }
}
