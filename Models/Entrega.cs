using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace apisafeguardpro.Models;

public partial class Entrega
{
    public int EntregaCod { get; set; }

    public int ColaboradorCod { get; set; }

    public DateOnly DataValidade { get; set; }

    public DateOnly DataEntrega { get; set; }

    public int EpiCod { get; set; }
[JsonIgnore]
    public virtual Colaborador? ColaboradorCodNavigation { get; set; }
[JsonIgnore]
    public virtual Epi? EpiCodNavigation { get; set; }
}
