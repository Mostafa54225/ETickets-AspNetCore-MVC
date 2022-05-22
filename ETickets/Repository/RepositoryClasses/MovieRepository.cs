using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using ETickets.Data;
using ETickets.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using ETickets.Data.ViewModels;
using Microsoft.AspNetCore.Http;

namespace ETickets.Repository.RepositoryClasses
{
    public class MovieRepository: RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(AppDBContext appDBContext): base(appDBContext)
        {
        }
        public IEnumerable<Movie> GetMoviesAsync()
        {
            return FindAll().OrderBy(movie => movie.Name).ToList();
        }
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId))
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies)
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync();
            
        }
        
        public async Task CreateMovie(Movie movie)
        {
            await Create(movie);
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

        public async Task<MovieDropdownsVM> GetMovieDropdownValues()
        {
            var response = new MovieDropdownsVM()
            {
                Actors = await AppDBContext.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await AppDBContext.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await AppDBContext.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task CreateMovie(MovieVM movie, IFormFile file)
        {
            if(file != null)
            {
                var newMovie = new Movie()
                {
                    Name = movie.Name,
                    Description = movie.Description,
                    Price = movie.Price,
                    Image = Utils.Helper.ConvertToBytes(file),
                    CinemaId = movie.CinemaId,
                    StartDate = movie.StartDate,
                    EndDate = movie.EndDate,
                    ProducerId = movie.ProducerId
                };
                await AppDBContext.Movies.AddAsync(newMovie);
                await AppDBContext.SaveChangesAsync();
                //Add Movie Actors
                foreach (var actorId in movie.ActorsIds)
                {
                    var newActorMovie = new Actor_Movie()
                    {
                        MovieId = newMovie.Id,
                        ActorId = actorId
                    };
                    await AppDBContext.Actor_Movies.AddAsync(newActorMovie);
                }
                await AppDBContext.SaveChangesAsync();
            }
            
            
        }

        public async Task UpdateMovieAsync(MovieVM movie, IFormFile file)
        {
            var dbMovie = await AppDBContext.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if(dbMovie != null)
            {
                if (file != null)
                    dbMovie.Image = Utils.Helper.ConvertToBytes(file);
                else if (dbMovie.Image != null)
                    dbMovie.Image = dbMovie.Image;
                
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.CinemaId = movie.CinemaId;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                dbMovie.ProducerId = movie.ProducerId;
                await AppDBContext.SaveChangesAsync();
                
            }

            // Remove Existing Actors
            var existingActors = AppDBContext.Actor_Movies.Where(n => n.MovieId == movie.Id).ToList();
            AppDBContext.Actor_Movies.RemoveRange(existingActors);
            await AppDBContext.SaveChangesAsync();
            
            //Add Movie Actors
            foreach (var actorId in movie.ActorsIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await AppDBContext.Actor_Movies.AddAsync(newActorMovie);
            }
            await AppDBContext.SaveChangesAsync();
        }
        
    }
}
