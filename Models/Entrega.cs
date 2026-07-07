namespace apisafeguardpro.Models;

public class Entrega
{
    public int EntregaCod { get; set; }

    public int ColaboradorCod { get; set; }

    public int EpiCod { get; set; }

    public DateOnly DataEntrega { get; set; }

    public DateOnly DataValidade { get; set; }

    public Colaborador? ColaboradorCodNavigation { get; set; }

    public Epi? EpiCodNavigation { get; set; }
}
