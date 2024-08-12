import ajaxService from "../AjaxService.js";

export default class SolicitudService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Solicitud/";
    }

    registrar(formData) {
        return new Promise((resolve) => {
            this.ajax.postWithFile(`${this.url}IngresoContratista`, formData).then((d) => {
                resolve(d);
            }); 
        });
    }
    registrarCol(formData) {
        return new Promise((resolve) => {
            this.ajax.postWithFile(`${this.url}IngresoColaborador`, formData).then((d) => {
                resolve(d);
            });
        });
    }
    responderSolicitud(formData) {
        return new Promise((resolve) => {
            this.ajax.post(`${this.url}ResponderSolicitud`, formData).then((d) => {
                resolve(d);
            });
        });
    }
    responderSolicitudConFile(formData) {
        return new Promise((resolve) => {
            this.ajax.postWithFile(`${this.url}ResponderSolicitud`, formData).then((d) => {
                resolve(d);
            });
        });
    }
    responderSolicitudArea(formData) {
        return new Promise((resolve) => {
            this.ajax.post(`${this.url}ResponderSolicitudAreas`, formData).then((d) => {
                resolve(d);
            });
        });
    }

    //guardar(formData) {
    //    return new Promise((resolve) => {
    //        this.ajax.post(`${this.url}_EditPartial`, formData).then((d) => {
    //            resolve(d);
    //        }); 
    //    });
    //}
    //eliminar(formData) {
    //    return new Promise((resolve) => {
    //        this.ajax.post(`${this.url}DeleteConfirmed`, formData).then((d) => {
    //            resolve(d);
    //        });
    //    });
    //}
}