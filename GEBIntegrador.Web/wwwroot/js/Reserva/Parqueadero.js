import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../shared/mensajes.js"


var msg = new Mensajes();
let ajax = new ajaxService();


var fechaSeleccionada;


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






//funcion para crear reserva en BD
$("#okk-button").unbind().click(realizarReserva);
function realizarReserva() {
    var parqueaderoId = document.getElementById('parqueaderoDropdownItems').value;
    var jornadaElegida = document.getElementById('jornadaDropdownItems');
    var placa = document.getElementById('plaque').value;

    var jornada = jornadaElegida.options[jornadaElegida.selectedIndex].value;



    jornada = jornada.split("-");
    var horaInicio = jornada[0];
    var horaFin = jornada[1];
    

    var fecha_hora_inicio = fechaSeleccionada + "T" + horaInicio;
    var fecha_hora_fin = fechaSeleccionada + "T" + horaFin;

    var data = new Object()
    data.n_id_recurso = parqueaderoId;
    data.d_fecha_hora_inicio = fecha_hora_inicio;
    data.d_fecha_hora_fin = fecha_hora_fin;
    data.v_placa_vehiculo = placa;
           

    ajax.post("/Recursos/CrearReserva", data).then((d) => {
        procesarRespuestaCreacionReserva(d);
    });
 
}
function procesarRespuestaCreacionReserva(d) {
    if (d.success) {
        msg.msgSuccess(d.message).then((result) => {
            msg.msgSuccess("Reserva de parqueadero exitosa");
            window.location.reload();
        });
    } else {
        if (!d.messages) 
            msg.msgError(d.message);
        
        else
            msg.msgError(d.messages.join('<br>'));
    }
}








function seleccionarSotano() {
    var sotanosLista = document.getElementById('sotanoDropdownItems');
    var categoriaId = document.getElementById('categoriaDropdownItems').value;
    var sotanoId = document.getElementById('sotanoDropdownItems').value;
    var textoSotano = sotanosLista.options[sotanosLista.selectedIndex].text;

    if (categoriaId != "" && sotanoId != "") {

        //var imagePath = '/images/niveles/' + textoSotano + '.png';
        //document.getElementById('imagenNivel').src = imagePath;
        var pdfPath = '/images/niveles/' + textoSotano + '.pdf';
        document.getElementById('pdfViewer').src = pdfPath;

        var data = new Object()
        data.idCategoria = categoriaId;
        data.idSotano = sotanoId;

        ajax.get("/Recursos/ObtenerParqueaderos", data).then((d) => {
            procesarRespuestaParqueaderos(d);
        });   
    }
}

function procesarRespuestaParqueaderos(d) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var parqueaderoDropdown = document.getElementById('parqueaderoDropdownItems');
            parqueaderoDropdown.innerHTML = ""; // Limpiar el select antes de agregar opciones

            // Agregar la opción predeterminada
            var defaultOption = document.createElement('option');
            defaultOption.value = "";
            defaultOption.text = "--Elija un parqueadero--";
            parqueaderoDropdown.appendChild(defaultOption);

            // Llenar el select con los datos de d.parqueaderos
            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_nombre;
                parqueaderoDropdown.appendChild(option);
            });
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}




function procesarRespuestaReservas(d) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var parqueaderoDropdown = document.getElementById('parqueaderoDropdownItems');
            parqueaderoDropdown.innerHTML = ""; // Limpiar el select antes de agregar opciones

            // Llenar el select con los datos de d.parqueaderos
            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_nombre;
                parqueaderoDropdown.appendChild(option);
            });
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }
}





//<!--logica del calendar-- >

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


            
////<!--logica del calendar para usar hora inicio y fin en el formulario-- >
 
//        (function ($) {

//            "use strict";

//        // Setup the calendar with the current date
//        $(document).ready(function () {
//            var date = new Date();
//        var today = date.getDate();
//        // Set click handlers for DOM elements
//        $(".right-button").click({date: date }, next_year);
//        $(".left-button").click({date: date }, prev_year);
//        $(".month").click({date: date }, month_click);
//        $("#add-button").click({date: date }, new_event);
//        // Set current month as active
//        $(".months-row").children().eq(date.getMonth()).addClass("active-month");
//        init_calendar(date);
//        var events = check_events(today, date.getMonth() + 1, date.getFullYear());
//        show_events(events, months[date.getMonth()], today);
//        });

