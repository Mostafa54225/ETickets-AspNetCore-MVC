using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data;
using Microsoft.EntityFrameworkCore;
using ETickets.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

namespace ETickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        public MoviesController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _repository.Movie.GetAllAsync(c => c.Cinema); 
            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _repository.Movie.GetMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movie, [FromForm(Name = "Image")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownData = await _repository.Movie.GetMovieDropdownValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _repository.Movie.CreateMovie(movie, file);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _repository.Movie.GetMovieByIdAsync(id);
            return View(movie);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movieDetils = await _repository.Movie.GetMovieByIdAsync(id);
            if (movieDetils == null) return View("NotFound");
            var response = new MovieVM()
            {
                Id = movieDetils.Id,
                Name = movieDetils.Name,
                Description = movieDetils.Description,
                Price = movieDetils.Price,
                Image = movieDetils.Image,
                StartDate = movieDetils.StartDate,
                EndDate = movieDetils.EndDate,
                CinemaId = movieDetils.CinemaId,
                ProducerId = movieDetils.ProducerId,
                ActorsIds = movieDetils.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var movieDropdownData = await _repository.Movie.GetMovieDropdownValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieVM movie, [FromForm(Name = "Image")] IFormFile file)
        {
            if (id != movie.Id) return View("NotFound");
            if(!ModelState.IsValid)
            {
                var movieDropdownData = await _repository.Movie.GetMovieDropdownValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _repository.Movie.UpdateMovieAsync(movie, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _repository.Movie.GetAllAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredMovies = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredMovies);
            }

            return View(allMovies);

        }
    }
}
