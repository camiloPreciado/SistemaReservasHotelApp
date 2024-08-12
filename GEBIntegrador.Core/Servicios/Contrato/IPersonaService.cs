using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IPersonaService
    {
        Task<PersonaDto> Obtener(string? cc);
    }
}
