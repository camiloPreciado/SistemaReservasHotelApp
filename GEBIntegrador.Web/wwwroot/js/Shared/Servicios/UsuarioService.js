import ajaxService from "../AjaxService.js";

export default class UsuarioService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Usuarios/";
    }

    registrar(formData) {
        return new Promise((resolve) => {
            this.ajax.post(`${this.url}_RegistrarPartial`, formData).then((d) => {
                resolve(d);
            }); 
        });
    }
    guardar(formData) {
        return new Promise((resolve) => {
            this.ajax.post(`${this.url}_EditPartial`, formData).then((d) => {
                resolve(d);
            }); 
        });
    }
    eliminar(formData) {
        return new Promise((resolve) => {
            this.ajax.post(`${this.url}DeleteConfirmed`, formData).then((d) => {
                resolve(d);
            });
        });
    }
}