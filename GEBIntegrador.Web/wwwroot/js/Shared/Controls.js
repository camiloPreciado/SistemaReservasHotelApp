export default class Controles{

    crearChecks(ForId, label, inputAttri) {
        // Crear el contenedor div
        var divContainer = $("<div></div>").addClass("col-md-4 col-sm-6 mb-2");

        // Crear la etiqueta del interruptor
        var labelSwitch = $("<label></label>")
            .addClass("form-check-label")
            .attr("for", ForId)
            .text(label);

        // Crear el interruptor (checkbox)
        var inputSwitch = $("<input>")
            .addClass("form-check-input")
            .attr(inputAttri);

        // Crear el contenedor del interruptor (form-switch)
        var divSwitch = $("<div></div>").addClass("form-check form-switch");

        // Agregar el interruptor y la etiqueta al contenedor del interruptor
        divSwitch.append(inputSwitch, labelSwitch);

        // Agregar el contenedor del interruptor al contenedor principal
        divContainer.append(divSwitch);

        // Devolver el elemento creado
        return divContainer;
    }
    crearLoader(ConTexto) {
        // Crear el contenedor div
        var divContainer = $("<div></div>").addClass("d-flex align-items-center");

        // Crear la etiqueta del text
        var text = $("<strong></strong>")
            .addClass("text-success")
            .text("Cargando...");

        // Crear el interruptor (checkbox)
        var divSpinner = $("<div></div>")
            .addClass("spinner-border text-success ms-auto")
            .attr({ "role": "status", "aria-hidden":"true"});
        if (ConTexto)
            divContainer.append(text);

        // Agregar el texto y el spinner
        divContainer.append(divSpinner);

        // Devolver el elemento creado
        return divContainer;
    }

}
