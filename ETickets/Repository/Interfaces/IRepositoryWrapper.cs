using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Repository.Interfaces;
namespace ETickets.Repository.Interfaces
{
    public interface IRepositoryWrapper
    {
        IActorRepository Actor { get; }
        ICinemaRepository Cinema { get; }
        IProducerRepository Producer { get; }
        IMovieRepository Movie { get; }
        Task SaveAsync();
    }
}
