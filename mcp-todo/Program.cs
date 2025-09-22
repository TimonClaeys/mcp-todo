using mcp_todo.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddDbContext<TodoContext>();

builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});


var app = builder.Build();

var todoContext = app.Services.GetService<TodoContext>();

if (todoContext == null)
{
    Console.Error.WriteLine("Failed to initialize TodoContext");
}

todoContext?.Database.EnsureCreated();

await app.RunAsync();