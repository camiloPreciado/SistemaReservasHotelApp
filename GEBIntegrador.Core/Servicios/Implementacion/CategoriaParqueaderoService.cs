using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class CategoriaParqueaderoService : ICategoriaParqueaderoService
    {
        private readonly IGenericRepositorio<categorias_parqueadero> _recursoCategoriaParqueadero;

        public CategoriaParqueaderoService(IGenericRepositorio<categorias_parqueadero> recursoCategoriaParqueadero)
        {
            _recursoCategoriaParqueadero = recursoCategoriaParqueadero;
        }

        public async Task<List<categorias_parqueadero>> ListarCategoriasParqueadero()
        {
            List<categorias_parqueadero> listaCategoriasP = new List<categorias_parqueadero>();
            try
            {

                var queryCategoriasP = await _recursoCategoriaParqueadero.Consultar();

                listaCategoriasP = queryCategoriasP.Where(x => x.n_estado == 1).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaCategoriasP;
        }
    }
}
