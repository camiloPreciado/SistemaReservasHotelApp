using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class ParametrosService : IParametrosService
    {
        private readonly IGenericRepositorio<parametro> _parametroRepositorio;

        public ParametrosService(IGenericRepositorio<parametro> parametroRepositorio)
        {
            _parametroRepositorio = parametroRepositorio;
        }

        public async Task<parametro> ObtenerParametro(int id)
        {
            parametro Parametro = new parametro();
            try
            {
                Parametro = await _parametroRepositorio.Obtener(x => x.n_estado == 1 && x.n_id == id); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Parametro;
        }

        public async Task<List<parametro>> ListarParametros()
        {
            List<parametro> list = new List<parametro>();
            try
            {
                var queryParametros = await _parametroRepositorio
                                            .Consultar();

                list = queryParametros.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public async Task<parametro> ObtenerPorId(int n_id)
        {
            parametro parametro_encontrado = new parametro();
            try
            {
                parametro_encontrado = await _parametroRepositorio.Obtener(p => p.n_id == n_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return parametro_encontrado;
        }


        public async Task<parametro> CrearParametro(parametro modelo)
        {
            try
            {
                modelo.d_fecha_actualiza = DateTime.Now;

                parametro parametroNuevo;

                parametroNuevo = await _parametroRepositorio.Crear(modelo);

                if (parametroNuevo.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el parametro");
                }

                return parametroNuevo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<parametro> EditarParametro(parametro modelo)
        {
            try
            {
                modelo.d_fecha_actualiza = DateTime.Now;

                parametro parametroNuevo;

                parametroNuevo = await _parametroRepositorio.Editar(modelo);

                if (parametroNuevo.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo editar el parametro");
                }

                return parametroNuevo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
