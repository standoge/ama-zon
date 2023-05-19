using System;
using System.Collections.Generic;

namespace ama_zon;

public partial class Pai
{
    public int PaisId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
