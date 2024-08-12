using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GEBIntegrador.Web.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        private readonly IPersonaService _personaServicio;

        public PersonaController(IPersonaService personaServicio)
        {
            _personaServicio = personaServicio;
        }

        [HttpGet]
        public async Task<JsonResult> getPersona(string cc)
        {
            PersonaDto persona = new PersonaDto();
            try
            {

                if (cc == "")
                    return Json(new //RespuestaDto
                    {
                        success = false,
                        data = persona
                    });

                persona = await _personaServicio.Obtener(cc);

                return Json(new //RespuestaDto
                {
                    success = true,
                    data = persona
                });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }

    }
}
