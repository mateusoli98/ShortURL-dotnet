using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShortURL_API.Domain.Entities;

[Table("ShortUrls")]
public class ShortUrl
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid ShortUrlGuid { get; set; }

    [Required]
    public string OriginalUrl { get; set; } = string.Empty;    

    [Required]
    [MaxLength(20)]
    public string ShortCode { get; set; } = string.Empty;

    public int AccessCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
   
}
