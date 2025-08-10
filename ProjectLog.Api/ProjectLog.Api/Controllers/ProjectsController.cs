using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLog.Api.Data;
using ProjectLog.Api.Models;

namespace ProjectLog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(ProjectLogDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> Get() =>
        await db.Projects.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> Get(string id)
    {
        var project = await db.Projects.FindAsync(id);
        return project is null ? NotFound() : project;
    }

    [HttpPost]
    public async Task<ActionResult<Project>> Post(Project project)
    {
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Project updated)
    {
#if NoSQL
        var parsedId = id;
#else
        if (!int.TryParse(id, out var parsedId)) return BadRequest();
#endif

        if (parsedId != updated.Id) return BadRequest();

        var exists = await db.Projects.CountAsync(p => p.Id == parsedId) > 0;
        if (!exists) return NotFound();

        db.Entry(updated).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
#if NoSQL
        var parsedId = id;
#else
        if (!int.TryParse(id, out var parsedId)) return BadRequest();
#endif

        var project = await db.Projects.FindAsync(parsedId);
        if (project is null) return NotFound();

        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return NoContent();
    }
}