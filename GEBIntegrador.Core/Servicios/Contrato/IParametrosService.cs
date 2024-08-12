using GEBIntegrador.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IParametrosService
    {
        Task<List<parametro>> ListarParametros();
        Task<parametro> ObtenerParametro(int id);
        Task<parametro> ObtenerPorId(int n_id);
        Task<parametro> CrearParametro(parametro modelo);
        Task<parametro> EditarParametro(parametro modelo);
    }
}
