using AutoMapper;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Dto;
using GEBIntegrador.Infraestructura;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Persistencia.Repositorios.Implementacion
{
    public class UsuarioRepositorio : GenericRepositorio<usuario>, IUsuarioRepositorio
    {
        private readonly DBContext _dbContext;
        private readonly IMapper _mapper;

        public UsuarioRepositorio(DBContext dbContext, IMapper mapper) :base(dbContext) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<usuario> CrearUsuario(UsuarioCrearDto modelo)
        {
            persona PersonaCreada = new persona();
            usuario usuarioEncontrado = new usuario();

            //Se genera por transacciones para cuidarse de algún error generado
            using (var transation = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //Validar que el correo no esté registrado
                    usuarioEncontrado = await _dbContext.usuarios.Where(c => c.v_correo.ToUpper() == modelo.v_correo.ToUpper()).FirstOrDefaultAsync();
                    if (usuarioEncontrado != null)
                        throw new TaskCanceledException(MensajesSistema.MsgCorreoYaRegistrado);

                    if (!(modelo.persona != null))
                        throw new TaskCanceledException(MensajesSistema.MsgDatosdePersonasonRequeridos);

                    usuarioEncontrado = _mapper.Map<usuario>(modelo);

                    if (!(modelo.persona.n_id > 0)) //Crear persona
                    {
                        usuarioEncontrado.n_id_personaNavigation = _mapper.Map<persona>( modelo.persona);
                    }

                    //Se crea V2 el usuario
                    var Usuario =  await _dbContext.usuarios.AddAsync(usuarioEncontrado);
                    await _dbContext.SaveChangesAsync();
                    usuarioEncontrado = Usuario.Entity;

                    transation.Commit();
                }
                catch (Exception ex)
                {
                    transation.Rollback();
                    throw new TaskCanceledException(ex.Message);
                }
            }
            //return modelo;
            return usuarioEncontrado;
        }
        public async Task<usuario> EditarUsuario(UsuarioCrearDto modelo)
        {
            //Se genera por transacciones para cuidarse de algún error generado
            usuario usuarioEncontrado = new usuario();
            using (var transation = _dbContext.Database.BeginTransaction()){
                try
                {
                    usuarioEncontrado = await _dbContext.usuarios.Where(c => c.v_correo.ToUpper() == modelo.v_correo.ToUpper() && c.n_id != modelo.n_id).FirstOrDefaultAsync();
                    if (usuarioEncontrado != null)
                        throw new TaskCanceledException(MensajesSistema.MsgCorreoYaRegistrado);

                    usuarioEncontrado = await _dbContext.usuarios.Where(c => c.n_id == modelo.n_id)
                                                .Include(p => p.n_id_personaNavigation)
                                                .FirstOrDefaultAsync();

                    if (usuarioEncontrado == null)
                        throw new TaskCanceledException(MensajesSistema.MsgUsuarioNoExiste);

                    //Se Edita el usuario
                    _mapper.Map(modelo, usuarioEncontrado);
                    _mapper.Map(modelo.persona, usuarioEncontrado.n_id_personaNavigation);
                    _dbContext.SaveChanges();

                    transation.Commit();
                }
                catch (Exception ex)
                {
                    transation.Rollback();
                    throw new TaskCanceledException(ex.Message);
                }
            }
            //return modelo;
            return usuarioEncontrado;
        }
        public async Task<usuario> Activar(usuario modelo)
        {
            try
            {
                usuario usuarioEncontrado = await Obtener(u => u.n_id == modelo.n_id);

                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioNoExiste);
                }

                usuarioEncontrado.v_correo = modelo.v_correo;
                usuarioEncontrado.n_id_perfil = modelo.n_id_perfil;
                usuarioEncontrado.n_estado = modelo.n_estado;

                //bool respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);
                usuario respuesta = await Editar(usuarioEncontrado);

                if (respuesta == null)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioNosePudoEditar);
                }

                return respuesta;

            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new TaskCanceledException(MensajesSistema.MsgOcurrioUnProblema + "; " + ex.Message);
            }
        }

    }
}
