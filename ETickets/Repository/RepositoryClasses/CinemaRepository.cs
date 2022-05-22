using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using ETickets.Data;
using ETickets.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

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
        public async Task CreateCinema(Cinema cinema, IFormFile file)
        {
            if (file != null)
                cinema.Logo = Utils.Helper.ConvertToBytes(file);
            await Create(cinema);
        }
        public async Task UpdateCinema(int cinemaId, Cinema newCinema, IFormFile file)
        {
            Cinema cinema = await FindByCondition(cinema => cinema.Id.Equals(cinemaId)).FirstOrDefaultAsync();
            if (file != null)
                newCinema.Logo = Utils.Helper.ConvertToBytes(file);
            else if (cinema.Logo != null)
                newCinema.Logo  = cinema.Logo;
            Update(newCinema);
        }
        public void DeleteCinema(int cinemaId)
        {
            Cinema cinema = FindByCondition(cinema => cinema.Id.Equals(cinemaId)).FirstOrDefault();
            Delete(cinema);
        }
    }
}
