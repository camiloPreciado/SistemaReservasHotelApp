using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class perfile
{
    public int n_id { get; set; }

    public string v_nombre { get; set; } = null!;

    public string? v_descripcion { get; set; }

    /// <summary>
    /// 0-InActivo 1-Activo
    /// </summary>
    public int n_estado { get; set; }

    public virtual ICollection<menu_perfil> menu_perfils { get; set; } = new List<menu_perfil>();

    public virtual ICollection<permisos_perfil> permisos_perfils { get; set; } = new List<permisos_perfil>();

    public virtual ICollection<usuario> usuarios { get; set; } = new List<usuario>();
}
