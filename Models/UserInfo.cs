using System.ComponentModel.DataAnnotations;

namespace apisafeguardpro.Models;

public class UserInfo
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? Cpf { get; set; }
}
