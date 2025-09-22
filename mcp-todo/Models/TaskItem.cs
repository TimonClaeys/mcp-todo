using System;

namespace mcp_todo.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Description { get; set; } = "";
    public bool Done { get; set; }
    public DateTime? DueDate { get; set; }
}
