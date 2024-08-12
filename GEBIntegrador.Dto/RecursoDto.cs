using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GEBIntegrador.Dto;

public partial class RecursoDto
{
    public int n_id { get; set; }

    [Required(ErrorMessage = "Se requiere elegir el tipo de recurso")]
    [DisplayName("Tipo de recurso")]
    public int? n_tipo_recurso { get; set; }


    public string? v_tipo_recurso { get; set; }

    [Required(ErrorMessage = "Se requiere elegir el nivel del recurso")]
    [DisplayName("Nivel del recurso")]
    public int? n_nivel_recurso { get; set; }


    public string? v_nivel_recurso { get; set; }

    public int? n_capacidad { get; set; }

    [Required(ErrorMessage = "Se requiere el nombre del recurso")]
    [DisplayName("Recurso")]
    public string? v_nombre { get; set; }

    public int? n_categoria_parqueadero { get; set; }

    public DateTime? d_estado_actual_desde { get; set; }

    public DateTime? d_estado_actual_hasta { get; set; }

    [DisplayName("Estado del recurso")]
    public int? n_estado { get; set; }

    public string ObtenerEstado
    {
        get { return n_estado == 1 ? "Activo" : "Inactivo"; }
    }

}
