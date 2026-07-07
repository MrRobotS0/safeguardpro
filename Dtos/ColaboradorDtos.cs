using System.ComponentModel.DataAnnotations;

namespace apisafeguardpro.Dtos;

public class ColaboradorRequest
{
    [Required]
    [StringLength(100)]
    public string NomeColab { get; set; } = null!;

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string Cpf { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string Telefone { get; set; } = null!;

    public DateOnly DataAdmissao { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(60)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string Ctps { get; set; } = null!;
}

public class ColaboradorResponse
{
    public int ColaboradorCod { get; set; }
    public string NomeColab { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public DateOnly DataAdmissao { get; set; }
    public string Email { get; set; } = null!;
    public string Ctps { get; set; } = null!;
}
