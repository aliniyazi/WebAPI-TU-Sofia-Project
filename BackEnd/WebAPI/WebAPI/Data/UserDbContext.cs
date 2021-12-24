using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;

namespace WebAPI.Data
{
    public class UserDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=GameWebAPI;Integrated Security=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => 
            {
                entity.HasKey(u => u.UserId);

                entity
                .Property(u => u.UserName)
                .IsRequired(true)
                .HasMaxLength(50);

                entity
                .Property(u => u.Age)
                .IsRequired(false);

                entity
                .Property(u => u.IsDeleted)
                .IsRequired(false)
                .HasDefaultValue(false);

                entity
                .Property(u => u.Password)
                .IsRequired(true);

                entity
                .Property(u => u.Email)
                .IsRequired(true);
            });
            modelBuilder.Entity<Game>(entity => 
            {
                entity.HasKey(g => g.GameId);

                entity
                .Property(g => g.Name)
                .IsRequired(true)
                .HasMaxLength(50);

                entity
                .Property(g => g.IsDeleted)
                .IsRequired(false)
                .HasDefaultValue(false);

                entity
                .Property(g => g.DeleteTime)
                .IsRequired(false);

                entity
                .Property(g => g.CreatedTime)
                .IsRequired(false);
            });
            modelBuilder.Entity<UserGame>(entity => 
            {
                entity.HasKey(ug => new { ug.GameId, ug.UserId });

                entity.HasOne(ug => ug.User).WithMany(u => u.UserGames).HasForeignKey(ug => ug.UserId);
                entity.HasOne(ug => ug.Game).WithMany(g => g.UserGames).HasForeignKey(ug => ug.GameId);

                entity.Property(ug => ug.Score).IsRequired(true).HasDefaultValue(0.0);
            });
        }
    }
}
