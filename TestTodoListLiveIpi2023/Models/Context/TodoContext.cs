using Microsoft.EntityFrameworkCore;

namespace TestTodoListLiveIpi2023.Models.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) 
        { 
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
