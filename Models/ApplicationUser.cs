using Microsoft.AspNetCore.Identity;

namespace apisafeguardpro.Models;
public class ApplicationUser : IdentityUser{
    public decimal Cpf { get; set; }
}