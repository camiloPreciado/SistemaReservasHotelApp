using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GEBIntegrador.Dto;

public partial class PersonaDto
{
    public int n_id { get; set; }

    [Required(ErrorMessage = "El documento es requerido")]
    [DisplayName("N° documento")]
    public string v_documento { get; set; } = "";

    [Required(ErrorMessage = "Los nombres son requeridos")]
    [DisplayName("Nombres")]
    public string v_nombres { get; set; } = "";

    [Required(ErrorMessage = "Los apellidos son requeridos")]
    [DisplayName("Apellidos")]
    public string? v_apellidos { get; set; }

}
