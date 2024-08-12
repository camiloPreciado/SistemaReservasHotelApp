using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class permisos_perfil
{
    public int n_id { get; set; }

    public int? n_id_perfil { get; set; }

    public int? n_id_permiso { get; set; }

    public int? n_estado { get; set; }

    public virtual perfile? n_id_perfilNavigation { get; set; }

    public virtual permiso? n_id_permisoNavigation { get; set; }
}
