using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dto;

public partial class ReservaParqueaderoDto
{
    public int n_id { get; set; }

    public int? n_id_recurso { get; set; }

    public int? n_id_usuario { get; set; }

    public DateTime? d_fecha_reserva { get; set; }

    public DateTime? d_fecha_hora_inicio { get; set; }

    public DateTime? d_fecha_hora_fin { get; set; }

    public int? n_tipo_reserva { get; set; }

    public string? v_placa_vehiculo { get; set; }

    public int? n_estado { get; set; }

}
