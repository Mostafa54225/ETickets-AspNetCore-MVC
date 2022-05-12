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
    public class ActorRepository: RepositoryBase<Actor>, IActorRepository
    {
        public ActorRepository(AppDBContext appDBContext): base(appDBContext)
        {
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            return await FindAll().OrderBy(a => a.FullName).ToListAsync();
        }
        public async Task<Actor> GetActorByIdAsync(int actorId)
        {
            return await FindByCondition(actor => actor.Id.Equals(actorId)).FirstOrDefaultAsync();
        }

        public void CreateActor(Actor actor)
        {
            Create(actor);
        }
        public void UpdateActor(int actorId)
        {
            Actor actor = FindByCondition(actor => actor.Id.Equals(actorId)).FirstOrDefault();
            Update(actor);
        }
        public void DeleteActor(int actorId)
        {
            Actor actor = FindByCondition(actor => actor.Id.Equals(actorId)).FirstOrDefault();
            Delete(actor);
        }
    }
}
