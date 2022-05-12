using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
namespace ETickets.Repository.Interfaces
{
    public interface IProducerRepository: IRepositoryBase<Producer>
    {
        Task<IEnumerable<Producer>> GetProducersAsync();
        Task<Producer> GetProducerByIdAsync(int producerId);
        void CreateProducer(Producer producer);
        void UpdateProducer(int producerId);
        void DeleteProducer(int proucerId);
    }
}
