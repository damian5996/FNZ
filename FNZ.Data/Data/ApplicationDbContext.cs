using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FNZ.Share.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<Application> Applications { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<MoneyCollection> MoneyCollections { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Tab> Tabs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Animal>()
                .HasKey(a => a.Id);
            builder.Entity<Animal>()
                .HasMany(a => a.Applications)
                .WithOne(a => a.Animal);

            builder.Entity<Application>()
                .HasKey(a => a.Id);
            builder.Entity<Application>()
                .HasOne(a => a.Animal);

            builder.Entity<Moderator>()
                .HasKey(a => a.Id);
            builder.Entity<Moderator>()
                .HasMany(a => a.Requests)
                .WithOne(a => a.Moderator);
            builder.Entity<Moderator>()
                .HasMany(a => a.Tabs)
                .WithOne(a => a.Moderator);
            builder.Entity<Moderator>()
                .Ignore(a => a.EmailConfirmed)
                .Ignore(a => a.AccessFailedCount)
                .Ignore(a => a.LockoutEnabled)
                .Ignore(a => a.LockoutEnd)
                .Ignore(a => a.NormalizedEmail)
                .Ignore(a => a.PhoneNumber)
                .Ignore(a => a.PhoneNumberConfirmed)
                .Ignore(a => a.TwoFactorEnabled)
                .Ignore(a => a.NormalizedUserName)
                .ToTable("Moderators");

            builder.Entity<MoneyCollection>()
                .HasKey(a => a.Id);
            builder.Entity<MoneyCollection>()
                .HasOne(a => a.Post);

            builder.Entity<Request>()
                .HasKey(a => a.Id);
            builder.Entity<Request>()
                .HasOne(a => a.Post);
            builder.Entity<Request>()
                .HasOne(a => a.Moderator);

            builder.Entity<Tab>()
                .HasKey(a => a.Id);
            builder.Entity<Tab>()
                .HasOne(a => a.Moderator);

            builder.Entity<Post>()
                .HasOne(a => a.Animal);

            base.OnModelCreating(builder);
        }
    }
}
