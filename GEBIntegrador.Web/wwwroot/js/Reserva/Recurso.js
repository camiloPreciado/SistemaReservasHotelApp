import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../shared/mensajes.js"


var msg = new Mensajes();
let ajax = new ajaxService();

var fechaSeleccionada;
var idRecurso;
var calendar;
var idReserva;
var cardRecursos = document.getElementById('card_recursos');
var cardMapa = document.getElementById('card_mapa');
var infoText = document.getElementById('info');
var diasLimite = 2;
var sab;
var dom;


$(document).ready(function () {
    cardRecursos.style.display = 'none';
    cardMapa.style.display = 'none';
    infoText.style.display = 'none';
    //DiasHabiles();
    //Cargar Timepickers
    jQuery('#timepicker-input').timepicker();
    jQuery('#timepicker-input-fin').timepicker();
    validezSabado();
    validezDomingo();
})


//function DiasHabiles() {
//    var data = new Object()
//    data.id = 11;

//    ajax.get("/Parametros/ObtenerParametros", data).then((d) => {
//        if (d.success) {
//            diasLimite = d.data.v_valor;
//        }
//    });
//}

// Función para convertir la hora al formato deseado
function convertirHoraFormato(hora) {
    // Divide la cadena en partes (hora, minutos, AM/PM)
    var partes = hora.match(/(\d{1,2}):(\d{2})\s?([APMapm]{2})/);

    if (partes && partes.length === 4) {
        var horas = parseInt(partes[1]);
        var minutos = partes[2];
        var meridiano = partes[3].toUpperCase();

        // Convierte las horas al formato de 24 horas
        if (meridiano === "PM" && horas !== 12) {
            horas += 12;
        } else if (meridiano === "AM" && horas === 12) {
            horas = 0;
        }

        // Formatea las horas y minutos en el formato deseado
        return horas.toString().padStart(2, '0') + ":" + minutos + ":00";
    } else {
        // Manejar el caso en el que la hora no se pueda parsear
        console.error("No se pudo parsear la hora:", hora);
        return null;
    }
}

function valorDefecto(nivelDropdown,tipo) {
    var defaultOption = document.createElement('option');
    defaultOption.value = "";
    defaultOption.text = `--Elija un ${tipo}--`;
    nivelDropdown.appendChild(defaultOption);
}

function cargarNivelesSegunTipoRecurso() {
    limpiarEventos();
    limpiarObjetos()
    cardRecursos.style.display = 'none';
    


    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    
    var nivel =document.getElementById('nivelDropdownItems');
    nivel.innerHTML = "";
    valorDefecto(nivel, "nivel");

    var recurso = document.getElementById('recursosDropdownItems');
    recurso.innerHTML = "";
    valorDefecto(recurso, "recurso");

    var contenedor = document.getElementById('external-events-list');
    contenedor.innerHTML = '';

    document.getElementById('categoriaDropdownItems').selectedIndex =0;
    


    var categoriaParqueaderoContainer = document.getElementById('categoriaParqueaderoContainer');
    var placaContainer = document.getElementById('placaContainer');

    if (tipoRecursoId === "1") {
        categoriaParqueaderoContainer.style.display = 'block';
        placaContainer.style.display = 'block';
    } else if (tipoRecursoId === "3") {
        categoriaParqueaderoContainer.style.display = 'none';
        placaContainer.style.display = 'none';
    } else {
        categoriaParqueaderoContainer.style.display = 'none';
        placaContainer.style.display = 'none';
    }


    if (tipoRecursoId != "") {
        var data = new Object()
        data.TipoRecurso = tipoRecursoId;

        ajax.get("/Recursos/ObtenerNiveles", data).then((d) => {
            procesarRespuestaNiveles(d);
        });
    }
}

