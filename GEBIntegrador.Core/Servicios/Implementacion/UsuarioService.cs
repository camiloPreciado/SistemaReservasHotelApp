using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Dto;
using GEBIntegrador.Infraestructura;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        //private readonly IGenericRepositorio<usuario> _usuarioRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="usuarioRepositorio"></param>
        public UsuarioService(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDto>> Lista()
        {
            try
            {
                var queryUsuarios = await _usuarioRepositorio.Consultar(u => u.n_is_delete == 0);

                var listaUsuarios = queryUsuarios
                                        .Include(cat => cat.n_id_perfilNavigation)
                                        .Include(per => per.n_id_personaNavigation)
                                        .ToList();

                return _mapper.Map<List<UsuarioDto>>(listaUsuarios.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

        public async Task<usuario> GetUsuario(int? id)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.n_id == id);

                return await queryUsuario
                                    .Include(per => per.n_id_perfilNavigation)
                                    .Include(pe => pe.n_id_personaNavigation)
                                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

        public async Task<usuario> GetUsuario(string correo, string? clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.v_correo == correo && u.v_clave == clave);

                var UsuarioEncontrado = await queryUsuario
                                              .Include(p => p.n_id_perfilNavigation)
                                              .Include(pe => pe.n_id_personaNavigation)
                                              .FirstOrDefaultAsync();
                return UsuarioEncontrado;

            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }

        }

        public async Task<usuario> GetUsuario(string correo)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.v_correo == correo);

                return await queryUsuario.FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

        public async Task<List<usuario>> GetUsuarioByPerfil(int? perfil)
        {
            try
            {
                //var queryUsuario = await _usuarioRepositorio.Consultar(u => u.n_id == id);
                var queryUsuario = await _usuarioRepositorio.Consultar(u => u.n_id_perfil == perfil);

                return await queryUsuario
                                .Include(per => per.n_id_perfilNavigation)
                                .Include(pers => pers.n_id_personaNavigation)
                                .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

        public async Task<usuario> Registrarse(LoginDto modelo)
        {
            try
            {
                if (modelo.v_clave == null)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgClavenoValida);
                }

                //Buscar el usuario
                usuario usuario_encontrado = await GetUsuario(modelo.v_correo);
                if (usuario_encontrado == null)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgRequierePreRegistro);
                }
                else
                {
                    //Pregunta si está pendiente el registro
                    if (usuario_encontrado.v_clave == null)
                    {
                        usuario_encontrado.v_clave = Utilidades.EncriptarClave(modelo.v_clave);
                        //usuario_encontrado = await SaveUsuarioRegistro(usuario_encontrado);
                        usuario_encontrado = await _usuarioRepositorio.Activar(usuario_encontrado); // posiblemente enviar a editar en el repositorio

                        if (usuario_encontrado.n_id > 0)
                            return usuario_encontrado;
                        else
                        {
                            throw new TaskCanceledException(MensajesSistema.MsgNosePuedeCrearUsuario);
                        }
                    }
                    else
                    {
                        throw new TaskCanceledException(MensajesSistema.MsgUsuarioYaseEncuentraRegistrado);
                    }
                }
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

        private async Task<usuario> SaveUsuarioRegistro(usuario modelo)
        {
            try
            {
                //if (modelo.n_id != 0)
                    //return await Crear(modelo);
                    //return await _usuarioRepositorio.CrearUsuario(modelo);
                //else
                    //return await Editar(modelo);
                    return await _usuarioRepositorio.Activar(modelo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<usuario> SaveUsuario(UsuarioCrearDto modelo)
        {
            try
            {
                if (modelo.n_id == 0)
                    //return await Crear(modelo);
                    return await _usuarioRepositorio.CrearUsuario(modelo);
                else
                    //return await Editar(modelo);
                    return await _usuarioRepositorio.EditarUsuario(modelo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<usuario>> GetResponsables()
        {
            //List<Usuario> listaResponsables = await _context.Usuarios.Where(u => u.Estado == 1).ToListAsync();

            var queryResponsables = await _usuarioRepositorio.Consultar(u => u.n_estado == 1);
            return  await queryResponsables.ToListAsync();
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.n_id == id);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioNoExiste);

                //bool respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

                //if (respuesta == null)
                //    throw new TaskCanceledException(MensajesSistema.MsgNosePudoEliminar);

                //return respuesta;

                usuarioEncontrado.n_is_delete = Enumeradores.UsuarioEliminado;
                usuario respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if (respuesta == null)
                    throw new TaskCanceledException(MensajesSistema.MsgNosePudoEliminar);

                return true;

            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(MensajesSistema.MsgOcurrioUnProblema + "; " +ex.Message);
            }

        }

    }
}
