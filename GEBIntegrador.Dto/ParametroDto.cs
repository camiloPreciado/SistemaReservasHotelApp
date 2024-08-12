using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GEBIntegrador.Dto;

public class ParametroDto
{
    [DisplayName("Id")]
    public int n_id { get; set; }

    [Required(ErrorMessage = "La descripción del parametro es requerida")]
    [DisplayName("Descripción del parametro")]
    public string? v_descripcion_parametro { get; set; }

    [Required(ErrorMessage = "El valor del parametro es requerido")]
    [DisplayName("Valor del parametro")]
    public string? v_valor { get; set; }

    public int? n_estado { get; set; }

    public DateTime d_fecha_actualiza { get; set; }
}