function procesarRespuestaNiveles(d) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var nivelDropdown = document.getElementById('nivelDropdownItems');
            nivelDropdown.innerHTML = "";
            valorDefecto(nivelDropdown, "nivel");

            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_descripcion;
                nivelDropdown.appendChild(option);
            });
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}


function cargarRecursos() {
    limpiarEventos();
    limpiarObjetos();
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    var nivelId = document.getElementById('nivelDropdownItems').value;

    if (tipoRecursoId == 1 && nivelId != "") {
        var categoriaId = document.getElementById('categoriaDropdownItems').value;
        if (nivelId != "") {
            var data = new Object()
            data.idCategoria = categoriaId;
            data.idSotano = nivelId;

            ajax.get("/Recursos/ObtenerParqueaderos", data).then((d) => {
                procesarRespuestaListaRecursos(d);
            });

        }
    } else if (tipoRecursoId != 1 && nivelId != "") {
        var data = new Object()
        data.idTipoRecurso = tipoRecursoId;
        data.idNivel = nivelId;

        ajax.get("/Recursos/ObtenerRecursos", data).then((d) => {
            procesarRespuestaListaRecursos(d);
        });

    }
}

function procesarRespuestaListaRecursos(d) {
    if (d.success) {
        if (d.data != null) {  
       //     cardMapa.style.display = 'block';
            var lista = d.data;

            var recursoDropdown = document.getElementById('recursosDropdownItems');
            recursoDropdown.innerHTML = "";
            valorDefecto(recursoDropdown, "recurso");

            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_nombre;
                recursoDropdown.appendChild(option);
            });

            // Agregar evento al cambio de selección en el dropdown de salas
            //recursoDropdown.addEventListener('change', function () {
            //    var selectedIndex = recursoDropdown.selectedIndex;

            //    // Verificar si se seleccionó una sala válida (no "--Elija una sala--")
            //    if (selectedIndex > 0) {
            //        mostrarCapacidadSala(lista[selectedIndex - 1].n_capacidad);
            //    } else {
            //        // Si no se selecciona una sala válida, puedes manejarlo según tus necesidades.
            //        // Por ejemplo, puedes limpiar el campo de capacidad o mostrar un mensaje.
            //        mostrarCapacidadSala(""); // Limpiar el campo de capacidad
            //    }
            //});

            //lista.forEach(function (item) {
            //    // Crear el elemento div principal
            //    var eventDiv = document.createElement("div");
            //    eventDiv.className = "fc-event fc-h-event fc-daygrid-event fc-daygrid-block-event";
            //    eventDiv.dataset.event = JSON.stringify({ title: item.v_nombre, id: item.n_id });
            //    /*eventDiv.style.background = item.Color;*/
            //    eventDiv.style.margin = "2px";

            //    // Crear el elemento interno con el evento onclick
            //    var eventMainDiv = document.createElement("div");
            //    eventMainDiv.className = "fc-event-main";
            //    eventMainDiv.id = item.n_id;
            //    eventMainDiv.style.cursor = "move";
            //    eventMainDiv.style.padding = "8px";
            //    eventMainDiv.onclick = function () {
            //        verventanilla(this.id);
            //    };

            //    // Crear el párrafo dentro del elemento interno
            //    var pElement = document.createElement("p");
            //    pElement.style.marginBottom = "5px";
            //    pElement.title = item.v_nombre;
            //    pElement.textContent = item.v_nombre;
            //    if (item.n_tipo_recurso == 3) {
            //        var brElement = document.createElement("br");

            //        // Agregar el salto de línea al párrafo
            //        pElement.appendChild(brElement);

            //        // Agregar la capacidad al párrafo
            //        pElement.innerHTML += "Capacidad: " + item.n_capacidad+ " personas" ;
            //    } 

            //    // Agregar el párrafo al elemento interno
            //    eventMainDiv.appendChild(pElement);

            //    // Agregar el elemento interno al elemento principal
            //    eventDiv.appendChild(eventMainDiv);

            //    // Agregar el elemento principal al contenedor
            //    contenedor.appendChild(eventDiv);
            //});
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}


