using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace apisafeguardpro.Models;

public partial class Epi
{
    public int EpiCod { get; set; }

    public int ColaboradorCod { get; set; }

    public string Nome { get; set; } = null!;

    public string FormaAdequada { get; set; } = null!;
[JsonIgnore]
    public virtual Colaborador? ColaboradorCodNavigation { get; set; }

    public virtual ICollection<Entrega> Entregas { get; } = new List<Entrega>();
}
