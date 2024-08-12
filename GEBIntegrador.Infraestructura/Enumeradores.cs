namespace GEBIntegrador.Infraestructura
{
    public class Enumeradores
    {

        #region General
        public static int Activo = 1;
        public static int Inactivo = 0;
        #endregion General

        #region Usuario
        public static int UsuarioEliminado = 1;        
        public static int UsuarioInactivo = 0;
        public static int UsuarioActivo = 1;
        #endregion Contrato

        #region Niveles
        public static int sotanos = 1;
        public static int pisos = 2;
        #endregion Niveles

        #region TiposRecurso
        public static int puesto = 2;
        public static int sala = 3;
        #endregion TiposRecurso

        #region Reservas
        public static int tipodeReservaPorUsuario = 1;
        public static int tipodeReservaPorMantenimiento = 2;

        #endregion Reservas

        #region Perfil
        public static int sst = 5;
        #endregion Perfil

    }
}