$("#categoriaDropdownItems").on('change', function () {
    limpiarEventos();
    limpiarObjetos();
    cardRecursos.style.display = 'none';
});


//Se ejecuta cuando se selecciona el recurso
$("#recursosDropdownItems").on('change', function () {
    validezSabado();
    validezDomingo();
    var recurso = document.getElementById('recursosDropdownItems');
    var textoRecurso = recurso.options[recurso.selectedIndex].text;
    var item = new Object()
    item.n_id = $(this).val();
    item.v_nombre = textoRecurso;

    $("#tittle").empty();
    $("#tittle").append(`Agenda del recurso ${textoRecurso}`);

    cardRecursos.style.display = 'block';

    idRecurso = recurso.value;

    var data = new Object()
    data.id = $(this).val();

    ajax.get("/Recursos/ObtenerReservas", data).then((d) => {
        procesarRespuestaCalendario(d, calendar);
    });


    crearObjeto(item)
});

function crearObjeto(item) {
    var contenedor = document.getElementById('external-events-list');
    contenedor.innerHTML = '' 
   
    var eventDiv = document.createElement("div");
    eventDiv.className = "fc-event fc-h-event fc-daygrid-event fc-daygrid-block-event";
    eventDiv.dataset.event = JSON.stringify({ title: item.v_nombre, id: item.n_id });
    eventDiv.style.margin = "2px";

    // Crear el elemento interno con el evento onclick
    var eventMainDiv = document.createElement("div");
    eventMainDiv.className = "fc-event-main";
    eventMainDiv.id = item.n_id;
    eventMainDiv.style.cursor = "move";
    eventMainDiv.style.padding = "8px";
    eventMainDiv.onclick = function () {
        verventanilla(this.id);
    };

    // Crear el párrafo dentro del elemento interno
    var pElement = document.createElement("p");
    pElement.style.marginBottom = "5px";
    pElement.title = item.v_nombre;
    pElement.textContent = item.v_nombre;
    if (item.n_tipo_recurso == 3) {
        var brElement = document.createElement("br");
        pElement.appendChild(brElement);
        pElement.innerHTML += "Capacidad: " + item.n_capacidad + " personas";
    }

    eventMainDiv.appendChild(pElement);
    eventDiv.appendChild(eventMainDiv);
    contenedor.appendChild(eventDiv);   
}

function cargarPdf() {
    var nivelLista = document.getElementById('nivelDropdownItems');
    var textoNivel = nivelLista.options[nivelLista.selectedIndex].text;
    var pdfPath = '/images/niveles/' + textoNivel + '.pdf';
    document.getElementById('pdfViewer').src = pdfPath;

    var modal = $('#myModal');
    modal.modal('show');
}

$("#mapa-button").unbind().click(cargarPdf);


$("#okk-button").on("click", function(){
    var data_edit = $(this).data("edit");
    if (data_edit == 0) {
        realizarReserva();
    } else {
        editarReserva();
    }
});


$("#cancel-button").unbind().click(limpiarObjetos);

$('#exampleModal').on('hidden.bs.modal', function () {
    infoText.style.display = 'none';
    limpiarObjetos();
});


