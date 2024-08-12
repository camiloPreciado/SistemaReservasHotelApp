using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Dto
{
    public class RespuestaDto
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public List<string>? messages { get; set; }

    }
}
