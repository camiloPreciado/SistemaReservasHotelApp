using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using GEBIntegrador.Persistencia;
using GEBIntegrador.Dto;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class MenuService : IMenuService
    {
        private readonly DBContext _dbContext;
        private readonly IGenericRepositorio<menu_perfil> _menuRepositorio;

        public MenuService(DBContext dBContext, IGenericRepositorio<menu_perfil> menuRepositorio)
        {
            _dbContext = dBContext;            
            _menuRepositorio = menuRepositorio;
        }
        public async Task<List<menu>> ListarMenu(int? IdPerfil)
        {
            List<menu> listaMenus = new List<menu>();
            try
            {

                var queryPerfil = await _menuRepositorio.Consultar(x => x.n_id_perfil == IdPerfil &&
                                              x.n_id_menuNavigation.n_estado == 1);

                listaMenus = queryPerfil.Where(x => x.n_estado == 1)
                                        .Select(m => m.n_id_menuNavigation).ToList().Where(x => x.n_id_menu_padre == null).ToList();


                //list =  _dbContext.menu_perfils.Include(m => m.IdMenuNavigation.Inversen_id_menu_padreNavigation)
                //                       .Where(x => x.IdPerfil == IdPerfil &&
                //                              x.IdMenuNavigation.IdMenuPadre == null &&
                //                              x.n_id_menuNavigation.n_estado == 1)
                //                       .Select(m => m.IdMenuNavigation).ToList();

                //var options = new JsonSerializerOptions
                //{
                //    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                //    WriteIndented = true,
                //};
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaMenus;
        }
    }
}
