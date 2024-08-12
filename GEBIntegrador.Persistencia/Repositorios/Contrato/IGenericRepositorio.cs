using GEBIntegrador.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Persistencia.Repositorios.Contrato
{
    public interface IGenericRepositorio<TModel> where TModel : class
    {
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        Task<TModel> Crear(TModel modelo);
        Task<TModel> Editar(TModel modelo);
        Task<bool> Eliminar(TModel modelo);
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel,bool>> filtro = null);
    }
}
