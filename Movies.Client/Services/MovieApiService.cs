using Movies.Client.Models;

namespace Movies.Client.Services
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<Movie> CreateMovie(Movie movie)
        {
            return movie;
        }

        public async Task DeleteMovie(int id)
        {
        }

        public async Task<Movie> GetMovie(int id)
        {
            return new() { Id = id, Genre = "Comics", Title = "asd", Rating = "9.2", ImageUrl = "images/src", ReleaseDate = DateTime.Now, Owner = "bies" };
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return new List<Movie>() { new() { Id=1,Genre="Comics",Title="asd", Rating="9.2", ImageUrl= "images/src", ReleaseDate=DateTime.Now,Owner="bies"} };
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            return movie;
        }
    }
}
