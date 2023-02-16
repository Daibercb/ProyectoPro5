using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ParametrosServicio
{
    public int IdParametro { get; set; }

    public int IdServicios { get; set; }

    public double RangoNormal { get; set; }

    [JsonIgnore]
    public virtual Parametro IdParametroNavigation { get; set; } = null!;
    [JsonIgnore]

    public virtual Servicio IdServiciosNavigation { get; set; } = null!;
}
