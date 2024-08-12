using System.Security.Cryptography;
using System.Text;

namespace GEBIntegrador.Infraestructura
{
    public class Utilidades
    {
        public static string EncriptarClave(string clave)
        {

            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encriptado = Encoding.UTF8;

                byte[] resultado = hash.ComputeHash(encriptado.GetBytes(clave));

                foreach (byte b in resultado)
                {
                    sb.Append(b.ToString("x2")); //x2 : Formatea la cadena en Hex
                }

            }
            return sb.ToString();
        }

    }
}
