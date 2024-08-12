using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using GEBIntegrador.Persistencia.Repositorios.Implementacion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class RecursoService : IRecursoService
    {
        private readonly IGenericRepositorio<recurso> _recursoRepositorio;
        private readonly IGenericRepositorio<tipos_recurso> _recursoTiposRepositorio;
    
        public RecursoService(IGenericRepositorio<recurso> recursoRepositorio,
                                IGenericRepositorio<tipos_recurso> recursoTiposRepositorio)
        {
            _recursoRepositorio = recursoRepositorio;  
            _recursoTiposRepositorio = recursoTiposRepositorio;
        }

        public async Task<recurso> BuscarRecursoPorId(int? n_id)
        {
            recurso recursoEncontrado = new recurso();
            try
            {
                var queryRecurso = await _recursoRepositorio.Consultar();

                recursoEncontrado = await queryRecurso
                                            .Include(n => n.n_nivel_recursoNavigation)
                                            .Where(c => c.n_id == n_id).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return recursoEncontrado;
        }

        public async Task<recurso> CrearRecurso(recurso modelo)
        {
            try
            {
                recurso recursoNuevo;

                recursoNuevo = await _recursoRepositorio.Crear(modelo);

                if (recursoNuevo.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el recurso");
                }

                return recursoNuevo;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<recurso> EditarRecurso(recurso modelo)
        {
            try
            {
                recurso recursoNuevo;

                recursoNuevo = await _recursoRepositorio.Editar(modelo);

                if (recursoNuevo.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo editar el recurso");
                }

                return recursoNuevo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<recurso>> ListarParqueaderos(int? IdCategoria, int? IdNivelRecurso)
        {
            List<recurso> listaParqueaderos = new List<recurso>();
            try
            {

                var queryParqueadero = await _recursoRepositorio.Consultar(x => x.n_nivel_recurso == IdNivelRecurso &&
                                                x.n_categoria_parqueadero == IdCategoria &&
                                                x.n_estado == 1); 
                listaParqueaderos = queryParqueadero.ToList();
                           
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaParqueaderos;
        }

        public async Task<List<recurso>> ListarRecursos(int? TipoRecurso, int? IdNivelRecurso)
        {
            List<recurso> listaRecursos = new List<recurso>();
            try
            {

                var queryRecursos = await _recursoRepositorio.Consultar(x => x.n_nivel_recurso == IdNivelRecurso &&
                                                x.n_tipo_recurso == TipoRecurso &&
                                                x.n_estado == 1);
                listaRecursos = queryRecursos.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaRecursos;
        }
        
        
        public async Task<List<recurso>> ListarRecursos()
        {
            List<recurso> listaRecursos = new List<recurso>();
            try
            {

                var queryRecursos = await _recursoRepositorio.Consultar();
                listaRecursos = queryRecursos
                                       .Include(r => r.n_nivel_recursoNavigation)
                                       .Include(r => r.n_tipo_recursoNavigation)
                                       .ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaRecursos;
        }

        public async Task<List<tipos_recurso>> ListarTiposRecursos()
        {
            List<tipos_recurso> listaTiposRecurso = new List<tipos_recurso>();
            try
            {

                var queryTiposRecurso = await _recursoTiposRepositorio.Consultar();

                listaTiposRecurso = queryTiposRecurso.Where(x => x.n_estado == 1).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaTiposRecurso;
        }


        public async Task<bool> CambiarEstado(int n_id)
        {
            bool respuesta = true;
            try
            {
                recurso recursoEncontrado = await BuscarRecursoPorId(n_id);
                if (recursoEncontrado != null)
                {
                    recursoEncontrado.n_estado = recursoEncontrado.n_estado == 1 ? 0 : 1;                   
                    recursoEncontrado = await EditarRecurso(recursoEncontrado);
                }
                else
                {
                    respuesta = false;
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioNoExiste);
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                throw new Exception(ex.Message);
            }
            return respuesta;
        }
    }
}
