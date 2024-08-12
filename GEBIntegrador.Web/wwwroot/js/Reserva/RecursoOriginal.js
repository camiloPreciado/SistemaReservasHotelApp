import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../shared/mensajes.js"


var msg = new Mensajes();
let ajax = new ajaxService();

var fechaSeleccionada;


//< !--logica de timepicker  para cuando sea sala o puesto-- >
$(document).ready(function () {
    jQuery('#timepicker-input').timepicker();
    jQuery('#timepicker-input-fin').timepicker();
})

function ObtenerFechaActual() {
    // Obtén la fecha actual
    var fechaActual = new Date();

    // Obtiene el año, mes y día
    var year = fechaActual.getFullYear();
    var month = ('0' + (fechaActual.getMonth() + 1)).slice(-2); // Los meses van de 0 a 11, así que se suma 1
    var day = ('0' + fechaActual.getDate()).slice(-2);

    // Formatea la fecha como YYYY-MM-DD
    var fechaFormateada = year + '-' + month + '-' + day;

    // Almacena la fecha formateada en la variable fechaSeleccionada
    fechaSeleccionada = fechaFormateada;

}



function cargarNivelesSegunTipoRecurso() {
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;

    // Restablecer el select de Categoria Parqueadero
    document.getElementById('categoriaDropdownItems').selectedIndex =0;
    

    // Restablecer el select de Piso
    var nivel =document.getElementById('nivelDropdownItems');
    //Limpiar el drop
    nivel.innerHTML = "";
    valorDefecto(nivel, "nivel");

    // Restablecer el select de Salas
    var recurso = document.getElementById('recursosDropdownItems');
    recurso.innerHTML = "";
    valorDefecto(recurso, "recurso");

    // Dejar vacío el campo de entrada de capacidad de la sala
    document.getElementById('capacidadSala').value = '';

    var pdfPath = '/images/niveles/' + "" + '.pdf';
    document.getElementById('pdfViewer').src = pdfPath;




    // Obtener el contenedor del bloque Categoria Parqueadero
    var categoriaParqueaderoContainer = document.getElementById('categoriaParqueaderoContainer');
    var capacidadSalaContainer = document.getElementById('capacidadSalaContainer');
    var placaContainer = document.getElementById('placaContainer');

    if (tipoRecursoId === "1") {
        // Mostrar el bloque Categoria Parqueadero
        categoriaParqueaderoContainer.style.display = 'block';
        placaContainer.style.display = 'block';
        capacidadSalaContainer.style.display = 'none';

    } else if (tipoRecursoId === "3") {
        // Ocultar el bloque Categoria Parqueadero
        categoriaParqueaderoContainer.style.display = 'none';
        placaContainer.style.display = 'none';
        capacidadSalaContainer.style.display = 'block';
    } else {
        categoriaParqueaderoContainer.style.display = 'none';
        placaContainer.style.display = 'none';
        capacidadSalaContainer.style.display = 'none';
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
            nivelDropdown.innerHTML = ""; // Limpiar el select antes de agregar opciones


            // Agregar la opción predeterminada
            //var defaultOption = document.createElement('option');
            //defaultOption.value = "";
            //defaultOption.text = "--Elija un nivel--";
            //nivelDropdown.appendChild(defaultOption);
            valorDefecto(nivelDropdown, "nivel");

            // Llenar el select con los datos de d.parqueaderos
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
function valorDefecto(nivelDropdown,tipo) {
    var defaultOption = document.createElement('option');
    defaultOption.value = "";
    //defaultOption.text = "--Elija un nivel--";
    defaultOption.text = `--Elija un ${tipo}--`;
    nivelDropdown.appendChild(defaultOption);
}




function cargarRecursos() {
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    var nivelLista = document.getElementById('nivelDropdownItems');
    var nivelId = document.getElementById('nivelDropdownItems').value;
    var textoNivel = nivelLista.options[nivelLista.selectedIndex].text;

    var pdfPath = '/images/niveles/' + textoNivel + '.pdf';
    document.getElementById('pdfViewer').src = pdfPath;
    if (tipoRecursoId == 1 && nivelId != "") {
        var categoriaId = document.getElementById('categoriaDropdownItems').value;
        if (nivelId != "") {

            //var imagePath = '/images/niveles/' + textoSotano + '.png';
            //document.getElementById('imagenNivel').src = imagePath;
            //var pdfPath = '/images/niveles/' + textoNivel + '.pdf';
           

            var data = new Object()
            data.idCategoria = categoriaId;
            data.idSotano = nivelId;

            ajax.get("/Recursos/ObtenerParqueaderos", data).then((d) => {
                procesarRespuestaListaParqueaderos(d);
            });

        }
    } else if (tipoRecursoId != 1 && nivelId != "") {
            //var pdfPath = '/images/niveles/' + textoNivel + '.pdf';
           /* document.getElementById('pdfViewer').src = pdfPath;*/

            var data = new Object()
                    data.idTipoRecurso = tipoRecursoId;
                    data.idNivel = nivelId;

            ajax.get("/Recursos/ObtenerRecursos", data).then((d) => {
                procesarRespuestaListaRecursos(d);
            });

    }
}

function procesarRespuestaListaParqueaderos(d) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var recursoDropdown = document.getElementById('recursosDropdownItems');
            recursoDropdown.innerHTML = ""; // Limpiar el select antes de agregar opciones

            // Agregar la opción predeterminada
            //var defaultOption = document.createElement('option');
            //defaultOption.value = "";
            //defaultOption.text = "--Elija un recurso--";
            //recursoDropdown.appendChild(defaultOption);
            valorDefecto(recursoDropdown, "recurso");

            // Llenar el select con los datos de d.parqueaderos
            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_nombre;
                recursoDropdown.appendChild(option);
            });
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}