function realizarReserva() {
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    var horaInicio = $("#timepicker-input").val().trim();
    var horaFin = $("#timepicker-input-fin").val().trim();

    if (horaInicio !== null && horaInicio !== "" && horaFin !== null && horaFin !== "") {
        var horaInicioFormateada = convertirHoraFormato(horaInicio);
        var horaFinFormateada = convertirHoraFormato(horaFin);

        var callback = (result) => {
            if (result) {
                if (validarHoras(horaInicioFormateada, horaFinFormateada)) {
                    var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicioFormateada;
                    var fecha_hora_fin = fechaSeleccionada + "T" + horaFinFormateada;


                    if (tipoRecursoId == "1") {
                        var placa = document.getElementById('plaque').value;

                        if (placa !== null && placa.trim() !== "") {
                            var data = new Object()
                            data.n_id_recurso = idRecurso;
                            data.d_fecha_hora_inicio = fecha_hora_inicio;
                            data.d_fecha_hora_fin = fecha_hora_fin;
                            data.v_placa_vehiculo = placa;

                            ajax.post("/Recursos/CrearReservaParqueadero", data).then((d) => {
                                procesarRespuestaCreacionReserva(d);
                            });
                        } else {
                            msg.msgError("La placa no puede estar vacia");
                        }


                    } else {
                        var data = new Object()
                        data.n_id_recurso = idRecurso;
                        data.d_fecha_hora_inicio = fecha_hora_inicio;
                        data.d_fecha_hora_fin = fecha_hora_fin;


                        ajax.post("/Recursos/CrearReservaRecurso", data).then((d) => {
                            procesarRespuestaCreacionReserva(d);
                        });
                    }
                }
               
            }
        }
        validarHoraInicio(callback, horaInicioFormateada, horaFinFormateada);
    } else {
        msg.msgError("Ninguna de las horas puede estar vacia");
    }  
}

//function realizarReserva() {
//    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
//    var horaInicio = $("#timepicker-input").val().trim();
//    var horaFin = $("#timepicker-input-fin").val().trim();

//    if (horaInicio !== null && horaInicio !== "" && horaFin !== null && horaFin !== "") {
//        var horaInicioFormateada = convertirHoraFormato(horaInicio);
//        var horaFinFormateada = convertirHoraFormato(horaFin);


//        if (validarHoras(horaInicioFormateada, horaFinFormateada)) {
//            var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicioFormateada;
//            var fecha_hora_fin = fechaSeleccionada + "T" + horaFinFormateada;


//            if (tipoRecursoId == "1") {
//                var placa = document.getElementById('plaque').value;

//                if (placa !== null && placa.trim() !== "") {
//                    var data = new Object()
//                    data.n_id_recurso = idRecurso;
//                    data.d_fecha_hora_inicio = fecha_hora_inicio;
//                    data.d_fecha_hora_fin = fecha_hora_fin;
//                    data.v_placa_vehiculo = placa;

//                    ajax.post("/Recursos/CrearReservaParqueadero", data).then((d) => {
//                        procesarRespuestaCreacionReserva(d);
//                    });
//                } else {
//                    msg.msgError("La placa no puede estar vacia");
//                }
                

//            } else {
//                var data = new Object()
//                data.n_id_recurso = idRecurso;
//                data.d_fecha_hora_inicio = fecha_hora_inicio;
//                data.d_fecha_hora_fin = fecha_hora_fin;


//                ajax.post("/Recursos/CrearReservaRecurso", data).then((d) => {
//                    procesarRespuestaCreacionReserva(d);
//                });
//            }
//        }
//    } else {
//        msg.msgError("Ninguna de las horas puede estar vacia");
//    }
//}

function procesarRespuestaCreacionReserva(d) {
    if (d.success) {
        limpiarObjetos();
        limpiarEventos();
        var data = new Object()
        data.id = idRecurso;

        ajax.get("/Recursos/ObtenerReservas", data).then((d) => {
            procesarRespuestaCalendario(d, calendar);
        });

        msg.msgSuccess(d.message).then((result) => {            
        });
    } else {
        if (!d.messages) {
            limpiarObjetos();
            msg.msgError(d.message);
        }
        else {
            limpiarObjetos();
            msg.msgError(d.messages.join('<br>'));
        }
    }
}





