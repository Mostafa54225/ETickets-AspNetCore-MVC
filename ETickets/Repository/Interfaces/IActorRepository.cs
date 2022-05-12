using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Models;
namespace ETickets.Repository.Interfaces
{
    public interface IActorRepository: IRepositoryBase<Actor>
    {
        Task<IEnumerable<Actor>> GetActorsAsync();
        Task<Actor> GetActorByIdAsync(int actorId);
        void CreateActor(Actor actor);
        void UpdateActor(int actorId);
        void DeleteActor(int actorId);

    }
}
