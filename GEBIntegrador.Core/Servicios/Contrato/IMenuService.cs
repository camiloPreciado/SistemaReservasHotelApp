using GEBIntegrador.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IMenuService
    {
        Task<List<menu>> ListarMenu(int? IdPerfil);
    }
}
