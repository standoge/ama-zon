using System;
using System.Collections.Generic;

namespace ama_zon;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Correo { get; set; } = null!;

    public int PaisId { get; set; }

    public int AcuerdoId { get; set; }

    public virtual Acuerdo Acuerdo { get; set; } = null!;

    public virtual Pai Pais { get; set; } = null!;
}
