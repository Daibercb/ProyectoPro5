using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class ParametrosServidore
{
    public int ParametroSensibilidad { get; set; }

    public int Componente { get; set; }

    public int IdServidor { get; set; }

    public string Porcentaje { get; set; } = null!;

    public virtual Componente ComponenteNavigation { get; set; } = null!;

    public virtual Servidor IdServidorNavigation { get; set; } = null!;

    public virtual ParametrosSensibilidad ParametroSensibilidadNavigation { get; set; } = null!;
}
