using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyectoprogra5.DataAccess.Models;

public partial class EncargadoServicio
{
    public string Usuario { get; set; } = null!;

    public int CodigoServicio { get; set; }

    public bool? Alerta { get; set; }

    [JsonIgnore]
    public virtual Servicio CodigoServicioNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
