using Microsoft.EntityFrameworkCore;
using NET1041_ChuaDeThiMau.Context;
using NET1041_ChuaDeThiMau.Models;

namespace NET1041_ChuaDeThiMau.Services
{
    public class PetService : IPetService
    {
        private readonly MyDbContext _context;

        public PetService(MyDbContext context)
        {
            _context = context;
        }

        public void Add(Pet pet)
        {
            pet.Id = Guid.NewGuid();
            _context.Add(pet);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var pet = _context.Pets.Find(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }

            _context.SaveChanges();
        }

        public void Edit(Pet pet)
        {
            _context.Update(pet);
            _context.SaveChanges();
        }

        public List<Pet> GetAll()
        {
            return _context.Pets.ToList();
        }

        public Pet GetById(Guid id)
        {
            return _context.Pets
                .FirstOrDefault(m => m.Id == id);
        }

        public bool PetExist(Guid id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}
