using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class menu_perfil
{
    public int n_id { get; set; }

    public int? n_id_menu { get; set; }

    public int? n_id_perfil { get; set; }

    public int? n_estado { get; set; }

    public virtual menu? n_id_menuNavigation { get; set; }

    public virtual perfile? n_id_perfilNavigation { get; set; }
}
