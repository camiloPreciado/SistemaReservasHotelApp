using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class menu
{
    public int n_id_menu { get; set; }

    public string? v_descripcion { get; set; }

    public int? n_id_menu_padre { get; set; }

    public string? v_url { get; set; }

    public int n_estado { get; set; }

    public virtual ICollection<menu> Inversen_id_menu_padreNavigation { get; set; } = new List<menu>();

    public virtual ICollection<menu_perfil> menu_perfils { get; set; } = new List<menu_perfil>();

    public virtual menu? n_id_menu_padreNavigation { get; set; }
}
