using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.Models;

namespace FNZ.Data.Repository.Interfaces
{
    public interface IAnimalRepository
    {
        Task<bool> InsertAsync(Animal animal);
        Task<bool> SaveAsync();
        bool Save();
        Animal Get(Func<Animal, bool> function);
        Task<bool> Remove(Animal animal);
        List<Animal> GetAll(Func<Animal, bool> function);
    }
}
