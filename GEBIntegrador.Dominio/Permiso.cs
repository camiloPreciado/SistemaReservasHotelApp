using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class permiso
{
    public int n_id { get; set; }

    public string? v_nombre { get; set; }

    public string? v_descripcion { get; set; }

    public int? n_estado { get; set; }

    public virtual ICollection<permisos_perfil> permisos_perfils { get; set; } = new List<permisos_perfil>();
}
