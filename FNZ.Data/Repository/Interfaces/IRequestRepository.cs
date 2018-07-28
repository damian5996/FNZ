using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.Models;

namespace FNZ.Data.Repository.Interfaces
{
    public interface IRequestRepository
    {
        Task<bool> InsertAsync(Request request);
        Task<bool> SaveAsync();
        bool Save();
    }
}
