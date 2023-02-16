using System;
using System.Collections.Generic;

namespace Proyectoprogra5.DataAccess.Models;

public partial class DashboardServicio
{
    public int IdDashboard { get; set; }

    public int CodigoServicio { get; set; }

    public DateTime? FechaMonitoreo { get; set; }

    public double Timeout { get; set; }

    public string Disponibilidad { get; set; } = null!;

    public string? InformacionParametros { get; set; }

    public virtual Servicio CodigoServicioNavigation { get; set; } = null!;
}
