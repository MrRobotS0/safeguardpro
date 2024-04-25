using System;
using System.Collections.Generic;

namespace apisafeguardpro.Models;

public partial class Colaborador
{
    public decimal Cpf { get; set; }

    public char Telefone { get; set; }

    public DateOnly DataAdmissao { get; set; }

    public char Email { get; set; }

    public int ColaboradorCod { get; set; }

    public string NomeColab { get; set; } = null!;

    public int Ctps { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();

    public virtual ICollection<Epi> Epis { get; } = new List<Epi>();
}
