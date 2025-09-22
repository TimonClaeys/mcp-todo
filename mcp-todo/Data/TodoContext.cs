using mcp_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace mcp_todo.Data;

public class TodoContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(AppContext.BaseDirectory, "tasks.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
