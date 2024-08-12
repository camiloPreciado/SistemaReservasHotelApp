using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using GEBIntegrador.Infraestructura;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GEBIntegrador.Dominio.Mensajes;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class SesionService : ISesionService
    {
        private readonly IUsuarioService _usuarioServicio;
        private readonly IMenuService _menuServicio;
        private readonly DBContext _dbContext;

        public SesionService(IUsuarioService usuarioServicio, DBContext dbContext, IMenuService menuServicio)
        {
            _usuarioServicio = usuarioServicio;
            _dbContext = dbContext;
            _menuServicio = menuServicio;
        }

        public async Task<List<Claim>> IniciarSesion(LoginDto modelo)
        {
            try
            {
                usuario usuario_encontrado = await _usuarioServicio.GetUsuario(modelo.v_correo, Utilidades.EncriptarClave(modelo.v_clave));

                if (usuario_encontrado == null)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgAutenticacionNoValida);
                }

                if (usuario_encontrado.n_estado != Enumeradores.UsuarioActivo)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioInactivo);
                }

                //if (usuario_encontrado.n_is_delete == 1)
                if (usuario_encontrado.n_is_delete == Enumeradores.UsuarioEliminado)
                {
                    throw new TaskCanceledException(MensajesSistema.MsgUsuarioInactivo);
                }

                //Menus
                var menuLista2 = await _menuServicio.ListarMenu(usuario_encontrado.n_id_perfil);

                var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles,
                        WriteIndented = true,
                    };
                //Menus

                    List<Claim> claims = new List<Claim>()
                {
                    new Claim("Id", usuario_encontrado.n_id.ToString()),
                    new Claim(ClaimTypes.Name, usuario_encontrado.n_id_personaNavigation.v_nombres  + " " +usuario_encontrado.n_id_personaNavigation.v_apellidos ),
                    new Claim("Correo", usuario_encontrado.v_correo),
                    new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.n_id.ToString()),
                    //new Claim("Perfil", usuario_encontrado.Perfil.ToString()),
                    new Claim(ClaimTypes.Role, usuario_encontrado.n_id_perfil.ToString()),
                    new Claim("Menu", JsonSerializer.Serialize(menuLista2,options)),
                    new Claim("TipoPerfil", usuario_encontrado.n_id_perfilNavigation.v_descripcion),
                };
                    return claims;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public SesionDto ObtenerSesion(HttpContext httpContext)
        {
            //usuario usuario = new usuario();
            SesionDto usuario = new SesionDto();

            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
                return usuario;

            var claims = httpContext.User.Claims;

            var nombresClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (nombresClaim != null)
                usuario.v_nombre = nombresClaim.Value;

            var correoClaim = claims.FirstOrDefault(x => x.Type == "Correo");
            if (correoClaim != null)
                usuario.v_correo = correoClaim.Value;

            var idClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int id))
                usuario.n_id = id;

            var idPerfilClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (idPerfilClaim != null && int.TryParse(idPerfilClaim.Value, out int idPerfil))
                usuario.n_id_perfil = idPerfil;

            return usuario;
        }

    }
}
