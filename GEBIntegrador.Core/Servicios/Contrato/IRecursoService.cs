using GEBIntegrador.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IRecursoService
    {
        Task<List<recurso>> ListarParqueaderos(int? IdCategoria, int? IdNivelRecurso);
        Task<List<recurso>> ListarRecursos(int? TipoRecurso, int? IdNivelRecurso);
        Task<List<recurso>> ListarRecursos();
        Task<List<tipos_recurso>> ListarTiposRecursos();
        Task<recurso> CrearRecurso(recurso modelo);
        Task<recurso> EditarRecurso(recurso modelo);
        Task<bool> CambiarEstado(int n_id);
        Task<recurso> BuscarRecursoPorId(int? n_id);

    }
}
