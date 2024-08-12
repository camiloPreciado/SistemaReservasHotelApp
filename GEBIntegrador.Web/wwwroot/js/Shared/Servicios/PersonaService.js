import ajaxService from "../AjaxService.js";

export default class PersonaService {
    constructor() {
        this.ajax = new ajaxService();
        this.url = "/Persona/";
    }

    getPersona(data) {
        return new Promise((resolve) => {
            this.ajax.get(`${this.url}getPersona`, data).then((response) => {
                resolve(response);
            });
        });
    }
}