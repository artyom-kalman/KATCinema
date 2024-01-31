using Microsoft.EntityFrameworkCore;

namespace KATCinema.Utils.DBConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

    }
}
