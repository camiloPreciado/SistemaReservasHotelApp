using GEBIntegrador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IPerfilService
    {
        Task<List<PerfilDto>> Listar();
        Task<List<PerfilDto>> ListarActivos();
    }
}