//        // Initialize the calendar by appending the HTML dates
//        function init_calendar(date) {
//            $(".tbody").empty();
//        $(".events-container").empty();
//        var calendar_days = $(".tbody");
//        var month = date.getMonth();
//        var year = date.getFullYear();
//        var day_plaque = days_in_month(month, year);
//        var row = $("<tr class='table-row'></tr>");
//        var today = date.getDate();
//        // Set date to 1 to find the first day of the month
//        date.setDate(1);
//        var first_day = date.getDay();
//        // 35+firstDay is the number of date elements to be added to the dates table
//        // 35 is from (7 days in a week) * (up to 5 rows of dates in a month)
//        for (var i = 0; i < 35 + first_day; i++) {
//                // Since some of the elements will be blank,
//                // need to calculate actual date from index
//                var day = i - first_day + 1;
//        // If it is a sunday, make a new row
//        if (i % 7 === 0) {
//            calendar_days.append(row);
//        row = $("<tr class='table-row'></tr>");
//                }
//        // if current index isn't a day in this month, make it blank
//        if (i < first_day || day > day_plaque) {
//                    var curr_date = $("<td class='table-date nil'>" + "</td>");
//        row.append(curr_date);
//                }
//        else {
//                    var curr_date = $("<td class='table-date'>" + day + "</td>");
//        var events = check_events(day, month + 1, year);
//        if (today === day && $(".active-date").length === 0) {
//            curr_date.addClass("active-date");
//        show_events(events, months[month], day);
//                    }
//        // If this date has any events, style it with .event-date
//        if (events.length !== 0) {
//            curr_date.addClass("event-date");
//                    }
//        // Set onClick handler for clicking a date
//        curr_date.click({events: events, month: months[month], day: day }, date_click);
//        row.append(curr_date);
//                }
//            }
//        // Append the last row and set the current year
//        calendar_days.append(row);
//        $(".year").text(year);
//        }

//        // Get the number of days in a given month/year
//        function days_in_month(month, year) {
//            var monthStart = new Date(year, month, 1);
//        var monthEnd = new Date(year, month + 1, 1);
//        return (monthEnd - monthStart) / (1000 * 60 * 60 * 24);
//        }

//        // Event handler for when a date is clicked
//        function date_click(event) {
//            $(".events-container").show(250);
//        $("#dialog").hide(250);
//        $(".active-date").removeClass("active-date");
//        $(this).addClass("active-date");
//        show_events(event.data.events, event.data.month, event.data.day);
//        };

//        // Event handler for when a month is clicked
//        function month_click(event) {
//            $(".events-container").show(250);
//        $("#dialog").hide(250);
//        var date = event.data.date;
//        $(".active-month").removeClass("active-month");
//        $(this).addClass("active-month");
//        var new_month = $(".month").index(this);
//        date.setMonth(new_month);
//        init_calendar(date);
//        }

//        // Event handler for when the year right-button is clicked
//        function next_year(event) {
//            $("#dialog").hide(250);
//        var date = event.data.date;
//        var new_year = date.getFullYear() + 1;
//        $("year").html(new_year);
//        date.setFullYear(new_year);
//        init_calendar(date);
//        }

//        // Event handler for when the year left-button is clicked
//        function prev_year(event) {
//            $("#dialog").hide(250);
//        var date = event.data.date;
//        var new_year = date.getFullYear() - 1;
//        $("year").html(new_year);
//        date.setFullYear(new_year);
//        init_calendar(date);
//        }

