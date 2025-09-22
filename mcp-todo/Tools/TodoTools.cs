using System.ComponentModel;
using mcp_todo.Data;
using ModelContextProtocol.Server;
using Microsoft.EntityFrameworkCore;

namespace mcp_todo.Tools;

[McpServerToolType]
public class TodoTools
{
    private readonly TodoContext _context;

    public TodoTools(TodoContext context)
    {
        _context = context;
    }

    [McpServerTool, Description("Add A Task to the database")]
    public async Task<string> AddTask(
        [Description("The description of the task")] string description,
        [Description("Indicator wether the task is done")] bool done,
        [Description("The due date of the task (optional)")] DateTime? dueDate = null)
    {
        try
        {
            _context.Tasks.Add(new Models.TaskItem { Description = description, Done = done, DueDate = dueDate });
            await _context.SaveChangesAsync();
            return "The task has been created";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }

    }

    [McpServerTool, Description("Get all tasks from the database")]
    public async Task<string> GetAllTasks()
    {
        try
        {
            var tasks = await _context.Tasks.ToListAsync();
            if (!tasks.Any())
            {
                return "No tasks found in the database.";
            }

            var taskList = tasks.Select(t => $"ID: {t.Id}, Description: {t.Description}, Done: {t.Done}, Due date: {t.DueDate}");
            return string.Join("\n", taskList);
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [McpServerTool, Description("Update the status or due date of a Task")]
    public async Task<string> UpdateTask(
        [Description("The id of the task to update")] int id,
        [Description("The new status of the task")] bool done,
        [Description("The new due date of the task")] DateTime? dueDate)
    {
        try
        {
            var task = await _context.Tasks.Where(task => task.Id == id).FirstOrDefaultAsync();
            if (task == null)
            {
                return $"No task found with id {id}";
            }

            task.Done = done;
            task.DueDate = dueDate;
            await _context.SaveChangesAsync();
            return $"Task with id {id} has been updated successfully";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}
