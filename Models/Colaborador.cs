using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace apisafeguardpro.Models;

public partial class Colaborador
{
    public decimal Cpf { get; set; }

    public string Telefone { get; set; }

    public DateOnly DataAdmissao { get; set; }

    public string Email { get; set; }

    public int ColaboradorCod { get; set; }

    public string NomeColab { get; set; } = null!;

    public int Ctps { get; set; }
[JsonIgnore]
    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
[JsonIgnore]
    public virtual ICollection<Epi> Epis { get; } = new List<Epi>();
}
