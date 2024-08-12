using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface INivelService
    {
        Task<List<niveles>> ListarNiveles(int? TipoRecurso);

        Task<List<NivelesDto>> CargarNiveles();

        Task<niveles> CrearNivel(niveles modelo);
        Task<niveles> EditarNivel(niveles modelo);
        Task<niveles> CargarNivel(int id);
        Task<bool> CambiarEstado(int n_id);
    }
}
