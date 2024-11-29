using NET1041_ChuaDeThiMau.Models;

namespace NET1041_ChuaDeThiMau.Services
{
    public interface IPetService
    {
        List<Pet> GetAll();
        Pet GetById(Guid id);

        void Add(Pet pet);
        void Edit(Pet pet);
        void Delete(Guid id);

        bool PetExist(Guid id);
    }
}
