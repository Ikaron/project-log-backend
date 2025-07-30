using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ProjectLog.Api.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectLogDbContext>(options =>
    options.UseCosmos(
        Environment.GetEnvironmentVariable("COSMOS__ACCOUNTENDPOINT")!,
        Environment.GetEnvironmentVariable("COSMOS__ACCOUNTKEY")!,
        Environment.GetEnvironmentVariable("COSMOS__DATABASENAME")!
    ));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();