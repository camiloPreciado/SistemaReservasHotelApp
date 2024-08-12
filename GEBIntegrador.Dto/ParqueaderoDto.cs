using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dto;

public partial class ParqueaderoDto
{
    public int n_id { get; set; }

    //public int? n_tipo_recurso { get; set; }

    //public int? n_nivel_recurso { get; set; }

    public string? v_nombre { get; set; }

    public DateTime? d_estado_actual_desde { get; set; }

    public DateTime? d_estado_actual_hasta { get; set; }

    public int? n_estado { get; set; }

}
