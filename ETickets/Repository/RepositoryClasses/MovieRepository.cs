using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using ETickets.Data;
using ETickets.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repository.RepositoryClasses
{
    public class MovieRepository: RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(AppDBContext appDBContext): base(appDBContext)
        {
        }
        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await FindAll().OrderBy(movie => movie.Name).ToListAsync();
        }
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId)).FirstOrDefaultAsync();
        }
        public void CreateMovie(Movie movie)
        {
            Create(movie);
        }
        public void UpdateMovie(int movieId)
        {
            Movie movie = FindByCondition(movie => movie.Id.Equals(movieId)).FirstOrDefault();
            Update(movie);
        }
        public void DeleteMovie(int movieId)
        {
            Movie movie = FindByCondition(movie => movie.Id.Equals(movieId)).FirstOrDefault();
            Delete(movie);
        }
    }
}
