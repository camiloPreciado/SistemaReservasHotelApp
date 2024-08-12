import ajaxService from "../Shared/AjaxService.js";
import Mensajes from "../Shared/Mensajes.js";
import FormValidator from "../Shared/FormValidator.js";


let msg = new Mensajes();
let ajax = new ajaxService();
let FormV = new FormValidator();

function valorDefecto(nivelDropdown, tipo) {
    var defaultOption = document.createElement('option');
    defaultOption.value = "";
    defaultOption.text = `--Elija un ${tipo}--`;
    nivelDropdown.appendChild(defaultOption);
}

function tipoRecursoElegido(n_id) {
    var tipoRecursoId = document.getElementById('n_tipo_recurso').value;

    //// Restablecer el select de Categoria Parqueadero
    //document.getElementById('n_categoria_parqueadero').selectedIndex = 0;


    //// Restablecer el select de Piso
    var nivel = document.getElementById('n_nivel_recurso');
    //Limpiar el drop
    nivel.innerHTML = "";
    valorDefecto(nivel, "nivel");

    //document.getElementById('v_nombre').value = '';


    //// Dejar vacío el campo de entrada de capacidad de la sala
    //document.getElementById('n_capacidad').value = '';



    // Obtener el contenedor del bloque Categoria Parqueadero
    var capacidadRecurso = document.getElementById('capacidadRecurso');
    var categoriaRecurso = document.getElementById('categoriaRecurso');
    

    if (tipoRecursoId === "1") {
        // Mostrar el bloque Categoria Parqueadero
        categoriaRecurso.style.display = 'block';        
        capacidadRecurso.style.display = 'none';

    } else if (tipoRecursoId === "3") {
        // Ocultar el bloque Categoria Parqueadero
        categoriaRecurso.style.display = 'none';        
        capacidadRecurso.style.display = 'block';
    } else {
        categoriaRecurso.style.display = 'none';
        capacidadRecurso.style.display = 'none';
    }
    if (tipoRecursoId != "") {

        var data = new Object()
        data.TipoRecurso = tipoRecursoId;


        ajax.get("/Recursos/ObtenerNiveles", data).then((d) => {
            procesarRespuestaNiveles(d, n_id);
        });
    }
}

function procesarRespuestaNiveles(d, n_id) {
    if (d.success) {
        if (d.data != null) {
            var lista = d.data;

            var nivelDropdown = document.getElementById('n_nivel_recurso');
            
            // Llenar el select
            lista.forEach(p => {
                var option = document.createElement('option');
                option.value = p.n_id;
                option.text = p.v_descripcion;
                nivelDropdown.appendChild(option);

            });
            if (n_id) {
                nivelDropdown.value = n_id;
            }
        }
    } else {
        if (!d.messages)
            msg.msgError(d.message);
        else
            msg.msgError(d.messages.join('<br>'));
    }

    
}

$(document).ready(function () {
    var modelo = $("#form").data("model");
    if (modelo) {
        document.getElementById('n_tipo_recurso').selectedIndex = modelo.n_tipo_recurso;
        tipoRecursoElegido(modelo.n_nivel_recurso);
        document.getElementById('v_nombre').value = modelo.v_nombre;
        document.getElementById('n_categoria_parqueadero').selectedIndex = modelo.n_categoria_parqueadero;
        document.getElementById('n_capacidad').value = modelo.n_capacidad;
        
    }


    $('#form').on('submit', function (e) {
        e.preventDefault();

        if ($('#form').valid()) {
            var formData = $(this).serializeArray();

            var formData = FormV.getFormValues(formData);

            var Data = { ...modelo, ...formData };

            //metodo post en el controller
            ajax.post('/Recursos/EditarRecurso', Data).then((d) => {
                procesarRespuesta(d);
            });
        }
    });
});
function procesarRespuesta(d) {
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

window.tipoRecursoElegido = tipoRecursoElegido;