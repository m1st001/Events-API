using EventsApi.Data;
using EventsApi.Data.Models;
using EventsApi.Identity;
using Microsoft.AspNetCore.Identity;

namespace EventsApi.Endpoints
{
    /// <summary>
    /// A set of endpoints for testing purposes.
    /// </summary>
    public static class TestEndpoints
    {
        public static void RegisterTestEndpoints(this WebApplication app)
        {
            var test = app.MapGroup("/test");

            test.MapGet("/setupdb", async (AppDbContext db, IdentityDbContext idb) =>
            {
                await db.Events.AddRangeAsync(new List<Event>()
                {
                    new Event(),
                    new Event(),
                    new Event()
                });

                await idb.Users.AddRangeAsync(new List<IdentityUser>()
                {
                    new IdentityUser("Beeba"),
                    new IdentityUser("Chomas"),
                });

                await db.SaveChangesAsync();
                await idb.SaveChangesAsync();
            });
        }
    }
}