function procesarRespuestaListaRecursos(d) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var recursoDropdown = document.getElementById('recursosDropdownItems');
            recursoDropdown.innerHTML = ""; // Limpiar el select antes de agregar opciones

            // Agregar la opción predeterminada
            //var defaultOption = document.createElement('option');
            //defaultOption.value = "";
            //defaultOption.text = "--Elija un recurso--";
            //recursoDropdown.appendChild(defaultOption);
            valorDefecto(recursoDropdown, "recurso");


            // Llenar el select con los datos de d
            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_nombre;
                recursoDropdown.appendChild(option);
            });

            // Agregar evento al cambio de selección en el dropdown de salas
            recursoDropdown.addEventListener('change', function () {
                var selectedIndex = recursoDropdown.selectedIndex;

                // Verificar si se seleccionó una sala válida (no "--Elija una sala--")
                if (selectedIndex > 0) {
                    mostrarCapacidadSala(lista[selectedIndex - 1].n_capacidad);
                } else {
                    // Si no se selecciona una sala válida, puedes manejarlo según tus necesidades.
                    // Por ejemplo, puedes limpiar el campo de capacidad o mostrar un mensaje.
                    mostrarCapacidadSala(""); // Limpiar el campo de capacidad
                }
            });
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}

function mostrarCapacidadSala(capacidad) {
    var capacidadSala = document.getElementById('capacidadSala');
    capacidadSala.value = capacidad + " Personas";
}


//funcion para crear reserva en BD
$("#okk-button").unbind().click(realizarReserva);

function realizarReserva() {
    var tipoRecursoId = document.getElementById('recursoDropdownItems').value;
    var recursoId = document.getElementById('recursosDropdownItems').value;
    var horaInicio = $("#timepicker-input").val().trim();
    var horaFin = $("#timepicker-input-fin").val().trim();
    var horaInicioFormateada = convertirHoraFormato(horaInicio);
    var horaFinFormateada = convertirHoraFormato(horaFin);
    var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicioFormateada;
    var fecha_hora_fin = fechaSeleccionada + "T" + horaFinFormateada;


    if (tipoRecursoId == "1") {
        var placa = document.getElementById('plaque').value;
        var data = new Object()
        data.n_id_recurso = recursoId;
        data.d_fecha_hora_inicio = fecha_hora_inicio;
        data.d_fecha_hora_fin = fecha_hora_fin;
        data.v_placa_vehiculo = placa;

        ajax.post("/Recursos/CrearReservaParqueadero", data).then((d) => {
            procesarRespuestaCreacionReserva(d);
        });

    } else {
        var data = new Object()
        data.n_id_recurso = recursoId;
        data.d_fecha_hora_inicio = fecha_hora_inicio;
        data.d_fecha_hora_fin = fecha_hora_fin;


        ajax.post("/Recursos/CrearReservaRecurso", data).then((d) => {
            procesarRespuestaCreacionReserva(d);
        });
    } 
}
function procesarRespuestaCreacionReserva(d) {
    if (d.success) {
        msg.msgSuccess(d.message).then((result) => {
            window.location.reload();
        });
    } else {
        if (!d.messages)
            msg.msgError(d.message);

        else
            msg.msgError(d.messages.join('<br>'));
    }
}


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



