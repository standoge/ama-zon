using System;
using System.Collections.Generic;

namespace ama_zon;

public partial class Proyecto
{
    public int ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Acuerdo> Acuerdos { get; set; } = new List<Acuerdo>();
}