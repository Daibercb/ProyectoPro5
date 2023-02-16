using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class Servicio
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int CodigoServidor { get; set; }

    public int Timeout { get; set; }

    public virtual ICollection<DashboardServicio> DashboardServicios { get; } = new List<DashboardServicio>();

    public virtual ICollection<EncargadoServicio> EncargadoServicios { get; } = new List<EncargadoServicio>();

    public virtual ICollection<ParametrosServicio> ParametrosServicios { get; } = new List<ParametrosServicio>();
}
