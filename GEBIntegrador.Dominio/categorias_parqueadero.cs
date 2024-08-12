using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class categorias_parqueadero
{
    public int n_id { get; set; }

    public string? v_descripcion { get; set; }

    public int? n_estado { get; set; }

    public virtual ICollection<recurso> recursos { get; set; } = new List<recurso>();
}
