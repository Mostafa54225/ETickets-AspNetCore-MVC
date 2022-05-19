using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

namespace ETickets.Repository.Interfaces
{
    public interface IActorRepository: IRepositoryBase<Actor>
    {
        Task<IEnumerable<Actor>> GetActorsAsync();
        Task<Actor> GetActorByIdAsync(int actorId);
        Task CreateActor(Actor actor, IFormFile file);
        Task UpdateActor(int actorId, Actor actor, IFormFile file);
        void DeleteActor(int actorId);

    }
}
