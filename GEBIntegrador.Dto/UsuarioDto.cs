using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Dto
{
    public class UsuarioDto
    {
        public int n_id { get; set; }

        [Required(ErrorMessage = "Los nombres son requeridos")]
        [DisplayName("Nombres")]
        public string v_nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son requeridos")]
        [DisplayName("Apellidos")]
        public string v_apellidos { get; set; }

        [Required(ErrorMessage = "El Correo es requerido")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Correo")]
        [EmailAddress(ErrorMessage = "El correo suministrado no cumple con el formato")]
        public string v_correo { get; set; }

        [Required(ErrorMessage = "El tipo de perfil es requerido")]
        [DisplayName("Perfil")]
        public int n_id_perfil { get; set; }

        public string? Perfil { get; set; }

        [DisplayName("Estado")]
        public int? n_estado { get; set; }

        [DisplayName("Usuario")]
        public string? v_usuario { get; set; }
        public int n_is_delete { get; set; }

        public string ObtenerEstado
        {
            get { return n_estado == 1 ? "Activo" : "Inactivo"; }
        }

    }
}
