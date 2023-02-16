using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class Usuario
{
    public string Usuario1 { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public virtual ICollection<EncargadoServicio> EncargadoServicios { get; } = new List<EncargadoServicio>();

    public virtual ICollection<EncargadoServidore> EncargadoServidores { get; } = new List<EncargadoServidore>();
}
