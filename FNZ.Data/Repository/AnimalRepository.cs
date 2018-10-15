using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FNZ.Data.Data;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.Models;

namespace FNZ.Data.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnimalRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAsync(Animal animal)
        {
            await _dbContext.Animals.AddAsync(animal);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public Animal Get(Func<Animal, bool> function)
        {
            return _dbContext.Animals.FirstOrDefault(function);
        }

        public async Task<bool> Remove(Animal animal)
        {
            _dbContext.Remove(animal);
            var result = await SaveAsync();
            return result;
        }

        public List<Animal> GetAll(Func<Animal, bool> function)
        {
            return _dbContext.Animals.Where(function).ToList();
        }
    }
}
