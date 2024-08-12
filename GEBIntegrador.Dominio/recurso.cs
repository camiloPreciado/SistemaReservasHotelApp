using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class recurso
{
    public int n_id { get; set; }

    public int? n_tipo_recurso { get; set; }

    public int? n_nivel_recurso { get; set; }

    public int? n_capacidad { get; set; }

    public string? v_nombre { get; set; }

    public int? n_categoria_parqueadero { get; set; }

    public DateTime? d_estado_actual_desde { get; set; }

    public DateTime? d_estado_actual_hasta { get; set; }

    public string? v_observaciones { get; set; }

    public int? n_estado { get; set; }

   // public virtual ICollection<agenda> agenda { get; set; } = new List<agenda>();

    public virtual categorias_parqueadero? n_categoria_parqueaderoNavigation { get; set; }

    public virtual niveles? n_nivel_recursoNavigation { get; set; }

    public virtual tipos_recurso? n_tipo_recursoNavigation { get; set; }

    public virtual ICollection<reserva> reservas { get; set; } = new List<reserva>();
}
