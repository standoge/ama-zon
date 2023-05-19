using System;
using System.Collections.Generic;

namespace ama_zon;

public partial class Acuerdo
{
    public int AcuerdoId { get; set; }

    public DateTime FechaEmision { get; set; }

    public DateTime FechaRenuncia { get; set; }

    public decimal CantidadSancion { get; set; }

    public int PlazaId { get; set; }

    public int ProyectoId { get; set; }

    public int DepartamentoId { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual Plaza Plaza { get; set; } = null!;

    public virtual Proyecto Proyecto { get; set; } = null!;
}
