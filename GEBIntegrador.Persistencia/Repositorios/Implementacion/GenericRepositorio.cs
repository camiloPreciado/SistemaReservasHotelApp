using GEBIntegrador.Dominio;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Persistencia.Repositorios.Implementacion
{
    public class GenericRepositorio<TModelo> : IGenericRepositorio<TModelo> where TModelo : class
    {

        private readonly DBContext _dbcontext;

        public GenericRepositorio(DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<TModelo> Obtener(Expression<Func<TModelo, bool>> filtro)
        {
            try
            {
                TModelo modelo = await _dbcontext.Set<TModelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception ex)
            {
                throw  new Exception(ex.Message) ;
            }
        }
        public async Task<TModelo> Crear(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Add(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<TModelo> Editar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> Eliminar(TModelo modelo)
        {
            try
            {
                _dbcontext.Set<TModelo>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModelo> queryModelo = filtro == null ? _dbcontext.Set<TModelo>() : _dbcontext.Set<TModelo>().Where(filtro);

                return queryModelo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
