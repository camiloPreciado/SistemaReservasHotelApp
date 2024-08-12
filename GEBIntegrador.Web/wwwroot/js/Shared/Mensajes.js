export default class Mensajes{

    dialog = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success m-2',
            cancelButton: 'btn btn-danger m-2'
        },
        buttonsStyling: false
    })

    msgConfirmacion(titulo, texto) {

        return new Promise((response) => {
            this.dialog.fire({
                title: titulo,
                text: texto,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Confirmar!',
                cancelButtonText: 'Cancelar!',
                reverseButtons: true
            }).then((result) => {
                response(result);
            })
        });
    }

    msgConfirmacionInput({ titulo, errorTexto, textButton, consecutivo, placeholderCons }) {

        var inputs = '';
        if (!!consecutivo) {
            inputs = `<input type="text" id="v_consecutivo" class="form-control mb-2" placeholder="${placeholderCons}" />
                    <textarea class="form-control mb-2" id="v_observaciones" placeholder="Ingrese las observaciones..." ></textarea>`;

        } else {
            inputs = `
                    <textarea class="form-control mb-2" id="v_observaciones" placeholder="Ingrese las observaciones..." ></textarea>`;
        }

        var funcion = () => {
            var consInput;
            var v_consecutivo;

            if (!!consecutivo)
                consInput = document.getElementById('v_consecutivo');

            const textInput = document.getElementById('v_observaciones');

            if (!!consecutivo)
                v_consecutivo = consInput.value;

            const v_observaciones = textInput.value;

            if (!!consecutivo) {
                if (v_observaciones === '' || v_consecutivo === '') {
                    Swal.showValidationMessage(errorTexto);
                }

                return { v_observaciones, v_consecutivo };

            } else {
                if (v_observaciones === '') {
                    Swal.showValidationMessage(errorTexto);
                }

                return { v_observaciones };
            }

        }

        return new Promise((response) => {

            this.dialog.fire({
                title: titulo,//titulo
                html: inputs,
                inputAttributes: {
                    autocapitalize: "off"
                },
                showCancelButton: true,
                confirmButtonText: textButton,
                cancelButtonText: "Cancelar",
                showLoaderOnConfirm: true,
                preConfirm: funcion,
            }).then((result) => {
                if (result.isConfirmed) {
                    if (result.value) {
                        response(result.value);
                    }
                }
            });
        });
    }

    msgConfirmacionInputAndFile({ titulo, errorTexto, textButton, typefile, tipomsg }) {

        var inputs = '';
        if (!!tipomsg) {
            inputs = `<input type="text" id="v_consecutivo" class="form-control mb-2" placeholder="Ingrese el consecutivo..." />
                    <textarea class="form-control mb-2" id="v_observaciones" placeholder="Ingrese las observaciones..." ></textarea>
                    <input type="file" id="f_archivo" accept = "${typefile}" class="form-control mb-2" placeholder="Ingrese archivo PTAM definitivo..." />`;

        } else {
            inputs = `
                    <textarea class="form-control mb-2" id="v_observaciones" placeholder="Ingrese las observaciones..." ></textarea>
                    <input type="file" id="f_archivo" accept = "${typefile}" class="form-control mb-2" />`;
        }

        var funcion = () => {
            var consInput;
            var v_consecutivo;

            if (!!tipomsg)
                consInput = document.getElementById('v_consecutivo');

            const textInput = document.getElementById('v_observaciones');
            const fileInput = document.getElementById('f_archivo');

            if (!!tipomsg)
                v_consecutivo = consInput.value;

            const v_observaciones = textInput.value;
            const f_archivo = fileInput.files[0];

            if (!!tipomsg) {
                if (!f_archivo || v_observaciones === '' || v_consecutivo === '') {
                    Swal.showValidationMessage(errorTexto);
                }

                return { f_archivo, v_observaciones, v_consecutivo };

            } else {
                if (!f_archivo || v_observaciones === '') {
                    Swal.showValidationMessage(errorTexto);
                }

                return { f_archivo, v_observaciones };
            }

        }

        return new Promise((response) => {

            this.dialog.fire({
                title: titulo,//titulo
                html: inputs,
                inputAttributes: {
                    autocapitalize: "off"
                },
                showCancelButton: true,
                confirmButtonText: textButton,
                cancelButtonText: "Cancelar",
                showLoaderOnConfirm: true,
                preConfirm: funcion,
            }).then((result) => {
                if (result.isConfirmed) {
                    if (result.value) {
                        response(result.value);
                    }
                }
            });
        });
    }

    msgSuccess(titulo) {
        return new Promise((response) => {
            this.dialog.fire(titulo, '', 'success').then((result) => {
                response(result);
            });
        });
    }
    msgError(titulo) {
        return new Promise((response) => {
            this.dialog.fire(titulo, '', "error").then((result) => {
                response(result);
            });
        });
    }

}
