using Microsoft.EntityFrameworkCore;

namespace RRJConverter.Models.DatabaseModels
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ConvertingOperation> ConvertingOperations { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
  
    }
}
