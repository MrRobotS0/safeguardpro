using System.ComponentModel.DataAnnotations;

namespace apisafeguardpro.Dtos;

public class EntregaRequest
{
    [Range(1, int.MaxValue)]
    public int ColaboradorCod { get; set; }

    [Range(1, int.MaxValue)]
    public int EpiCod { get; set; }

    public DateOnly DataEntrega { get; set; }

    public DateOnly DataValidade { get; set; }
}

public class EntregaResponse
{
    public int EntregaCod { get; set; }
    public int ColaboradorCod { get; set; }
    public int EpiCod { get; set; }
    public DateOnly DataEntrega { get; set; }
    public DateOnly DataValidade { get; set; }
}
