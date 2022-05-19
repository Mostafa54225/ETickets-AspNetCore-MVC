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
    public class CinemasController : Controller
    {
        private IRepositoryWrapper _repository;
        public CinemasController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _repository.Cinema.GetCinemasAsync();
            return View(cinemas);
        }
    }
}