//Cargue de agenda
document.addEventListener('DOMContentLoaded', function () {
    var containerEl = document.getElementById('external-events-list');
    new FullCalendar.Draggable(containerEl, {
        itemSelector: '.fc-event',
        locale: 'es',
        eventData: function (eventEl) {
            return {
                title: eventEl.innerText.trim()
            };
        }
    });

    var calendarEl = document.getElementById('calendar');
    calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'es',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        eventSources: [],
        eventDragStop: function (data) {
            // Revertir el cambio de posición del evento
            data.revert();
            //msg.msgError("No puede mover esta reserva");
        },
        eventReceive: function (data) {
            var evento = JSON.parse(data.draggedEl.dataset.event);
            var dt = {
                HoraInicial: moment(data.event.startStr).format("YYYY-MM-DD HH:mm:ss"),
                HoraFinal: moment(data.event.endStr).format("YYYY-MM-DD HH:mm:ss"),
                Nombre: data.event._def.title,
                Fecha: moment(data.event.startStr).format("YYYY-MM-DD"),
                Id: evento.id
            };
            NAgendaEvento(dt);
        },
        eventResize: function (data) {
            var dat = {
                HoraInicial: moment(data.event.startStr).format("HH:mm"),
                HoraFinal: moment(data.event.endStr).format("HH:mm"),
                Nombre: data.event._def.title,
                Id: data.event.id
            };
            // Realizar acciones necesarias al cambiar el tamaño de un evento
        },
        editable: true,
        droppable: true,
        drop: function (arg) {
            //Para cuando son sabados
            var droppedDate = arg.date;
            if (droppedDate.getDay() === 6) {
                if (sab == 0) {
                    msg.msgError("¡No esta permitido reservas los sábados!");
                    arg.revert();
                }
            } else if (droppedDate.getDay() === 0) {
                if (dom == 0) {
                    msg.msgError("¡No esta permitido reservas los domingos!");
                    arg.revert();
                }
               
            }
        },
        eventClick: function (info) {
            Swal.fire({
                title: "¿Qué desea hacer con la reserva?",
                showDenyButton: true,
                showCancelButton: true,
                cancelButtonText: 'Salir',
                confirmButtonText: "Editar la reserva",
                denyButtonText: "Cancelar la reserva"
                }).then((result) => {
                    
                    if (result.isConfirmed) {
                        infoText.style.display = 'block';
                        procesarFecha(info.event._instance.range.start);
                        var callback = (result) => {
                            if (result) {
                                var modal = $('#exampleModal');
                                modal.modal('show');
                                $('#okk-button').data("edit", 1);
                                $("#okk-button").text(`Editar`);
                                idReserva = info.event._def.publicId;

                            }
                        }
                        validarFechaReserva(callback);

                    } else if (result.isDenied) {
                        procesarFecha(info.event._instance.range.start);
                        var callback = (result) => {
                            if (result) {
                                idReserva = info.event._def.publicId;
                                CambiarEstado(idReserva);
                                
                            }
                        }
                        validarFechaReserva(callback);
                    }
            });
        }
    });
    calendar.render();

  
});


function procesarRespuestaCalendario(data, calendar) {
    // Verificar si la respuesta es exitosa
    if (data.success) {
        calendar.getEventSources().forEach(source => {
            source.remove();
        });

        // Agregar los nuevos eventos al calendario
        calendar.addEventSource({
            events: data.data,
            color: '#1F9AA5',
            textColor: 'white'
        });

        // Refrescar el calendario para mostrar los cambios
        calendar.refetchEvents();
    } else {
        // Manejar el caso de error, por ejemplo, mostrar un mensaje de alerta
        alert('Error al obtener eventos del calendario: ' + data.message);
    }
}


function limpiarEventos() {
    calendar.getEventSources().forEach(source => {
        source.remove();
    });
}

function limpiarObjetos() {
    var draggedEvents = calendar.getEvents().filter(event => event.source === null);
    draggedEvents.forEach(draggedEvent => {
        draggedEvent.remove();
    });
}

