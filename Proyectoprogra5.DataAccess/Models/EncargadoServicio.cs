using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class EncargadoServicio
{
    public string Usuario { get; set; } = null!;

    public int CodigoServicio { get; set; }

    public bool? Alerta { get; set; }

    public virtual Servicio CodigoServicioNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
