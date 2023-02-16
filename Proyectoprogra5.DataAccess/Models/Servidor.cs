using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class Servidor
{
    public int Codigo { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Administrador { get; set; } = null!;

    public virtual ICollection<DashboardServidore> DashboardServidores { get; } = new List<DashboardServidore>();

    public virtual ICollection<EncargadoServidore> EncargadoServidores { get; } = new List<EncargadoServidore>();

    public virtual ICollection<ParametrosServidore> ParametrosServidores { get; } = new List<ParametrosServidore>();
}