//function NAgendaEvento(eleccion) { 
//    fechaSeleccionada = eleccion.Fecha;
//    if (validarFechaReserva()) {
//        var modal = $('#exampleModal');
//        modal.modal('show');
//        $('#okk-button').data("edit", 0);
//        $("#okk-button").text(`Reservar`);
//    }
//}
function NAgendaEvento(eleccion) { 
    fechaSeleccionada = eleccion.Fecha;

    var callback = (result) => {
        if (result) {
            var modal = $('#exampleModal');
            modal.modal('show');
            $('#okk-button').data("edit", 0);
            $("#okk-button").text(`Reservar`);
        }
    }
    validarFechaReserva(callback);
}


function validezSabado() {
    var data = new Object()
    data.id = 1;
    ajax.get("/Parametros/ObtenerParametros", data).then((d) => {
        if (d.success) {
            sab = d.data.v_valor;
        }
    });
}

function validezDomingo() {
    var data = new Object()
    data.id = 2;
    ajax.get("/Parametros/ObtenerParametros", data).then((d) => {
        if (d.success) {
            dom = d.data.v_valor;
        }
    });
}

//function validarFechaReserva() {
//    var fechaActual = new Date();
//    var year = fechaActual.getFullYear();
//    var month = ('0' + (fechaActual.getMonth() + 1)).slice(-2);
//    var day = ('0' + fechaActual.getDate()).slice(-2);
//    var fechaActualFormateada = year + '-' + month + '-' + day;


//    if (fechaSeleccionada < fechaActualFormateada) {
//        msg.msgError("La fecha de la reserva no puede ser anterior a la fecha de hoy");
//        limpiarObjetos();
//        return false;
//    } else {

//        var data = new Object()
//        data.id = 11;

//        ajax.get("/Parametros/ObtenerParametros", data).then((d) => {
//            if (d.success) {
//                diasLimite = d.data.v_valor;
//                var fechaLimite = new Date();
//                fechaLimite = new Date(fechaLimite.getTime() + diasLimite * 24 * 60 * 60 * 1000);
//                var year = fechaLimite.getFullYear();
//                var month = ('0' + (fechaLimite.getMonth() + 1)).slice(-2);
//                var day = ('0' + fechaLimite.getDate()).slice(-2);
//                var fechaLimiteFormateada = year + '-' + month + '-' + day;
//                if (fechaSeleccionada > fechaLimiteFormateada) {
//                    msg.msgError("La fecha de la reserva no puede ser posterior a " + diasLimite + " días hábiles a partir de hoy");
//                    limpiarObjetos();
//                    return false;
//                }
//                return true;
//            }
//        });

//    }
//}

function validarFechaReserva(callback) {
    var data = new Object()
    data.id = 3;
    ajax.get("/Parametros/ObtenerParametros", data).then((d) => {

        if (d.success) {
            var fechaActual = new Date();
            var year = fechaActual.getFullYear();
            var month = ('0' + (fechaActual.getMonth() + 1)).slice(-2);
            var day = ('0' + fechaActual.getDate()).slice(-2);
            var fechaActualFormateada = year + '-' + month + '-' + day;

            if (fechaSeleccionada < fechaActualFormateada) {
                msg.msgError("La fecha de la reserva no puede ser anterior a la fecha de hoy");
                limpiarObjetos();
                return false;
            } else {
                diasLimite = d.data.v_valor;
                var fechaLimite = new Date();
                fechaLimite = new Date(fechaLimite.getTime() + diasLimite * 24 * 60 * 60 * 1000);
                var year = fechaLimite.getFullYear();
                var month = ('0' + (fechaLimite.getMonth() + 1)).slice(-2);
                var day = ('0' + fechaLimite.getDate()).slice(-2);
                var fechaLimiteFormateada = year + '-' + month + '-' + day;
                if (fechaSeleccionada > fechaLimiteFormateada) {
                    msg.msgError("La fecha de la reserva no puede ser posterior a " + diasLimite + " días hábiles a partir de hoy");
                    limpiarObjetos();
                    //return false;
                    callback(false);
                } else {
                    //return true;
                    callback(true);
                }
            }
        }
    });

}



