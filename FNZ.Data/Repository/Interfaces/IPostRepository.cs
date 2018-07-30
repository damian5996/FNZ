using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.Models;

namespace FNZ.Data.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<bool> InsertAsync(Post post);
        Task<bool> SaveAsync();
        bool Save();
        Post Get(Func<Post, bool> function);
    }
}
