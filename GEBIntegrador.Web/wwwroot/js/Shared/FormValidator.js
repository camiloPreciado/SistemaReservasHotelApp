export default class FormValidator{

    constructor() {
        this.MAXIMO_TAMANIO_BYTES = 2000000; // 2MB = 2 millones de bytes
    }

    addValidationAlfanumeric() {
        $.validator.addMethod("alfanumeric", function (value, element) {
            value = value.replace(/[^ña-zA-ZÑ0-9]/g, '');
            $(element).val(value);
            return this.optional(element) || /^[ña-zA-ZÑ0-9]+$/.test(value);
        }, "Solo se permiten alfanumericos.");
    }
    addValidationAlfanumeric2() {
        $.validator.addMethod("alfanumeric2", function (value, element) {
            value = value.replace(/[^a-zA-Z0-9ñÑ.,á-úÁ-ÚÑñ\s]/g, '');
            $(element).val(value);
            return this.optional(element) || /^[a-zA-Z0-9ñÑ.,á-úÁ-ÚÑñ\s]+$/.test(value);
        }, "Solo se permiten alfanumericos.");
    }
    addValidationNumeric() {
        $.validator.addMethod("numeric", function (value, element) {
            value = value.replace(/[^0-9]/g, '');
            $(element).val(value);
            return this.optional(element) || /^[0-9]+$/.test(value);
        }, "Solo se permiten numericos.");
    }

    getFormValues(form) {
        const formData = form;
        const formObject = {};

        formData.forEach((value, key) => {
            formObject[value.name] = value.value;
        });
        return formObject;
    }

    validarTamanioArchivo(input) {
        var respuesta = '';

        const tamanoArchivo = input.files[0].size;

        if (tamanoArchivo > this.MAXIMO_TAMANIO_BYTES) {
            const tamanoPermitido = this.MAXIMO_TAMANIO_BYTES / 1000000;
            respuesta = `El archivo excede el tamaño limite ${tamanoPermitido}Mb`;
        }
        return respuesta;
    }
}
