using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Minimal_API_Swagger_JWT.Interfaces;
using Minimal_API_Swagger_JWT.Models;
using Minimal_API_Swagger_JWT.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    { 
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();

var app = builder.Build();
app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();

app.MapPost("/login", (UserLogin user, IUserService service) =>
{
    if(!String.IsNullOrEmpty(user.Password) && !String.IsNullOrEmpty(user.UserName))
    {
        var logInUser = service.Get(user);
        if (logInUser is null) return Results.NotFound("User not found");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, logInUser.Username ?? ""),
            new Claim(ClaimTypes.Email, logInUser.EmailAddress ?? ""),
            new Claim(ClaimTypes.GivenName, logInUser.GivenName ?? ""),
            new Claim(ClaimTypes.Surname, logInUser.Surname ?? ""),
            new Claim(ClaimTypes.Role, logInUser.Role ?? ""),
        };

        var token = new JwtSecurityToken
        (
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256
                )
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Results.Ok(tokenString);
    }

    return Results.NotFound("User not found");
});


app.MapPost("/create",
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
(Movie movie, IMovieService service) =>
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
