using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WorldFavor.Contracts.Dtos;
using WorldFavor.Contracts.Entities;
using WorldFavor.Persistence.Configurations;

namespace WorldFavor.Persistence.DbContext
{
    public class WorldFavorDbContext :Microsoft.EntityFrameworkCore.DbContext
    {
        public WorldFavorDbContext(DbContextOptions<WorldFavorDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReaderEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ReaderEntity> Readers { get; set; }
        public DbSet<BookEntity> Books { get; set; }
    }
}
