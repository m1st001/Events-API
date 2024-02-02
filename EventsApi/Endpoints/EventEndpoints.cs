using EventsApi.Data;
using EventsApi.Data.DTOs;
using EventsApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Endpoints
{
    /// <summary>
    /// Extension class for defining Event endpoints.
    /// </summary>
    public static class EventEndpoints
    {
        /// <summary>
        /// Map out Event-related endpoints.
        /// </summary>
        /// <param name="app"></param>
        public static void RegisterEventEndpoints(this WebApplication app)
        {
            var events = app.MapGroup("/events").RequireAuthorization();

            #region EndpointDeclarations

            events.MapGet("/", async (AppDbContext db) => await db.Events.Select(x => new EventDTO(x)).ToArrayAsync());

            events.MapGet("/{id}", GetEventById);

            events.MapGet("/open", async (AppDbContext db) => await db.Events.Where(x => x.StartDate > DateTime.Now).ToListAsync());

            events.MapGet("/user/{authorId}", GetEventsByAuthor);

            events.MapPost("/", CreateEvent);

            #endregion

            #region EndpointDefinitions

            static async Task<IResult> GetEventById(int id, AppDbContext db)
            {
                return await db.Events.FindAsync(id)
                    is Event item
                        ? TypedResults.Ok(new EventDTO(item))
                        : TypedResults.NotFound();
            }

            static async Task<IResult> CreateEvent(EventDTO todoItemDTO, AppDbContext db)
            {
                var item = new Event
                {

                };

                db.Events.Add(item);
                await db.SaveChangesAsync();

                EventDTO eventDTO = new EventDTO(item);
                return TypedResults.Created($"/events/{item.Id}", todoItemDTO);
            }

            static async Task<IResult> GetEventsByAuthor(int authorId, AppDbContext db)
            {
                return TypedResults.Ok(await db.Events.Where(x => x.AuthorId == authorId).ToListAsync());
            }

            #endregion
        }
    }
}
