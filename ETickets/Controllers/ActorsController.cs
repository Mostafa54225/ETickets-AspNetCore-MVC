using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data;
using Microsoft.EntityFrameworkCore;
using ETickets.Repository.Interfaces;
namespace ETickets.Controllers
{
    public class ActorsController : Controller
    {

        private IRepositoryWrapper _repository;
        public ActorsController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _repository.Actor.GetActorsAsync();
            return View(data);
        }
    }
}
