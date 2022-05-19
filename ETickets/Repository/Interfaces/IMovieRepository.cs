using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
namespace ETickets.Repository.Interfaces
{
    public interface IMovieRepository: IRepositoryBase<Movie>
    {
        IEnumerable<Movie> GetMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task CreateMovie(Movie movie);
        void UpdateMovie(int movieId);
        void DeleteMovie(int movieId);
    }
}
