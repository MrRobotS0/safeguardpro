namespace apisafeguardpro.Models;

public class Epi
{
    public int EpiCod { get; set; }

    public string Nome { get; set; } = null!;

    public string FormaAdequada { get; set; } = null!;

    public ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
