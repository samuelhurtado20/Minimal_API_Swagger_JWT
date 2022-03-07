using Minimal_API_Swagger_JWT.Interfaces;
using Minimal_API_Swagger_JWT.Models;
using Minimal_API_Swagger_JWT.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
app.UseSwagger();

app.MapPost("/create", (Movie movie, IMovieService service) =>
{
    var result = service.Create(movie);
    return Results.Ok(result);
});

app.MapGet("/get", (int id, IMovieService service) => 
{
    var result = service.Get(id);
    if (result is null) Results.NotFound("Not found a movie with id:" + id);
    return Results.Ok(result);
});

app.MapGet("/list", (IMovieService service) => 
{
    var result = service.List();
    return Results.Ok(result);
});

app.MapPut("/update", (Movie newMovie, IMovieService service) =>
{
    var result = service.Update(newMovie);
    if (result is null) Results.NotFound("Not found a movie with id:" + newMovie.Id);
    return Results.Ok(result);
});

app.MapDelete("/delete", (int id, IMovieService service) =>
{
    var result = service.Delete(id);
    if (!result) Results.BadRequest("Not found a movie with id:" + id);
    return Results.Ok(result);
});

app.UseSwaggerUI();
app.Run();
