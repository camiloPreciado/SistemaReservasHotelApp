using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Dto
{
    public class SesionDto
    {
        public int n_id { get; set; }

        public string v_nombre { get; set; }

        public string v_correo { get; set; }

        public int n_id_perfil { get; set; }

        public int? n_estado { get; set; }

        public string? v_usuario { get; set; }

    }
}
