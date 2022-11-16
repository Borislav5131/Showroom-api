using Microsoft.EntityFrameworkCore;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Infrastructure.Data
{
    public class ShowroomDbContext : DbContext
    {
        public DbSet<Entities.Showroom> Showrooms { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=Showroom;User Id=borislav;Password=bmw645;")
                .UseLazyLoadingProxies();
        }
    }
}
