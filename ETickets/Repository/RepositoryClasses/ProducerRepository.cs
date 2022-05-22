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
        public async Task CreateProducer(Producer producer, IFormFile file)
        {
            if (file != null)
                producer.ProfilePicture = Utils.Helper.ConvertToBytes(file);
            await Create (producer);
        }
        public async Task UpdateProducer(int producerId, Producer newProducer, IFormFile file)
        {
            Producer producer = await FindByCondition(producer => producer.Id.Equals(producerId)).FirstOrDefaultAsync();
            if (file != null)
                newProducer.ProfilePicture = Utils.Helper.ConvertToBytes(file);
            else if (producer.ProfilePicture != null)
                newProducer.ProfilePicture = producer.ProfilePicture;
            Update(newProducer);
        }
        public void DeleteProducer(int producerId)
        {
            Producer producer = FindByCondition(producer => producer.Id.Equals(producerId)).FirstOrDefault();
            Delete(producer);
        }
    }
}
