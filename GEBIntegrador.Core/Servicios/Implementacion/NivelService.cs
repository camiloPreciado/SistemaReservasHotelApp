using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Dto;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class NivelService : INivelService
    {
        private readonly IGenericRepositorio<niveles> _recursoNivel;

        private readonly IMapper _mapper;

        public NivelService(IGenericRepositorio<niveles> recursoNivel, 
                            IMapper mapper)
        {
            _recursoNivel = recursoNivel;
            _mapper = mapper;
        }

        public async Task<List<niveles>> ListarNiveles(int? TipoRecurso)
        {
            List<niveles> listaNiveles = new List<niveles>();
            try
            {

                var queryNiveles = await _recursoNivel.Consultar();

                if (TipoRecurso == 1)
                {
                    listaNiveles = queryNiveles.Where(x => x.n_nivel < 0 &&
                    x.n_estado == 1).ToList();
                }
                else {
                    listaNiveles = queryNiveles.Where(x => x.n_nivel > 0 &&
                   x.n_estado == 1).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaNiveles;
        }

        public async Task<List<NivelesDto>> CargarNiveles()
        {
            List<niveles> listaNiveles = new List<niveles>();
            try
            {

                var queryNiveles = await _recursoNivel.Consultar();

                listaNiveles = queryNiveles.ToList();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _mapper.Map<List<NivelesDto>>(listaNiveles.ToList()); ;
        }

        public async Task<niveles> CrearNivel(niveles modelo)
        {
            try
            {
                niveles nuevoNivel;

                nuevoNivel = await _recursoNivel.Crear(modelo);

                if (nuevoNivel.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el nivel");
                }

                return nuevoNivel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<niveles> EditarNivel(niveles modelo)
        {
            try
            {
                niveles nivel;

                nivel = await _recursoNivel.Editar(modelo);

                if (nivel.n_id == 0)
                {
                    throw new TaskCanceledException("No se pudo editar el nivel");
                }

                return nivel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<niveles> CargarNivel(int id)
        {
            niveles nivel = new niveles();
            try
            {
                var queryRegional = await _recursoNivel.Consultar(r => r.n_id == id);

                nivel = queryRegional.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return list;
            return nivel;
        }

        public async Task<bool> CambiarEstado(int n_id)
        {
            bool respuesta = true;
            try
            {
                niveles nivelEncontrado = await CargarNivel(n_id);
                if (nivelEncontrado != null)
                {
                    nivelEncontrado.n_estado = nivelEncontrado.n_estado == 1 ? 0 : 1;

                    nivelEncontrado = await EditarNivel(nivelEncontrado);
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
