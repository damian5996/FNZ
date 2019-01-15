using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Data.Data;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.Models;

namespace FNZ.Data.Repository
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AdoptionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAsync(Application application)
        {
            await _dbContext.Applications.AddAsync(application);
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
    }
}
