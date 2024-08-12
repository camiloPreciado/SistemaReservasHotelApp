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
    public class PersonaService : IPersonaService
    {
        private readonly IGenericRepositorio<persona> _personaRepositorio;
        private readonly IMapper _mapper;

        public PersonaService(IGenericRepositorio<persona> asistenteRepositorio, IMapper mapper)
        {
            _personaRepositorio = asistenteRepositorio;
            _mapper = mapper;
        }

        public async Task<PersonaDto> Obtener(string? cc)
        {
            persona personaEncontrada = new persona();
            try
            {
                personaEncontrada = await _personaRepositorio.Obtener(a => a.v_documento == cc);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return _mapper.Map<PersonaDto>(personaEncontrada);
        }   
    }
}
