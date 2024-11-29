using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET1041_ChuaDeThiMau.Context;
using NET1041_ChuaDeThiMau.Models;
using NET1041_ChuaDeThiMau.Services;
using Newtonsoft.Json;

namespace NET1041_ChuaDeThiMau.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: Pets
        public IActionResult Index()
        {
            return View(_petService.GetAll());
        }

        // GET: Pets/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Breed,Price")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _petService.Add(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Name,Breed,Price")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // add thong tin cu vao session
                    var petTruocKhiSua = _petService.GetById(id);
                    var petTruocKhiSuaJson = JsonConvert.SerializeObject(petTruocKhiSua);
                    HttpContext.Session.SetString(petTruocKhiSua.Id.ToString(), petTruocKhiSuaJson);

                    _petService.Edit(pet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public IActionResult Undo(Guid id)
        {
            var petTruocKhiSuaJson = HttpContext.Session.GetString(id.ToString());
            
            if (petTruocKhiSuaJson != null)
            {
                var petTruocKhiSua = JsonConvert.DeserializeObject<Pet>(petTruocKhiSuaJson);
                _petService.Edit(petTruocKhiSua);
                HttpContext.Session.Remove(id.ToString());
                TempData["Message"] = "Undo successful.";
            }
            else
            {
                TempData["Message"] = "Cannot undo.";
            }
            
            return RedirectToAction("Index");
        }

        // GET: Pets/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _petService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(Guid id)
        {
            return _petService.PetExist(id);
        }
    }
}
