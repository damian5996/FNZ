using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.Models;

namespace FNZ.Data.Repository.Interfaces
{
    public interface IAdoptionRepository
    {
        Task<bool> InsertAsync(Application application);
        Task<bool> SaveAsync();
        bool Save();
    }
}
