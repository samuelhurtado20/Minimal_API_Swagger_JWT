using Minimal_API_Swagger_JWT.Interfaces;
using Minimal_API_Swagger_JWT.Models;
using Minimal_API_Swagger_JWT.Repositories;

namespace Minimal_API_Swagger_JWT.Services
{
    public class MovieService : IMovieService
    {
        public Movie Create(Movie movie)
        {
            movie.Id = MovieRepository.Movies.Count + 1;
            MovieRepository.Movies.Add(movie);
            return movie;
        }

        public bool Delete(int id)
        {
            var movie = MovieRepository.Movies.FirstOrDefault(o => o.Id == id);
            if (movie == null) return false;

            MovieRepository.Movies.Remove(movie);
            return true;
        }

        public Movie? Get(int id)
        {
            var movie = MovieRepository.Movies.FirstOrDefault(o => o.Id == id);
            if (movie == null) return null;
            return movie;
        }

        public List<Movie> List()
        {
            return MovieRepository.Movies;
        }

        public Movie? Update(Movie movie)
        {
            var oldmovie = MovieRepository.Movies.FirstOrDefault(o => o.Id == movie.Id);
            if (oldmovie == null) return null;

            oldmovie.Title = movie.Title;
            oldmovie.Description = movie.Description;
            oldmovie.Rating = movie.Rating;

            return oldmovie;
        }
    }
}
