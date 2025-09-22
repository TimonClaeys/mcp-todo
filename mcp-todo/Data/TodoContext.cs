using mcp_todo.Models;
using Microsoft.EntityFrameworkCore;

namespace mcp_todo.Data;

public class TodoContext : DbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=tasks.db");
}
