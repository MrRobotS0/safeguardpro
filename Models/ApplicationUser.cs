using Microsoft.AspNetCore.Identity;

namespace apisafeguardpro.Models;

public class ApplicationUser : IdentityUser
{
    public string Cpf { get; set; } = null!;
}
