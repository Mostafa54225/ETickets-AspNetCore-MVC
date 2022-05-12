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
    public class CinemaRepository: RepositoryBase<Cinema>, ICinemaRepository
    {
        public CinemaRepository(AppDBContext appDBContext): base(appDBContext)
        {
        }
        public async Task<IEnumerable<Cinema>> GetCinemasAsync()
        {
            return await FindAll().OrderBy(cinema => cinema.Name).ToListAsync();
        }
        public async Task<Cinema> GetCinemaByIdAsync(int cinemaId)
        {
            return await FindByCondition(cinema => cinema.Id.Equals(cinemaId)).FirstOrDefaultAsync();
        }
        public void CreateCinema(Cinema cinema)
        {
            Create(cinema);
        }
        public void UpdateCinema(int cinemaId)
        {
            Cinema cinema = FindByCondition(cinema => cinema.Id.Equals(cinemaId)).FirstOrDefault();
            Update(cinema);
        }
        public void DeleteCinema(int cinemaId)
        {
            Cinema cinema = FindByCondition(cinema => cinema.Id.Equals(cinemaId)).FirstOrDefault();
            Delete(cinema);
        }
    }
}
