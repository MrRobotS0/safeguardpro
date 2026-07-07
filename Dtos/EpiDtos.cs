using System.ComponentModel.DataAnnotations;

namespace apisafeguardpro.Dtos;

public class EpiRequest
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string FormaAdequada { get; set; } = null!;
}

public class EpiResponse
{
    public int EpiCod { get; set; }
    public string Nome { get; set; } = null!;
    public string FormaAdequada { get; set; } = null!;
}