//        // Event handler for clicking the new event button
//        function new_event(event) {
//            // if a date isn't selected then do nothing
//            if ($(".active-date").length === 0)
//        return;
//        // remove red error input on click
//        $("input").click(function () {
//            $(this).removeClass("error-input");
//            })
//        // empty inputs and hide events
//        $("#dialog input[type=text]").val('');
//        $("#dialog input[type=text]").val('');
//        $("#dialog input[type=text]").val('');
//        $("#dialog input[type=text]").val('');
//        $(".events-container").hide(250);
//        $("#dialog").show(250);
//        // Event handler for cancel button
//        $("#cancel-button").click(function () {
//            $("#name").removeClass("error-input");
//        $("#plaque").removeClass("error-input");
//        $("#dialog").hide(250);
//        $(".events-container").show(250);
//            });
//        // Event handler for ok button
//        $("#ok-button").unbind().click({date: event.data.date }, function () {
//                var date = event.data.date;
//        var name = $("#name").val().trim();
//        var plaque = $("#plaque").val().trim();
//        var horaInicio = $("#timepicker-input").val().trim();
//        var horaFin = $("#timepicker-input-fin").val().trim();
//        var day = parseInt($(".active-date").html());
//        // Basic form validation
//        if (name.length === 0) {
//            $("#name").addClass("error-input");
//                }
//        else if (plaque.length === 0) {
//            $("#plaque").addClass("error-input");
//                } else if (horaInicio.length === 0) {
//            $("#timepicker-input").addClass("error-input");
//                } else if (horaFin.length === 0) {
//            $("#timepicker-input-fin").addClass("error-input");
//                }
//        else {
//            $("#dialog").hide(250);
//        console.log("new event");
//        new_event_json(name, plaque, horaInicio, horaFin, date, day);
//        date.setDate(day);
//        init_calendar(date);

//            msg.msgSuccess("Reserva de parqueadero exitosa");

//        //limpiar el formulario
//        var formulario = $('#form');
//        formulario[0].reset();
//                }
//            });
//        }

//        // Adds a json event to event_data
//        function new_event_json(name, plaque, horaInicio, horaFin, date, day) {
//            var event = {
//            "nombre": name,
//        "placa": plaque,
//        "horaInicio": horaInicio,
//        "horaFin": horaFin,
//        "year": date.getFullYear(),
//        "month": date.getMonth() + 1,
//        "day": day
//            };
//        event_data["events"].push(event);
//        }

//        // Display all events of the selected date in card views
//        function show_events(events, month, day) {
//            // Clear the dates container
//            $(".events-container").empty();
//        $(".events-container").show(250);
//        console.log(event_data["events"]);
//        // If there are no events for this date, notify the user
//        if (events.length === 0) {
//                var event_card = $("<div class='event-card'></div>");
//        var event_name = $("<div class='event-name'>No hay reservas para el dia " + month + " " + day + ".</div>");
//        $(event_card).css({"border-left": "10px solid #FF1744" });
//        $(event_card).append(event_name);
//        $(".events-container").append(event_card);
//            }
//        else {
//                // Go through and add each event as a card to the events container
//                for (var i = 0; i < events.length; i++) {
//                    var event_card = $("<div class='event-card'></div>");
//        var event_tittle = $("<div class='event-name justify-content-center'>Reservas del dia " + month + " " + day + "</div>");
//        var event_name = $("<div class='event-name'>" + "Nombre: " + events[i]["nombre"] + "</div>");
//        var event_plaque = $("<div class='event-plaque'>" + "Placa: " + events[i]["placa"] + " </div>");
//        var event_inicio = $("<div class='event-inicio'>" + "Hora inicio: " + events[i]["horaInicio"] + " </div>");
//        var event_fin = $("<div class='event-fin'>" + "Hora fin: " + events[i]["horaFin"] + " </div>");
//        if (events[i]["cancelled"] === true) {
//            $(event_card).css({
//                "border-left": "10px solid #FF1744"
//            });
//        event_plaque = $("<div class='event-cancelled'>Cancelled</div>");
//                    }
//        $(event_card).append(event_tittle);
//        $(event_card).append(event_name).append(event_plaque);
//        $(event_card).append(event_inicio).append(event_fin);
//        $(".events-container").append(event_card);
//                }
//            }
//        }

//        // Checks if a specific date has any events
//        function check_events(day, month, year) {
//            var events = [];
//        for (var i = 0; i < event_data["events"].length; i++) {
//                var event = event_data["events"][i];
//        if (event["day"] === day &&
//        event["month"] === month &&
//        event["year"] === year) {
//            events.push(event);
//                }
//            }
//        return events;
//        }

//        // Given data for events in JSON format
//        var event_data = {
//            "events": [

//        ]
//        };

//        const months = [
//        "Enero",
//        "Febrero",
//        "Marzo",
//        "Abril",
//        "Mayo",
//        "Junio",
//        "Julio",
//        "Agosto",
//        "Septiembre",
//        "Octubre",
//        "Noviembre",
//        "Diciembre"
//        ];

//    })(jQuery);



window.seleccionarSotano = seleccionarSotano;
window.realizarReserva = realizarReserva;