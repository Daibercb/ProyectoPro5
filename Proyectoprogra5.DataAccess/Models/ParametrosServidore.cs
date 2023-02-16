using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ParametrosServidore
{
    public int ParametroSensibilidad { get; set; }

    public int Componente { get; set; }

    public int IdServidor { get; set; }

    public string Porcentaje { get; set; } = null!;
    [JsonIgnore]
    public virtual Componente ComponenteNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Servidor IdServidorNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ParametrosSensibilidad ParametroSensibilidadNavigation { get; set; } = null!;
}
