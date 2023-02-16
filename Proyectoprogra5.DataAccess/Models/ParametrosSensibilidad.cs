using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ParametrosSensibilidad
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ParametrosServidore> ParametrosServidores { get; } = new List<ParametrosServidore>();
}
