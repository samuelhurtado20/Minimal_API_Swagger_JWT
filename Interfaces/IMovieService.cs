using Minimal_API_Swagger_JWT.Models;

namespace Minimal_API_Swagger_JWT.Interfaces
{
    public interface IMovieService
    {
        public Movie Create(Movie movie);
        public Movie Get(int id);
        public List<Movie> List();
        public Movie Update(Movie movie);
        public bool Delete(int id);
    }
}
