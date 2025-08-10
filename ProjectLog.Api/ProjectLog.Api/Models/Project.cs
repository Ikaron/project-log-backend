using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectLog.Api.Models;

public class Project
{
#if NOSQL
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
#else
    [Key]
    public int Id { get; set; }
#endif

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}