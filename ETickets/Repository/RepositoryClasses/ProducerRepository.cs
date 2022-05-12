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
    public class ProducerRepository: RepositoryBase<Producer>, IProducerRepository
    {
        public ProducerRepository(AppDBContext appDBContext): base(appDBContext)
        {
        }
        public async Task<IEnumerable<Producer>> GetProducersAsync()
        {
            return await FindAll().OrderBy(producer => producer.FullName).ToListAsync();
        }
        public async Task<Producer> GetProducerByIdAsync(int producerId)
        {
            return await FindByCondition(producer => producer.Id.Equals(producerId)).FirstOrDefaultAsync();
        }
        public void CreateProducer(Producer producer)
        {
            Create(producer);
        }
        public void UpdateProducer(int producerId)
        {
            Producer producer = FindByCondition(producer => producer.Id.Equals(producerId)).FirstOrDefault();
            Update(producer);
        }
        public void DeleteProducer(int producerId)
        {
            Producer producer = FindByCondition(producer => producer.Id.Equals(producerId)).FirstOrDefault();
            Update(producer);
        }
    }
}
