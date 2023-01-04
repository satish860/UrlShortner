global using FastEndpoints;
using Marten;
using Microsoft.Extensions.Options;
using UrlShortner.Api.CreateUrl;
using Weasel.Core;

namespace UrlShortner.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IStoreShortUrl, StoreShortUrl>();
            builder.Services.AddFastEndpoints();
            builder.Services.AddMarten(opt =>
            {
                opt.Connection(builder.Configuration.GetConnectionString("Default"));
                if (builder.Environment.IsDevelopment())
                {
                    opt.AutoCreateSchemaObjects = AutoCreate.All;
                }
            });

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseFastEndpoints();
            app.Run();
        }
    }
}