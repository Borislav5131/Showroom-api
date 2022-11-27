using Microsoft.EntityFrameworkCore;
using Showroom.Infrastructure.Data.Entities;
using Showroom.Infrastructure.InitialSeed;

namespace Showroom.Infrastructure.Data
{
    public class ShowroomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Entities.Showroom> Showrooms { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Garage> Garages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=Showroom;User Id=borislav;Password=bmw645;")
                .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InitialDataConfiguration<Garage>(@"../Showroom.Infrastructure/InitialSeed/garages.json"));
            modelBuilder.ApplyConfiguration(new InitialDataConfiguration<User>(@"../Showroom.Infrastructure/InitialSeed/users.json"));
        }
    }
}
