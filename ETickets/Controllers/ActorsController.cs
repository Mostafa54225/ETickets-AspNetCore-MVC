using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETickets.Data;
using Microsoft.EntityFrameworkCore;
using ETickets.Repository.Interfaces;
using ETickets.Models;
using Microsoft.AspNetCore.Http;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create([Bind("FullName,Bio")]Actor actor, [FromForm(Name = "ProfilePicture")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }

            await _repository.Actor.CreateActor(actor, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _repository.Actor.GetActorByIdAsync(id);
            if (actorDetails == null) return View("Empty");
            return View(actorDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _repository.Actor.GetActorByIdAsync(id);
            if(actor == null) return View("NotFound");
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor, [FromForm(Name = "ProfilePicture")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await _repository.Actor.UpdateActor(id, actor, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Actor actor = await _repository.Actor.GetActorByIdAsync(id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Actor actor = await _repository.Actor.GetActorByIdAsync(id);
            if (actor == null) return View("NotFound");
            _repository.Actor.DeleteActor(id);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
