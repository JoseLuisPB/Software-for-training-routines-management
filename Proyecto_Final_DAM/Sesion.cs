using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Final_DAM.controlador;
using Proyecto_Final_DAM.modelo;

namespace Proyecto_Final_DAM
{
    class Sesion
    {
        private static string nombre;
        private static string apellidos;
        private static string dni;

        public static string Nombre { get => nombre; set => nombre = value; }
        public static string Apellidos { get => apellidos; set => apellidos = value; }
        public static string Dni { get => dni; set => dni = value; }

        //Función para encriptar el password
        public static string gestionPasswords(string pass)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            // Se codifica el password byte a byte
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(pass));
            // Se transforma el password a hexadecimal
            for (int i = 0; i < bytes.Length; i++)
            {
                //x2 indica al método to string que debe formatear el texto en hexadecimal
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
