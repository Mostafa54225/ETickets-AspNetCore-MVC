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

        public async Task CreateActor(Actor actor, IFormFile file)
        {
            if(file != null)
                actor.ProfilePicture = Utils.Helper.ConvertToBytes(file);
            await Create(actor);
        }
        public async Task UpdateActor(int actorId, Actor newActor, IFormFile file)
        {
            Actor actor = await FindByCondition(actor => actor.Id.Equals(actorId)).FirstOrDefaultAsync();
            if (file != null)
                newActor.ProfilePicture = Utils.Helper.ConvertToBytes(file);
            else if (actor.ProfilePicture != null)
                newActor.ProfilePicture = actor.ProfilePicture;
             Update(newActor);
        }
        public void DeleteActor(int actorId)
        {
            Actor actor = FindByCondition(actor => actor.Id.Equals(actorId)).FirstOrDefault();
            Delete(actor);
        }
    }
}
