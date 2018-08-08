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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> InsertAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
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

        public Post Get(Func<Post, bool> function)
        {
            return _dbContext.Posts.FirstOrDefault(function);
        }

        public async Task<bool> Remove(Post post)
        {
            _dbContext.Remove(post);
            var result = await SaveAsync();
            return result;
        }

        public List<Post> GetAll(Func<Post, bool> function)
        {
            return _dbContext.Posts.Where(function).ToList();
        }
    }
}
