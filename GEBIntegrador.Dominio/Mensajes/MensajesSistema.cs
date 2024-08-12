using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEBIntegrador.Dominio.Mensajes
{
    public class MensajesSistema
    {
        #region Sistema
        public static string MsgOcurrioUnProblema { get; set; } = "Ocurrio un error. Comuniquese con el Administrador del sistema.";
        public static string MsgAutenticacionNoValida { get; set; } = "Autenticación no valida.";
        public static string MsgClavenoValida { get; set; } = "Clave no valida.";
        public static string MsgErrorCreandoRutaArchivos { get; set; } = "No es posible crear la ruta de almacenamiento de archivos. Comuniquese con el Administrador del sistema.";
        public static string MsgArchivoNoEncontrado { get; set; } = "Archivo no encontrado. Comuniquese con el Administrador del sistema.";
        public static string MsgErrorLoginDirectorioActivo { get; set; } = "Error al autenticarse con el directorio activo, por favor comuniquese con el administrador.";
        public static string MsgDocumentoRegistradoNoCoincideconDA { get; set; } = "El documento registrado no coincide la información registrada en Directorio Activo, por favor comuniquese con el administrador.";
        public static string MsgDatosIngresadosIncorrectos { get; set; } = "Los datos ingresados son incorrectos.";
        public static string MsgContrasenaEnviada  { get; set; } = "El sistema a enviado un correo electronico con la nueva contraseña, revise su bandeja de entrada.";
        #endregion Sistema

        #region General
        public static string MsgNosePudoEliminar { get; set; } = "No se pudo eliminar!";
        public static string MsgInformacionAlmacenadaConExito { get; set; } = "Información almacenada con exito!";
        public static string MsgEstadoCambiado { get; set; } = "Estado cambiado con exito!";
        public static string MsgEstadoNoValido { get; set; } = "Estado no valido por favor validar la información enviada!";
        public static string MsgNosePudoCambiarElEstado { get; set; } = "No se pudo cambiar el estado!";
        #endregion General

        #region Usuario
        public static string MsgRequierePreRegistro { get; set; } = "Requiere un pre-registro. Contacte al Administrador de la aplicación";
        public static string MsgNosePuedeCrearUsuario { get; set; } = "No se puede crear el usuario. Contacte al Administrador de la aplicación";
        public static string MsgUsuarioYaseEncuentraRegistrado { get; set; } = "Ya se encuentra registrado y activado";
        public static string MsgUsuarioNoExiste { get; set; } = "El usuario no existe!";
        public static string MsgUsuarioNosePudoEditar { get; set; } = "El usuario no se pudo editar!";
        public static string MsgUsuarioInactivo { get; set; } = "Usuario desactivado. Contacte al Administrador de la aplicación";
        public static string MsgUsuarioActualizado { get; set; } = "Usuario actualizado de manera exitosa.";
        public static string MsgUsuarioEliminado { get; set; } = "Usuario eliminado.";
        public static string MsgUsuarioNoLogueado { get; set; } = "Usuario no logueado.";
        public static string MsgCorreoYaRegistrado { get; set; } = "El correo ya se encuentra registrado.";
        public static string MsgCedulaYaRegistrada { get; set; } = "El numero de documento ya se encuentra registrado.";
        public static string MsgDatosdePersonasonRequeridos { get; set; } = "Los datos de la persona son requeridos.";
        public static string MsgDatosDADesactualizados { get; set; } = "Sr. Usuario los siguientes datos se encuentran desactualizados en el directorio activo, por favor comuniquesé con el Administrador";
        public static string MsgUsuarioNoRequiereActivacion { get; set; } = "Sr. Usuario, usted no requiere activación, este proceso aplica unicamente para el usuario de tipo Contratista.";
        public static string MsgClaveActualErrada { get; set; } = "Sr. Usuario, la clave actual ingresada no coincide con la clave registrada.";
        public static string MsgClaveActualizada { get; set; } = "Sr. Usuario, la clave fue actualizada con exito.";
        public static string MsgClaveCaducada { get; set; } = "Sr. Usuario, su clave a caducado.";
        public static string MsgClaveNoCumpleSeguridad { get; set; } = "Sr. Usuario, la nueva clave ingresada no cumple con las condiciones de seguridad.";

        #endregion Usuario

        #region Parametros
        public static string MsgParametroCorreoOrigenNoConfigurado { get; set; } = "El parametro de 'Correo origen' no se encuentra configurado.";
        public static string MsgParametroHostNoConfigurado { get; set; } = "El parametro de 'Post' no se encuentra configurado.";
        public static string MsgParametroPortNoConfigurado { get; set; } = "El parametro de 'Port' no se encuentra configurado.";
        public static string MsgParametroSubjectNoConfigurado { get; set; } = "El parametro de 'Subject' no se encuentra configurado.";
        public static string MsgParametroPassNoConfigurado { get; set; } = "El parametro de 'Pass' no se encuentra configurado.";
        public static string MsgParametroImagenNoConfigurado { get; set; } = "El parametro de 'Logo' no se encuentra configurado.";
        public static string MsgParametroEdadContrasena { get; set; } = "El parametro de 'Tiempo vencimiento clave (Días)' no se encuentra configurado de manera correcta.";
        #endregion Parametros

        #region Reservas
        public static string MsgReservaCancelada { get; set; } = "Reserva cancelada exitosamente";
        public static string MsgReservaNoCancelada { get; set; } = "No se pudo cancelar la reserva";
        #endregion Reservas

    }
}
