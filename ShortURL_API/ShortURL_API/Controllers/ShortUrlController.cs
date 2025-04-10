using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortURL_API.Domain.Entities;
using ShortURL_API.Extensions;
using ShortURL_API.Infra;
using ShortURL_API.Models.Request;

namespace ShortURL_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortUrlController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public ShortUrlController(IConfiguration configuration, AppDbContext dbContext)
    {
        _configuration = configuration;
        _context = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortUrl([FromBody] ShortUrlRequest request)
    {
        var (isValid, errors) = request.Validate();
        if (!isValid)
        {
            return BadRequest(new { Errors = errors });
        }

        var newGuid = Guid.NewGuid();
        var shortCode = newGuid.EncodeGuidToBase62();

        while(await _context.ShortUrls.AnyAsync(x => x.ShortCode == shortCode))
        {
            newGuid = Guid.NewGuid();
            shortCode = newGuid.EncodeGuidToBase62();
        }

        var shortUrl = new ShortUrl
        {
            ShortUrlGuid = newGuid,
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode
        };

        await _context.ShortUrls.AddAsync(shortUrl);
        await _context.SaveChangesAsync();

        var baseUrl = _configuration["AppSettings:BaseUrl"];
        var shortUrlFull = $"{baseUrl}/{shortCode}";

        return Ok(new { ShortUrl = shortUrlFull });
    }  
}
