using System;
using System.Collections.Generic;

namespace ama_zon;

public partial class Plaza
{
    public int PlazaId { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Acuerdo> Acuerdos { get; set; } = new List<Acuerdo>();
}
