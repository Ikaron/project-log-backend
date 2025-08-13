using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectLog.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var cosmosEnabledStr = Environment.GetEnvironmentVariable("COSMOS__ENABLED");

Action<DbContextOptionsBuilder> buildOptions;
if (!string.IsNullOrEmpty(cosmosEnabledStr) && bool.TryParse(cosmosEnabledStr, out var cosmosEnabled) && cosmosEnabled)
{
    buildOptions = options =>
        options.UseCosmos(
            Environment.GetEnvironmentVariable("COSMOS__ENDPOINT")!,
            Environment.GetEnvironmentVariable("COSMOS__ACCOUNTKEY")!,
            Environment.GetEnvironmentVariable("COSMOS__CONTAINER")!
        );
}
else
{
    // SQL Server as a fallback in development or for small-scale deployment
    string? connectionString = builder.Configuration.GetConnectionString("DefaultSqlConnection");

    buildOptions = options => options.UseSqlServer(connectionString);
}

var disableHttpsStr = Environment.GetEnvironmentVariable("DISABLE_HTTPS_REDIRECTION");

var disableHttps = !string.IsNullOrEmpty(disableHttpsStr) && bool.TryParse(disableHttpsStr, out var disableHttpsRedirection) && disableHttpsRedirection;

builder.Services.AddDbContext<ProjectLogDbContext>(buildOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
if (disableHttps) // This fixes Azure incorrectly linking to HTTP instead of HTTPS as it is behind a proxy
{
    builder.Services.AddOpenApi(options => options.AddDocumentTransformer((doc, ctx, cancellationToken) =>
    {
        foreach (var server in doc.Servers)
            server.Url = server.Url.Replace("http://", "https://");

        return Task.CompletedTask;
    }));
}
else
    builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

if (disableHttps)
    app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();