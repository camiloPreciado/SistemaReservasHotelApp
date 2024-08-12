using System;
using System.Collections.Generic;

namespace GEBIntegrador.Dominio;

public partial class persona
{
    public int n_id { get; set; }

    public string v_documento { get; set; } = null!;

    public string v_nombres { get; set; } = null!;

    public string? v_apellidos { get; set; }

//    public virtual ICollection<solicitud_asistente> solicitud_asistentes { get; set; } = new List<solicitud_asistente>();

    public virtual ICollection<usuario> usuarios { get; set; } = new List<usuario>();
}
