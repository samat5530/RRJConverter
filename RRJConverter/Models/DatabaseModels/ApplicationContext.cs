using Microsoft.EntityFrameworkCore;

namespace RRJConverter.Models.DatabaseModels
{
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// Представляет набор объектов, которые хранятся в базе данных
        /// </summary>
        public DbSet<ConvertingOperation> ConvertingOperations { get; set; }

        /// <summary>
        /// Конструктор класса контекста базы данных
        /// </summary>
        /// <param name="options">Параметр, через который в конструктор контекста данных передаются настройки контекста.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }  
    }
}
