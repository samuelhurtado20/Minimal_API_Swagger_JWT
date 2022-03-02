using Minimal_API_Swagger_JWT.Models;

namespace Minimal_API_Swagger_JWT.Repositories
{
    public class MovieRepository
    {
        public static List<Movie> movie = new()
        {
            new() { Id = 1, Description="desc", Rating = 0.4, Title="Titanic"}
        };
    }
}
