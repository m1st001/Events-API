using EventsApi.Data.DTOs;
using EventsApi.Data.Models;
using EventsApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EventsApi.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Endpoints
{
    /// <summary>
    /// Extension class for defining User endpoints.
    /// </summary>
    public static class UserEndpoints
    {
        /// <summary>
        /// Map out user-related endpoints.
        /// </summary>
        /// <param name="app"></param>
        public static void RegisterUserEndpoints(this WebApplication app)
        {
            var users = app.MapGroup("/users").RequireAuthorization();

            #region EndpointDeclarations

            users.MapGet("/", async (IdentityDbContext idb) => await idb.Users.ToListAsync());

            users.MapGet("/{Id}", GetUserById);

            users.MapPost("/logout", async (SignInManager<IdentityUser> signInManager,[FromBody] object empty) =>
            {
                if (empty != null)
                {
                    await signInManager.SignOutAsync();
                    return Results.Ok();
                }
                return Results.Unauthorized();

            });

            #endregion

            #region EndpointDefinitions

            static async Task<IResult> GetUserById(int id, IdentityDbContext idb)
            {
                return await idb.Users.FindAsync(id)
                    is IdentityUser user
                        ? TypedResults.Ok(user)
                        : TypedResults.NotFound();
            }


            #endregion
        }
    }
}
