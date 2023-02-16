using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class Parametro
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<ParametrosServicio> ParametrosServicios { get; } = new List<ParametrosServicio>();
}
