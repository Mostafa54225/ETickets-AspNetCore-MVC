using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

namespace ETickets.Repository.Interfaces
{
    public interface IProducerRepository: IRepositoryBase<Producer>
    {
        Task<IEnumerable<Producer>> GetProducersAsync();
        Task<Producer> GetProducerByIdAsync(int producerId);
        Task CreateProducer(Producer producer, IFormFile file);
        Task UpdateProducer(int producerId, Producer producer, IFormFile file);
        void DeleteProducer(int proucerId);
    }
}
