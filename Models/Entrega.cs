using System;
using System.Collections.Generic;

namespace apisafeguardpro.Models;

public partial class Entrega
{
    public int EntregaCod { get; set; }

    public int ColaboradorCod { get; set; }

    public DateOnly DataValidade { get; set; }

    public DateOnly DataEntrega { get; set; }

    public int EpiCod { get; set; }

    public virtual Colaborador ColaboradorCodNavigation { get; set; } = null!;

    public virtual Epi EpiCodNavigation { get; set; } = null!;
}
