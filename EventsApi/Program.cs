using EventsApi.Endpoints;
using EventsApi.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Adding data contexts
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("IdentityDb"));
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddCors(options =>
{

});

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<IdentityUser>();

// Mapping endpoints
app.RegisterEventEndpoints();
app.RegisterUserEndpoints();

if (app.Environment.IsDevelopment())
{
    app.RegisterTestEndpoints();
}

app.Run();
