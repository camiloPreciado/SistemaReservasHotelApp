using AutoMapper;
using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Dto;
using GEBIntegrador.Infraestructura;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace GEBIntegrador.Web.Controllers
{
    [Authorize]
    public class RecursosController : Controller
    {
        private readonly ICategoriaParqueaderoService _categoriaParqueaderoServicio;

        private readonly INivelService _nivelServicio;

        private readonly IRecursoService _recursoServicio;

        private readonly IReservaService _reservaServicio;

        private readonly ISesionService _sesionServicio;

        private readonly IMapper _mapper;

        public RecursosController(ICategoriaParqueaderoService categoriaParqueaderoServicio,
                                INivelService nivelServicio,
                                IRecursoService recursolServicio,
                                ISesionService sesionServicio,
                                IReservaService reservaServicio,
                                IMapper mapper)
        {
            _categoriaParqueaderoServicio = categoriaParqueaderoServicio;
            _nivelServicio = nivelServicio;
            _recursoServicio = recursolServicio;
            _reservaServicio = reservaServicio;
            _sesionServicio = sesionServicio;
            _mapper = mapper;
        }


        [ClaimsRequirement("Menu", "/Recursos/Index")]
        public async Task<IActionResult> Index()
        {
            
            return View(_mapper.Map<List<RecursoDto>>(await _recursoServicio.ListarRecursos()));
        }


        public async Task<IActionResult> _CrearPartial()
        {
            ViewBag.items = await cargarListaCategoriasP();
            ViewBag.tipos = await cargarListaTiposRecurso();
            
            return PartialView();
        }


        public async Task<IActionResult> _EditarPartial(int? id)
        {
            ViewBag.items = await cargarListaCategoriasP();
            ViewBag.tipos = await cargarListaTiposRecurso();
           
            RecursoDto RecursoEncontrado = _mapper.Map<RecursoDto>(await _recursoServicio.BuscarRecursoPorId(id));
            return PartialView(RecursoEncontrado);
        }

        
        public async Task<IActionResult> _DetallePartial(int? id)
        {
            ViewBag.items = await cargarListaCategoriasP();
            ViewBag.tipos = await cargarListaTiposRecurso();
           
            RecursoDto RecursoEncontrado = _mapper.Map<RecursoDto>(await _recursoServicio.BuscarRecursoPorId(id));
            return PartialView(RecursoEncontrado);
        }


        public async Task<IActionResult> Recurso()
        {
            ViewBag.items = await cargarListaCategoriasP();
            ViewBag.tipos = await cargarListaTiposRecurso();
            return View();
        }


        private async Task<List<SelectListItem>> cargarListaCategoriasP()
        {
            List<categorias_parqueadero> lista = await _categoriaParqueaderoServicio.ListarCategoriasParqueadero();
            List<SelectListItem> items = lista.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.v_descripcion,
                    Value = p.n_id.ToString(),
                    Selected = false
                };
            });

            return items;
        }
        

        private async Task<List<SelectListItem>> cargarListaTiposRecurso()
        {
            List<tipos_recurso> lista = await _recursoServicio.ListarTiposRecursos();
            List<SelectListItem> items = lista.ConvertAll(p =>
            {
                return new SelectListItem()
                {
                    Text = p.v_descripcion,
                    Value = p.n_id.ToString(),
                    Selected = false
                };
            });

            return items;
        }

        public async Task<IActionResult> EditarReservaRecurso([Bind("n_id, n_id_recurso,d_fecha_hora_inicio,d_fecha_hora_fin,v_placa_vehiculo")] ReservaParqueaderoDto reserva)
        {
            try
            {
                SesionDto IdUsuarioLogueado = _sesionServicio.ObtenerSesion(HttpContext);
                reserva.n_id_usuario = IdUsuarioLogueado.n_id;
                reserva.n_estado = 1;

                reserva.n_tipo_reserva = Enumeradores.tipodeReservaPorUsuario;

                await _reservaServicio.EditarReservaRecurso(_mapper.Map<reserva>(reserva));
                return Json(new RespuestaDto { success = true, message = "Reserva editada exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }


        [HttpGet]
        public async Task<JsonResult> ObtenerParqueaderos(string idCategoria, string idSotano)
        {
            try
            {
                List<recurso> parqueaderos = await _recursoServicio.ListarParqueaderos(int.Parse(idCategoria), int.Parse(idSotano));
                return Json(new { success = true, data = _mapper.Map<List<ParqueaderoDto>>(parqueaderos) });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }
        
        
        [HttpGet]
        public async Task<JsonResult> ObtenerRecursos(string idTipoRecurso, string idNivel)
        {
            try
            {
                List<recurso> recursos = await _recursoServicio.ListarRecursos(int.Parse(idTipoRecurso), int.Parse(idNivel));
                return Json(new { success = true, data = _mapper.Map<List<RecursoDto>>(recursos) });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }
        
        
        [HttpGet]
        public async Task<JsonResult> ObtenerNiveles(string TipoRecurso)
        {
            try
            {
                List<niveles> niveles = await _nivelServicio.ListarNiveles(int.Parse(TipoRecurso));
                return Json(new { success = true, data=niveles });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<JsonResult> ObtenerReservas(string id)
        {

            List<AgendaDto> da = new List<AgendaDto>();
            try
            {
                List<reserva> reservas = await _reservaServicio.ListarReservas(id);
                foreach (var item in reservas)
                {
                    string endfin = item.d_fecha_hora_fin.ToString();
                    string start = item.d_fecha_hora_inicio.ToString();
                    string end = item.d_fecha_hora_fin.ToString();

                    if (start != null && end != null)
                    {
                        da.Add(new AgendaDto
                        {
                            id = item.n_id,
                            title = item.n_id_usuarioNavigation.n_id_personaNavigation.v_nombres,
                            start = DateTime.Parse(start).ToString("s"),
                            end = DateTime.Parse(end).ToString("s"),
                        });
                    }
                }

                return Json(new { success = true, data = da });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CrearReservaParqueadero([Bind("n_id_recurso,d_fecha_hora_inicio,d_fecha_hora_fin,v_placa_vehiculo")] ReservaParqueaderoDto reserva)
        {
            try
            {
                SesionDto IdUsuarioLogueado = _sesionServicio.ObtenerSesion(HttpContext);

                reserva.n_id_usuario = IdUsuarioLogueado.n_id;
                reserva.n_tipo_reserva = Enumeradores.tipodeReservaPorUsuario;
                reserva.v_placa_vehiculo = reserva.v_placa_vehiculo.ToUpper();

                await _reservaServicio.ReservaParqueadero(_mapper.Map<reserva>(reserva));
                return Json(new RespuestaDto { success = true, message = "Reserva creada exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }
        
        
        [HttpPost]
        public async Task<IActionResult> CrearReservaRecurso([Bind("n_id_recurso,d_fecha_hora_inicio,d_fecha_hora_fin")] ReservaSalaDto reserva)
        {
            try
            {
                    SesionDto IdUsuarioLogueado = _sesionServicio.ObtenerSesion(HttpContext);
                    reserva.n_id_usuario = IdUsuarioLogueado.n_id;

                    reserva.n_tipo_reserva = Enumeradores.tipodeReservaPorUsuario;

                    await _reservaServicio.ReservaRecurso(_mapper.Map<reserva>(reserva));
                    return Json(new RespuestaDto { success = true, message = "Reserva creada exitosamente" });
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }
        
    
        [HttpPost]
        public async Task<IActionResult> CrearNuevoRecurso(RecursoDto recurso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _recursoServicio.CrearRecurso(_mapper.Map<recurso>(recurso));
                    return Json(new RespuestaDto { success = true, message = "Recurso creado exitosamente" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return Json(new RespuestaDto { success = false, messages = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditarRecurso(RecursoDto recurso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _recursoServicio.EditarRecurso(_mapper.Map<recurso>(recurso));
                    return Json(new RespuestaDto { success = true, message = "Recurso editado exitosamente" });
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                  .Select(e => e.ErrorMessage)
                                                  .ToList();
                    return Json(new RespuestaDto { success = false, messages = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int n_id)
        {
            try
            {
                if (n_id != null)
                {
                    SesionDto Logueado = _sesionServicio.ObtenerSesion(HttpContext);
                    var n_id_usuario = Logueado.n_id;

                    if (await _recursoServicio.CambiarEstado(n_id))
                        return Json(new RespuestaDto { success = true, message = MensajesSistema.MsgEstadoCambiado });
                    else
                        return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgNosePudoCambiarElEstado });
                }
                else
                {
                    return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgNosePudoCambiarElEstado });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }


        [HttpPost]
        public async Task<IActionResult> CambiarEstadoReserva(int n_id)
        {
            try
            {
                if (n_id != null)
                {
                    SesionDto Logueado = _sesionServicio.ObtenerSesion(HttpContext);
                    var n_id_usuario = Logueado.n_id;

                    if (await _reservaServicio.CambiarEstado(n_id_usuario, n_id))
                        return Json(new RespuestaDto { success = true, message = MensajesSistema.MsgReservaCancelada });
                    else
                        return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgReservaNoCancelada });
                }
                else
                {
                    return Json(new RespuestaDto { success = false, message = MensajesSistema.MsgReservaNoCancelada });
                }
            }
            catch (Exception ex)
            {
                return Json(new RespuestaDto { success = false, message = ex.Message });
            }

        }

    }

}
