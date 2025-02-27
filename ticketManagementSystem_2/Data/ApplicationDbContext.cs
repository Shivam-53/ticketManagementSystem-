using Microsoft.EntityFrameworkCore;
using ticketManagementSystem_2.Models;

namespace ticketManagementSystem_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }  
    }
}
