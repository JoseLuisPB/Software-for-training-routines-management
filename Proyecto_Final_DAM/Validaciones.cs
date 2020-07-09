using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM
{
    class Validaciones
    {
        // Funcion para comprobar si un campo está en blanco
        // Devuelve true si el campo está en blanco y false si no lo está
        public static Boolean campoEnBlanco(string campo, string texto) {
            Boolean resultado = false;
            if (string.IsNullOrWhiteSpace(texto)){
                resultado = true;
                Mensajes.mensajeValidacionCampoBlanco(campo);
            }
            return resultado;
        }
        // Función para comprobar que el campo sólo contiene números
        // Devuelve true si el campo esta formado sólo por números y false si contiene algún caracter
        public static Boolean campoNumerico(string campo, string texto)
        {
            Boolean resultado = true;
            int i;
            if (!int.TryParse(texto, out i))
            {
                Mensajes.mensajeCampoNumerico(campo);
                resultado = false;
            }
            return resultado;
        }
        // Función para comprobar que el campo tiene el número de caracteres exacto.
        // Devuelve true si el número de caracteres es el exacto y false si no lo es.
        public static Boolean longitudCampo(string campo, string texto, int longitud) {
            Boolean resultado = true;
            int longitudTexto = texto.Length;
            if (longitudTexto != longitud) {
                resultado = false;
                Mensajes.mensajeLongitudCampo(campo, longitudTexto, longitud);
            }
            return resultado;
        }

        // Función para comprobar el formato del email.
        // Devuelve true si el formato es correcto y false si no lo es
        public static Boolean formatoMail(string texto) {
            Boolean resultado = true;
            //Usamos una expresión regular para comprobar el mail
            var regex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                       @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$";
            var match = Regex.Match(texto, regex);
            if (!match.Success)
            {
                resultado = false;
                Mensajes.mensajeInformacion("El formato del campo E-mail no es correcto", "Formato incorrecto");
            }
            return resultado;
        }
        //Función para comprobar que el formato del DNI o NIE es correcto
        //Devuelve true si el formato del DNI o del NIE es correcto y false si no lo es
        public static Boolean formatoDniNie(string texto) {
            Boolean resultado = false;
            var dniRegex = @"^[0-9]{8,8}[A-Za-z]$";
            var nieRegex = @"^[XxTtYyZz]{1}[0-9]{7}[a-zA-Z]{1}$";
            var matchDni = Regex.Match(texto, dniRegex);
            var matchNie = Regex.Match(texto, nieRegex);
            // Comprobación de que el formato del DNI o NIE es válido
            if (matchDni.Success || matchNie.Success)
            {
                // El formato del DNI o el NIE es válido 
                resultado = true;
            }
            else {
                Mensajes.mensajeInformacion("El formato del DNI o NIE no es correcto", "Formato incorrecto");
            }
            return resultado;
        }
    }
}
