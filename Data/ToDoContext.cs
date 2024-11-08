using Microsoft.EntityFrameworkCore;
using Todo.Web.Models.Entities;

namespace Todo.Web.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoElement> ToDoElements { get; set; }
    }
}