(function ($) {

    "use strict";

    // Setup the calendar with the current date
    $(document).ready(function () {
        ObtenerFechaActual();
        var date = new Date();
        var today = date.getDate();
        // Set click handlers for DOM elements
        $(".right-button").click({ date: date }, next_year);
        $(".left-button").click({ date: date }, prev_year);
        $(".month").click({ date: date }, month_click);
        //  $("#add-button").click({ date: date }, new_event);
        // Set current month as active
        $(".months-row").children().eq(date.getMonth()).addClass("active-month");
        init_calendar(date);
        var events = check_events(today, date.getMonth() + 1, date.getFullYear());
        show_events(events, months[date.getMonth()], today);
    });

    // Initialize the calendar by appending the HTML dates
    function init_calendar(date) {
        $(".tbody").empty();
        $(".events-container").empty();
        var calendar_days = $(".tbody");
        var month = date.getMonth();
        var year = date.getFullYear();
        var day_plaque = days_in_month(month, year);
        var row = $("<tr class='table-row'></tr>");
        var today = date.getDate();
        // Set date to 1 to find the first day of the month
        date.setDate(1);
        var first_day = date.getDay();
        // 35+firstDay is the number of date elements to be added to the dates table
        // 35 is from (7 days in a week) * (up to 5 rows of dates in a month)
        for (var i = 0; i < 35 + first_day; i++) {
            // Since some of the elements will be blank,
            // need to calculate actual date from index
            var day = i - first_day + 1;
            // If it is a sunday, make a new row
            if (i % 7 === 0) {
                calendar_days.append(row);
                row = $("<tr class='table-row'></tr>");
            }
            // if current index isn't a day in this month, make it blank
            if (i < first_day || day > day_plaque) {
                var curr_date = $("<td class='table-date nil'>" + "</td>");
                row.append(curr_date);
            }
            else {
                var curr_date = $("<td class='table-date'>" + day + "</td>");
                var events = check_events(day, month + 1, year);
                if (today === day && $(".active-date").length === 0) {
                    curr_date.addClass("active-date");
                    show_events(events, months[month], day);
                }
                // If this date has any events, style it with .event-date
                if (events.length !== 0) {
                    curr_date.addClass("event-date");
                }
                // Set onClick handler for clicking a date
                curr_date.click({ events: events, month: months[month], mes: month + 1, day: day, year: year }, date_click);
                row.append(curr_date);
            }
        }
        // Append the last row and set the current year
        calendar_days.append(row);
        $(".year").text(year);
    }

    // Get the number of days in a given month/year
    function days_in_month(month, year) {
        var monthStart = new Date(year, month, 1);
        var monthEnd = new Date(year, month + 1, 1);
        return (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
    }

    // Event handler for when a date is clicked
    function date_click(event) {
        $(".events-container").show(250);
        $("#dialog").hide(250);
        $(".active-date").removeClass("active-date");
        $(this).addClass("active-date");
        fechaSeleccionada = event.data.year + "-" + event.data.mes + "-" + event.data.day;
        show_events(event.data.events, event.data.month, event.data.day);
    };

    // Event handler for when a month is clicked
    function month_click(event) {
        $(".events-container").show(250);
        $("#dialog").hide(250);
        var date = event.data.date;
        $(".active-month").removeClass("active-month");
        $(this).addClass("active-month");
        var new_month = $(".month").index(this);
        date.setMonth(new_month);
        init_calendar(date);
    }

    // Event handler for when the year right-button is clicked
    function next_year(event) {
        $("#dialog").hide(250);
        var date = event.data.date;
        var new_year = date.getFullYear() + 1;
        $("year").html(new_year);
        date.setFullYear(new_year);
        init_calendar(date);
    }

    // Event handler for when the year left-button is clicked
    function prev_year(event) {
        $("#dialog").hide(250);
        var date = event.data.date;
        var new_year = date.getFullYear() - 1;
        $("year").html(new_year);
        date.setFullYear(new_year);
        init_calendar(date);
    }

    // Event handler for clicking the new event button
    /*  function new_event(event) {
          // if a date isn't selected then do nothing
          if ($(".active-date").length === 0)
              return;
          // remove red error input on click
          $("input").click(function () {
              $(this).removeClass("error-input");
          })
          // empty inputs and hide events
          $("#dialog input[type=text]").val('');
          $("#dialog input[type=text]").val('');
          $("#dialog input[type=text]").val('');
          $("#dialog input[type=text]").val('');
          $(".events-container").hide(250);
          $("#dialog").show(250);
          // Event handler for cancel button
          $("#cancel-button").click(function () {
              $("#name").removeClass("error-input");
              $("#plaque").removeClass("error-input");
              $("#dialog").hide(250);
              $(".events-container").show(250);
          });
  
  
          // Event handler for ok button
          $("#ok-button").unbind().click({ date: event.data.date }, function () {
              var date = event.data.date;
              var name = $("#name").val().trim();
              var plaque = $("#plaque").val().trim();
              var jornadaId = $("#jornadaDropdownItems").val().trim();
              var day = parseInt($(".active-date").html());
              // Basic form validation
              if (name.length === 0) {
                  $("#name").addClass("error-input");
              }
              else if (plaque.length === 0) {
                  $("#plaque").addClass("error-input");
              } else if (jornadaId === "") {
                  $("#timepicker-input").addClass("error-input");
              }
              else {
                  $("#dialog").hide(250);
                  console.log(date);
                  new_event_json(name, plaque, jornadaId, date, day);
                  date.setDate(day);
                  init_calendar(date);
  
                  msg.msgSuccess("Reserva de parqueadero exitosa");
  
                  //limpiar el formulario
                  var formulario = $('#form');
                  formulario[0].reset();
              }
          });
      }*/

    // Adds a json event to event_data
    function new_event_json(name, plaque, jornadaID, date, day) {
        console.log(jornadaID)
        var event = {
            "nombre": name,
            "placa": plaque,
            "jornada": jornadaID,
            "year": date.getFullYear(),
            "month": date.getMonth() + 1,
            "day": day
        };
        console.log(event)
        event_data["events"].push(event);
    }

    // Display all events of the selected date in card views
    function show_events(events, month, day) {
        // Clear the dates container
        $(".events-container").empty();
        $(".events-container").show(250);
        console.log(event_data["events"]);
        // If there are no events for this date, notify the user
        if (events.length === 0) {
            var event_card = $("<div class='event-card'></div>");
            var event_name = $("<div class='event-name'>No hay reservas para el dia " + month + " " + day + ".</div>");
            $(event_card).css({ "border-left": "10px solid #FF1744" });
            $(event_card).append(event_name);
            $(".events-container").append(event_card);
        }
        else {
            // Go through and add each event as a card to the events container
            for (var i = 0; i < events.length; i++) {
                var event_card = $("<div class='event-card'></div>");
                var event_tittle = $("<div class='event-name justify-content-center'>Reservas del dia " + month + " " + day + "</div>");
                var event_name = $("<div class='event-name'>" + "Nombre: " + events[i]["nombre"] + "</div>");
                var event_plaque = $("<div class='event-plaque'>" + "Placa: " + events[i]["placa"] + " </div>");
                var event_jornada = $("<div class='event-inicio'>" + "Jornada: " + events[i]["jornada"] + " </div>");
                if (events[i]["cancelled"] === true) {
                    $(event_card).css({
                        "border-left": "10px solid #FF1744"
                    });
                    event_plaque = $("<div class='event-cancelled'>Cancelled</div>");
                }
                $(event_card).append(event_tittle);
                $(event_card).append(event_name).append(event_plaque);
                $(event_card).append(event_jornada)
                $(".events-container").append(event_card);
            }
        }
    }

    // Checks if a specific date has any events
    function check_events(day, month, year) {
        var events = [];
        for (var i = 0; i < event_data["events"].length; i++) {
            var event = event_data["events"][i];
            if (event["day"] === day &&
                event["month"] === month &&
                event["year"] === year) {
                events.push(event);
            }
        }
        return events;
    }

    // Given data for events in JSON format
    var event_data = {
        "events": [

        ]
    };

    const months = [
        "Enero",
        "Febrero",
        "Marzo",
        "Abril",
        "Mayo",
        "Junio",
        "Julio",
        "Agosto",
        "Septiembre",
        "Octubre",
        "Noviembre",
        "Diciembre"
    ];

})(jQuery);



window.cargarNivelesSegunTipoRecurso = cargarNivelesSegunTipoRecurso;
window.cargarRecursos = cargarRecursos;