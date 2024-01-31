using KATCinema.Models;
using Microsoft.EntityFrameworkCore;

namespace KATCinema.Utils.DBConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservedSeat> ReservedSeats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users{ get; set; }
    }                                         
}