function procesarFecha(fechaReserva) {
    var year = fechaReserva.getFullYear();
    var month = ('0' + (fechaReserva.getMonth() + 1)).slice(-2);
    var day = ('0' + fechaReserva.getDate()).slice(-2);
    var fechaFormateada = year + '-' + month + '-' + day;
    fechaSeleccionada = fechaFormateada;
}


//function editarReserva() {
//    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
//    var horaInicio = $("#timepicker-input").val().trim();
//    var horaFin = $("#timepicker-input-fin").val().trim();
//    infoText.style.display = 'none';
//    if (horaInicio !== null && horaInicio !== "" && horaFin !== null && horaFin !== "") {

//        var horaInicioFormateada = convertirHoraFormato(horaInicio);
//        var horaFinFormateada = convertirHoraFormato(horaFin);

//        if (validarHoras(horaInicioFormateada, horaFinFormateada)) {
//            var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicioFormateada;
//            var fecha_hora_fin = fechaSeleccionada + "T" + horaFinFormateada;

//            var data = new Object()
//            data.n_id = idReserva;
//            data.n_id_recurso = idRecurso;
//            data.d_fecha_hora_inicio = fecha_hora_inicio;
//            data.d_fecha_hora_fin = fecha_hora_fin;

//            if (tipoRecursoId == "1") {
//                var placa = document.getElementById('plaque').value;
//                if (placa !== null && placa.trim() !== "") {
//                    data.v_placa_vehiculo = placa;
//                } else {
//                    msg.msgError("La placa no puede estar vacia");
//                    return;
//                }
//            }


//            ajax.post("/Recursos/EditarReservaRecurso", data).then((d) => {
//                procesarRespuestaCreacionReserva(d);
//            });
//        }
//    } else {
//        msg.msgError("Ninguna de las horas puede estar vacia");
//    }
//}

function editarReserva() {
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    var horaInicio = $("#timepicker-input").val().trim();
    var horaFin = $("#timepicker-input-fin").val().trim();
    infoText.style.display = 'none';
    if (horaInicio !== null && horaInicio !== "" && horaFin !== null && horaFin !== "") {

        var horaInicioFormateada = convertirHoraFormato(horaInicio);
        var horaFinFormateada = convertirHoraFormato(horaFin);

        var callback = (result) => {
            if (result) {
                if (validarHoras(horaInicioFormateada, horaFinFormateada)) {
                    var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicioFormateada;
                    var fecha_hora_fin = fechaSeleccionada + "T" + horaFinFormateada;

                    var data = new Object()
                    data.n_id = idReserva;
                    data.n_id_recurso = idRecurso;
                    data.d_fecha_hora_inicio = fecha_hora_inicio;
                    data.d_fecha_hora_fin = fecha_hora_fin;

                    if (tipoRecursoId == "1") {
                        var placa = document.getElementById('plaque').value;
                        if (placa !== null && placa.trim() !== "") {
                            data.v_placa_vehiculo = placa;
                        } else {
                            msg.msgError("La placa no puede estar vacia");
                            return;
                        }
                    }


                    ajax.post("/Recursos/EditarReservaRecurso", data).then((d) => {
                        procesarRespuestaCreacionReserva(d);
                    });
                }
            }
        }
        validarHoraInicio(callback, horaInicioFormateada, horaFinFormateada);
    } else {
        msg.msgError("Ninguna de las horas puede estar vacia");
    }
}


function CambiarEstado(idReserva) {

    let titulo = '¿Cancelar reserva?';
    let texto = "¿Está seguro que desea CANCELAR la reserva?";

    msg.msgConfirmacion(titulo, texto).then((result) => {
        if (result.isConfirmed)
            enviarCambio(idReserva);
    });
}


