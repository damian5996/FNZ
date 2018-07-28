using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Data.Data;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.Models;

namespace FNZ.Data.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAsync(Request request)
        {
            await _dbContext.Requests.AddAsync(request);
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
