using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class reserva
{
    public int n_id { get; set; }

    public int? n_id_recurso { get; set; }

    public int? n_id_usuario { get; set; }

    public DateTime? d_fecha_reserva { get; set; }

    public DateTime? d_fecha_hora_inicio { get; set; }

    public DateTime? d_fecha_hora_fin { get; set; }

    public int? n_tipo_reserva { get; set; }

    public string? v_placa_vehiculo { get; set; }

    public int? n_id_usuario_cancela { get; set; }

    public DateTime? d_fecha_cancela { get; set; }

    public string? v_observaciones { get; set; }

    public int? n_estado { get; set; }

    public virtual recurso? n_id_recursoNavigation { get; set; }

    public virtual usuario? n_id_usuarioNavigation { get; set; }

    public virtual tipos_reserva? n_tipo_reservaNavigation { get; set; }
}