function enviarCambio(n_id) {
    var data = new Object();
    data.n_id = n_id;

    ajax.post("/Recursos/CambiarEstadoReserva", data).then((d) => {
        procesarRespuestaCambio(d);
    });
}

function procesarRespuestaCambio(d) {
    if (d.success) {
        limpiarObjetos();
        limpiarEventos();
        var data = new Object()
        data.id = idRecurso;

        ajax.get("/Recursos/ObtenerReservas", data).then((d) => {
            procesarRespuestaCalendario(d, calendar);
        });

        msg.msgSuccess(d.message).then((result) => {
        });
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}


function validarHoras(horaInicioFormateada, horaFinFormateada) {
    var horaActual = new Date();
    var horaActualFormateada = horaActual.getHours().toString().padStart(2, '0') + ":" + horaActual.getMinutes().toString().padStart(2, '0') + ":00";
    var fechaActual = new Date();
    var year = fechaActual.getFullYear();
    var month = ('0' + (fechaActual.getMonth() + 1)).slice(-2);
    var day = ('0' + fechaActual.getDate()).slice(-2);
    var fechaActualFormateada = year + '-' + month + '-' + day;


    if (fechaSeleccionada == fechaActualFormateada) {
        // Verificar que ninguna de las horas sea menor a la hora actual
        if (horaInicioFormateada < horaActualFormateada || horaFinFormateada < horaActualFormateada) {
            msg.msgError("Ninguna de las horas ingresadas puede ser menor a la hora actual");
            return false;
        }
    }
 
    // Verificar que la horaFinFormateada no sea menor o igual a horaInicioFormateada
    if (horaFinFormateada <= horaInicioFormateada) {
        msg.msgError("La Hora Fin no puede ser menor o igual a la Hora Inicio");
        return false;
    }

    // Verificar que horaFinFormateada y horaInicioFormateada no sean iguales
    if (horaFinFormateada === horaInicioFormateada) {
        msg.msgError("Las horas ingresadas no pueden ser iguales");
        return false;
    }

    // Calcular la diferencia en minutos
    var diferenciaMinutos = (new Date('1970-01-01T' + horaFinFormateada) - new Date('1970-01-01T' + horaInicioFormateada)) / (1000 * 60);

    // Verificar que la diferencia sea de al menos una hora (60 minutos)
    if (diferenciaMinutos < 60) {
        msg.msgError("La reserva debe ser de minimo una hora");
        return false;
    }

    // Todas las verificaciones pasaron
    return true;
}

function validarHoraInicio(callback, horaInicioFormateada, horaFinFormateada) {
    var data = new Object()
    data.id = 4;
    ajax.get("/Parametros/ObtenerParametros", data).then((d) => {

        if (d.success) {
            var rangoHoraInicio = d.data.v_valor;

            if (rangoHoraInicio > horaInicioFormateada) {
                msg.msgError("La hora de inicio no puede ser menor a " + rangoHoraInicio);
                limpiarObjetos();
                callback(false);
            } else {
                var respuesta = (resultado) => {
                    if (resultado) {
                        callback(true);
                    }
                }
                validarHoraFin(respuesta, horaFinFormateada);

                
            }
        }
    });


    function validarHoraFin(respuesta, horaFinFormateada) {
        var data = new Object()
        data.id = 5;
        ajax.get("/Parametros/ObtenerParametros", data).then((d) => {
            if (d.success) {
                var rangoHoraFin = d.data.v_valor;

                if (rangoHoraFin < horaFinFormateada) {
                    msg.msgError("La hora fin no puede ser superior a " + rangoHoraFin);
                    limpiarObjetos();
                    callback(false);
                } else {
                    callback(true);
                }
            }
        });
    } 
}


window.cargarNivelesSegunTipoRecurso = cargarNivelesSegunTipoRecurso;
window.cargarRecursos = cargarRecursos;