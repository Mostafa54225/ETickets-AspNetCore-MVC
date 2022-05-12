using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
namespace ETickets.Repository.Interfaces
{
    public interface ICinemaRepository: IRepositoryBase<Cinema>
    {
        Task<IEnumerable<Cinema>> GetCinemasAsync();
        Task<Cinema> GetCinemaByIdAsync(int cinemaI);
        void CreateCinema(Cinema cinema);
        void UpdateCinema(int cinemaId);
        void DeleteCinema(int cinemaId);
    }
}
