using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class Componente
{
    public int CodigoComponente { get; set; }

    public string CuerpoCorreo { get; set; } = null!;

    public virtual ICollection<ParametrosServidore> ParametrosServidores { get; } = new List<ParametrosServidore>();
}
