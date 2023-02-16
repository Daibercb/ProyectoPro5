using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ParametrosServicio
{
    public int IdParametro { get; set; }

    public int IdServicios { get; set; }

    public double RangoNormal { get; set; }

    public virtual Parametro IdParametroNavigation { get; set; } = null!;

    public virtual Servicio IdServiciosNavigation { get; set; } = null!;
}
