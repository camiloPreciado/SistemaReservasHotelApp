using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Persistencia.Repositorios.Contrato
{
    public interface IUsuarioRepositorio : IGenericRepositorio<usuario>
    {
        Task<usuario> CrearUsuario(UsuarioCrearDto modelo);
        //Task<UsuarioCrearDto> EditarUsuario(UsuarioCrearDto modelo);
        //Task<UsuarioCrearDto> CrearUsuario(usuario modelo);
        Task<usuario> EditarUsuario(UsuarioCrearDto modelo);
        Task<usuario> Activar(usuario modelo);
    }
}
