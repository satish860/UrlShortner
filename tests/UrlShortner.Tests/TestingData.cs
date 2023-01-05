using Marten;
using Marten.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortner.Api.Domain;

namespace UrlShortner.Tests
{
    public class TestingData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            var shorturl = new List<ShortUrl>
            {
                new ShortUrl{Id="dssfs",Url="http://localhost/dssfs",OriginalUrl="https://google.co.in"},
                new ShortUrl{Id="dssfs1",Url="http://localhost/dssfs",OriginalUrl="https://google.co.in"},
                new ShortUrl{Id="dssfs2",Url="http://localhost/dssfs",OriginalUrl="https://google.co.in"},
                new ShortUrl{Id="dssfs3",Url="http://localhost/dssfs",OriginalUrl="https://google.co.in"},
            };
            using var session = store.LightweightSession();
            session.Store(shorturl.ToArray());
            await session.SaveChangesAsync();
        }
    }
}
