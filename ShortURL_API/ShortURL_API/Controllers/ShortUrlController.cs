using Microsoft.AspNetCore.Mvc;
using ShortURL_API.Extensions;
using ShortURL_API.Models.Request;

namespace ShortURL_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShortUrlController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ShortUrlController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateShortUrl([FromBody] ShortUrlRequest request)
    {
        var (isValid, errors) = request.Validate();
        if (!isValid)
        {
            return BadRequest(new { Errors = errors });
        }
       
        var shortCode = Guid.NewGuid().EncodeGuidToBase62();
        var baseUrl = _configuration["AppSettings:BaseUrl"];
        var shortUrlFull = $"{baseUrl}/{shortCode}";

        return Ok(new { ShortUrl = shortUrlFull });
    }
}
