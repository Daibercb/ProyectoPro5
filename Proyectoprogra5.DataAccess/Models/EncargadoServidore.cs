using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyectoprogra5.DataAccess.Models;

public partial class EncargadoServidore
{
    public string Usuario { get; set; } = null!;

    public int CodigoServidor { get; set; }

    public bool? Alerta { get; set; }


    [JsonIgnore]
    public virtual Servidor CodigoServidorNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Usuario UsuarioNavigation { get; set; } = null!;
}
