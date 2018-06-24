using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Share.Models;
using Microsoft.EntityFrameworkCore;

namespace FNZ.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<MoneyCollection> MoneyCollections { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Tab> Tabs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
