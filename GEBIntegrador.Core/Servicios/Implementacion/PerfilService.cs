using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class PerfilService : IPerfilService
    {
        private readonly IGenericRepositorio<perfile> _perfilRepositorio;
        private readonly IMapper _mapper;

        public PerfilService(IGenericRepositorio<perfile> perfilRepositorio, IMapper mapper)
        {
            _perfilRepositorio = perfilRepositorio;
            _mapper = mapper;
        }
        public async Task<List<PerfilDto>> Listar()
        {
            try
            {
                var queryPerfil = await _perfilRepositorio.Consultar(p => p.n_estado == 1);

                var listaPerfiles = queryPerfil.ToList();

                return _mapper.Map<List<PerfilDto>>(listaPerfiles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<List<PerfilDto>> ListarActivos()
        {
            try
            {
                var queryPerfil = await _perfilRepositorio.Consultar(p => p.n_estado == 1);

                var listaPerfiles = queryPerfil.ToList();

                return _mapper.Map<List<PerfilDto>>(listaPerfiles);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
