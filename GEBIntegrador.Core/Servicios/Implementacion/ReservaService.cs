using GEBIntegrador.Core.Servicios.Contrato;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dominio.Mensajes;
using GEBIntegrador.Persistencia.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Core.Servicios.Implementacion
{
    public class ReservaService : IReservaService
    {
        private readonly IGenericRepositorio<reserva> _reservaRepositorio;


        public ReservaService(IGenericRepositorio<reserva> reservaRepositorio)
        {
            _reservaRepositorio = reservaRepositorio;   
        }

        //public async Task<List<reserva>> ListarReservas(string tipoRecurso, string nivel)
        //{
        //    List<reserva> listaReservas = new List<reserva>();
        //    try
        //    {

        //        var queryReservas = await _reservaRepositorio.Consultar();

        //        listaReservas = queryReservas.Include(r => r.n_id_usuarioNavigation.n_id_personaNavigation)
        //            .Include(o => o.n_id_recursoNavigation)
        //        .Where(x => x.n_estado == 1 
        //        && x.n_id_recursoNavigation.n_tipo_recurso.ToString() == tipoRecurso
        //        && x.n_id_recursoNavigation.n_nivel_recurso.ToString() == nivel).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return listaReservas;
        //}
        
        public async Task<List<reserva>> ListarReservas(string id)
        {
            List<reserva> listaReservas = new List<reserva>();
            try
            {

                var queryReservas = await _reservaRepositorio.Consultar();

                listaReservas = queryReservas.Include(r => r.n_id_usuarioNavigation.n_id_personaNavigation)
                    .Include(o => o.n_id_recursoNavigation)
                .Where(x => x.n_estado == 1 
                && x.n_id_recursoNavigation.n_id.ToString() == id ).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listaReservas;
        }


        public async Task<reserva> ReservaParqueadero(reserva modelo)
        {
            try
            {

                //Validar que la jornada seleccionada o la jornada todo dia no esté seleccionada
                //if no estan seleccionada crear la reservas 
                // lese retornar un mensaje pruede ser un trow throw new TaskCanceledException("Jornada no disponible, por favro seleccione una nueva");
                modelo.d_fecha_reserva = DateTime.Now;

                reserva reservaNueva;
                if (await ValidarDisponibilidad(modelo))
                {
                    reservaNueva = await _reservaRepositorio.Crear(modelo);

                    if (reservaNueva.n_id == 0)
                    {
                        throw new TaskCanceledException("No se pudo crear la reserva");
                    }

                }
                else
                {
                    throw new TaskCanceledException("Recurso no disponible para los tiempos seleccionados");
                }

                return reservaNueva;


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
        
        
        
        public async Task<reserva> ReservaRecurso(reserva modelo)
        {
            try
            {
                modelo.d_fecha_reserva = DateTime.Now;

                reserva reservaNueva;
                if (await ValidarDisponibilidad(modelo))
                {
                    reservaNueva = await _reservaRepositorio.Crear(modelo);

                    if (reservaNueva.n_id == 0)
                    {
                        throw new TaskCanceledException("No se pudo crear la reserva");
                    }

                }
                else
                {
                    throw new TaskCanceledException("Recurso no disponible para los tiempos seleccionados");
                }

                return reservaNueva;


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


        public async Task<reserva> EditarReservaRecurso(reserva modelo)
        {
            try
            {
                var queryReservas = await _reservaRepositorio.Obtener(x => x.n_estado == 1 && x.n_id == modelo.n_id);

                modelo.d_fecha_reserva = DateTime.Now;

                reserva reservaEditada;

                if (await ValidarDisponibilidad(modelo))
                {
                    if (modelo.n_id_usuario == queryReservas.n_id_usuario)
                    {

                        queryReservas.d_fecha_hora_inicio = modelo.d_fecha_hora_inicio;
                        queryReservas.d_fecha_hora_fin = modelo.d_fecha_hora_fin;
                        queryReservas.v_placa_vehiculo = modelo.v_placa_vehiculo;
                        queryReservas.n_estado = modelo.n_estado;

                        reservaEditada = await _reservaRepositorio.Editar(queryReservas);

                        if (reservaEditada.n_id == 0)
                        {
                            throw new TaskCanceledException("No se pudo editar la reserva");
                        }

                    }
                    else
                    {
                        throw new TaskCanceledException("No puede editar reservas de otras personas");
                    }

                }
                else
                {
                    throw new TaskCanceledException("Recurso no disponible para los tiempos seleccionados");
                }
                return reservaEditada;
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


        public async Task<bool> ValidarDisponibilidad(reserva modelo)
        {
            List<reserva> listaReservas = new List<reserva>();
            try
            {

                var queryReservas = await _reservaRepositorio.Consultar();

                listaReservas = queryReservas.Where(x => x.n_estado == 1 &&
                x.n_id_recurso == modelo.n_id_recurso &&
               ((modelo.d_fecha_hora_inicio >= x.d_fecha_hora_inicio && modelo.d_fecha_hora_inicio < x.d_fecha_hora_fin) ||
                     (modelo.d_fecha_hora_fin > x.d_fecha_hora_inicio && modelo.d_fecha_hora_fin <= x.d_fecha_hora_fin) ||
                     (modelo.d_fecha_hora_inicio <= x.d_fecha_hora_inicio && modelo.d_fecha_hora_fin >= x.d_fecha_hora_fin))).ToList();


                if (listaReservas.Count != 0)
                {
                    if (listaReservas.Count == 1)
                    {
                        var Reserva = listaReservas[0];
                        if (Reserva.n_id == modelo.n_id)
                        {
                            return true;
                        }
                        else { 
                            return false; 
                        }
                    }
                    return false;
                }
                else { 
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CambiarEstado(int n_id_usuario, int n_id_reserva)
        {
            bool respuesta = true;
            try
            {
                var queryReservas = await _reservaRepositorio.Obtener(x => x.n_estado == 1 && x.n_id == n_id_reserva);

                if (n_id_usuario == queryReservas.n_id_usuario)
                {
                    queryReservas.n_estado = queryReservas.n_estado == 1 ? 0 : 1;
                    queryReservas.d_fecha_cancela = DateTime.Now;
                    queryReservas.n_id_usuario_cancela = n_id_usuario;

                    queryReservas = await EditarReservaRecurso(queryReservas);
                }
                else
                {
                    throw new TaskCanceledException("No puede cancelar reservas de otras personas");
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                throw new Exception(ex.Message);
            }
            return respuesta;
        }
    }

}
