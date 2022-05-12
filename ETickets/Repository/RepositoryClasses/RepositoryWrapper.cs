using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data;
using ETickets.Repository.Interfaces;
namespace ETickets.Repository.RepositoryClasses
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private AppDBContext _context;
        private IActorRepository _actor;
        private IMovieRepository _movie;
        private IProducerRepository _producer;
        private ICinemaRepository _cinema;

        public RepositoryWrapper(AppDBContext context)
        {
            _context = context;
        }
        public IActorRepository Actor
        {
            get
            {
                if(_actor == null)
                {
                    _actor = new ActorRepository(_context);
                }
                return _actor;
            }
        }
        public IMovieRepository Movie
        {
            get
            {
                if (_movie == null)
                {
                    _movie = new MovieRepository(_context);
                }
                return _movie;
            }
        }
        public IProducerRepository Producer
        {
            get
            {
                if (_producer == null)
                {
                    _producer = new ProducerRepository(_context);
                }
                return _producer;
            }
        }
        public ICinemaRepository Cinema
        {
            get
            {
                if (_cinema == null)
                {
                    _cinema = new CinemaRepository(_context);
                }
                return _cinema;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
