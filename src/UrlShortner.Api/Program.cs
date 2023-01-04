global using FastEndpoints;
using Marten;
using Microsoft.Extensions.Options;
using Weasel.Core;

namespace UrlShortner.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddFastEndpoints();

            // Add services to the container.

           
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