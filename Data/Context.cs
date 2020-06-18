using Microsoft.EntityFrameworkCore;

namespace MastersOfCinema.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<MastersOfCinema.Models.Director> Director { get; set; }
        public DbSet<MastersOfCinema.Models.Movie> Movie { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MastersOfCinama");
        }

        
    }
}
