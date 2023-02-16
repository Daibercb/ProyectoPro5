using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class EncargadoServidore
{
    public string Usuario { get; set; } = null!;

    public int CodigoServidor { get; set; }

    public bool? Alerta { get; set; }

    public virtual Servidor CodigoServidorNavigation { get; set; } = null!;

    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
