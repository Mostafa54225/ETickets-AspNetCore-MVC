using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data.ViewModels;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

namespace ETickets.Repository.Interfaces
{
    public interface IMovieRepository: IRepositoryBase<Movie>
    {
        IEnumerable<Movie> GetMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);
        Task CreateMovie(MovieVM movie, IFormFile file);
        void UpdateMovie(int movieId);
        void DeleteMovie(int movieId);

        Task<MovieDropdownsVM> GetMovieDropdownValues();

        Task UpdateMovieAsync(MovieVM movie, IFormFile file);
    }
}
