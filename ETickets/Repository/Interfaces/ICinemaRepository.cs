using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

namespace ETickets.Repository.Interfaces
{
    public interface ICinemaRepository: IRepositoryBase<Cinema>
    {
        Task<IEnumerable<Cinema>> GetCinemasAsync();
        Task<Cinema> GetCinemaByIdAsync(int cinemaI);
        Task CreateCinema(Cinema cinema, IFormFile file);
        Task UpdateCinema(int cinemaId, Cinema cinema, IFormFile file);
        void DeleteCinema(int cinemaId);
    }
}
