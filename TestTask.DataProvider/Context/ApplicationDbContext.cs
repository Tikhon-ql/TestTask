using Microsoft.EntityFrameworkCore;
using TestTask.DataProvider.Models;

namespace TestTask.DataProvider.Context
{
    /// <summary>
    /// Context to communicate with database
    /// </summary>
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}
