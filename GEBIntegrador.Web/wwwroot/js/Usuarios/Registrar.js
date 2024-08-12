import UsuarioService from "../Shared/Servicios/UsuarioService.js";
import PersonaService from "../Shared/Servicios/PersonaService.js";
import Mensajes from "../Shared/Mensajes.js";
import FormValidator from "../Shared/FormValidator.js";
import persona from "../Shared/Modelos/Persona.js";


let msg = new Mensajes();
//let ajax = new ajaxService();
let FormV = new FormValidator();
let usuarioServicio = new UsuarioService();
let personaServicio = new PersonaService();

$(document).ready(function () {

    FormV.addValidationAlfanumeric(); // Revisar como es por que no recuerdo

    var modelo = new Object();

    //$('#form').validate({
    //    rules: {
    //        'persona.v_documento': {
    //            required: true,
    //            numeric: true
    //        }
    //    },
    //    messages: {
    //        'persona.v_documento': {
    //            required: "Por favor ingrese solo numeros",
    //            numeric: "fff"
    //        }
    //    }
    //});

    //$('#form').validate({
    //    rules: {
    //        v_documento: {
    //            required: true,
    //            alfanumeric: ''
    //        }
    //    },
    //    messages: {
    //        v_documento: {
    //            required: "Por favor ingrese solo numeros",
    //            alfanumeric: "Por favor ingrese solo datos numeros"
    //        }
    //    }
    //});

    $('#button_cedula').on('click', function () {
        var data = new Object();
        var cc = $('#v_documento').val();
        if (cc != '') {
            data.cc = cc;            
            personaServicio.getPersona(data).then((response) => {
                procesarPersona(response);
            });
        } else {
            var v_nombre = $('#v_nombres');
            var v_apellidos = $('#v_apellidos');
            msg.msgError('Por favor, ingrese la identificación');
            v_nombre.val('');
            v_apellidos.attr("disabled", "disabled");
        }
        $('#btn_save').removeAttr("disabled");
    });
    $('#button_limpiar').on('click', function () {

        $('#form')[0].reset();
        $('#v_nombres').attr("disabled", "disabled");
        $('#v_apellidos').attr("disabled", "disabled");
        $('#v_documento').removeAttr("disabled");
        $('#btn_save').attr("disabled", "disabled");
    });

    $('#form').on('submit', function (e) {
        e.preventDefault();

        if ($('#form').valid()) {
            var formData = $(this).serializeArray();

            formData = FormV.getFormValues(formData);

            var Data = {...modelo , ...formData};

            //ajax.post('/Usuarios/_RegistrarPartial', formData).then((d) => {
            usuarioServicio.registrar(Data).then((response) => {
                procesarRespuesta(response);
            }); 
        }
    });

    $('#v_documento').on('input', function () {
        var resp = $(this).valid();
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
    function procesarPersona(response) {
        var v_nombre = $('#v_nombres');
        var v_apellidos = $('#v_apellidos');
        var v_documento = $('#v_documento');
        if (response.success) {
            if (response.data != null) {
                v_nombre.val(response.data.v_nombres);
                v_nombre.attr("disabled", "disabled");
                v_apellidos.val(response.data.v_apellidos);
                v_apellidos.attr("disabled", "disabled");
                v_documento.attr("disabled", "disabled");
                modelo.persona = response.data;
            } else {
                var dataPerson = new persona({ n_id: 0, v_documento: v_documento.val(), v_nombre: "" });
                modelo.persona = dataPerson;
                v_documento.attr("disabled", "disabled");
                v_nombre.val('');
                v_nombre.removeAttr("disabled").focus();
                v_apellidos.val('');
                v_apellidos.removeAttr("disabled");
                msg.msgError("El documento no se encuentra registrado en sistema, debe ingresar los datos.");
            }
        }
        v_documento.attr("disabled", "disabled");
    }
});
