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
    public class ProducersController : Controller
    {

        private readonly IRepositoryWrapper _repository;

        public ProducersController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var producers = await _repository.Producer.GetProducersAsync();
            return View(producers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producer = await _repository.Producer.GetProducerByIdAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }


        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer, [FromForm(Name = "ProfilePicture")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(producer);
            }
            await _repository.Producer.CreateProducer(producer, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _repository.Producer.GetProducerByIdAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer, [FromForm(Name = "ProfilePicture")] IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return View(producer);
            }
            await _repository.Producer.UpdateProducer(id, producer, file);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            Producer producer = await _repository.Producer.GetProducerByIdAsync(id);
            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Producer producer = await _repository.Producer.GetProducerByIdAsync(id);
            if (producer == null) return View("NotFound");
            _repository.Producer.DeleteProducer(id);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
