using System.ComponentModel.DataAnnotations;

namespace ShortURL_API.Models.Request;

public class ShortUrlRequest
{
    [Required]
    public string OriginalUrl { get; set; } = string.Empty;

    public (bool, List<string>) Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(OriginalUrl))
        {
            errors.Add("URL is required.");
        }
        else if (!Uri.IsWellFormedUriString(OriginalUrl, UriKind.Absolute))
        {
            errors.Add("URL is not valid.");
        }        

        return (errors.Count == 0, errors);
    }
}
