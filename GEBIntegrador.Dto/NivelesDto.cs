using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GEBIntegrador.Dto;

public  class NivelesDto
{
    [DisplayName("Id")]
    public int n_id { get; set; }

    [Required(ErrorMessage = "Se requiere un nombre para el nivel")]
    [DisplayName("Nombre")]
    public string? v_descripcion { get; set; }

    [Required(ErrorMessage = "Se requiere un numero de nivel")]
    [DisplayName("Numero de nivel")]
    public int? n_nivel { get; set; }

    [DisplayName("Estado del nivel")]
    public int? n_estado { get; set; }

    public string ObtenerEstado
    {
        get { return n_estado == 1 ? "Activo" : "Inactivo"; }
    }
}
