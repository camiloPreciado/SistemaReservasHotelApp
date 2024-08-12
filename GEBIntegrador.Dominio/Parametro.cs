using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class parametro
{
    public int n_id { get; set; }

    public string? v_descripcion_parametro { get; set; }

    public string? v_valor { get; set; }

    public int? n_estado { get; set; }

    public DateTime d_fecha_actualiza { get; set; }
}
