using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using ETickets.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema, [FromForm(Name = "Logo")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _repository.Cinema.CreateCinema(cinema, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            Cinema cinema = await _repository.Cinema.GetCinemaByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Cinema cinema = await _repository.Cinema.GetCinemaByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema, [FromForm(Name = "Logo")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _repository.Cinema.UpdateCinema(id, cinema, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Cinema cinema = await _repository.Cinema.GetCinemaByIdAsync(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Cinema cinema = await _repository.Cinema.GetCinemaByIdAsync(id);
            if (cinema == null) return View("NotFound");
            _repository.Cinema.DeleteCinema(id);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
