using APIcrud.Models;
using Microsoft.EntityFrameworkCore;

namespace APIcrud.Data
{
    public class DbContextData : DbContext
    {
        public DbContextData(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
