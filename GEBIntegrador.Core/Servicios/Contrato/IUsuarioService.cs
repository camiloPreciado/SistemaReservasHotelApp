using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Contrato
{
    public interface IUsuarioService
    {
        //Task<List<Usuario>> Lista();
        Task<List<UsuarioDto>> Lista();
        Task<usuario> GetUsuario(int? id);
        Task<usuario> GetUsuario(string correo, string? clave);
        Task<usuario> GetUsuario(string correo);
        Task<List<usuario>> GetUsuarioByPerfil(int? perfil);
        //Task<usuario> SaveUsuario(usuario modelo);
        Task<usuario> SaveUsuario(UsuarioCrearDto modelo);
        Task<usuario> Registrarse(LoginDto modelo);
        Task<List<usuario>> GetResponsables();
        Task<bool> Eliminar(int id);

    }
}
