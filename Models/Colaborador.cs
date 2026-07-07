namespace apisafeguardpro.Models;

public class Colaborador
{
    public int ColaboradorCod { get; set; }

    public string NomeColab { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateOnly DataAdmissao { get; set; }

    public string Email { get; set; } = null!;

    public string Ctps { get; set; } = null!;

    public ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
