using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GEBIntegrador.Dto;

public partial class RegionalDto
{
    public int n_id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [DisplayName("Nombre de la regional")]
    public string v_descripcion { get; set; } = null!;

    [DisplayName("Encargado SST")]
    public int? n_id_usuario_sst { get; set; }

    public string? nombre_encargado { get; set; }

    public int? n_id_usuario_crea { get; set; }

    public DateTime d_fecha_crea { get; set; }

    public int? n_id_usuario_actualiza { get; set; }

    public DateTime? d_fecha_actualiza { get; set; }

    public int n_estado { get; set; }

}
