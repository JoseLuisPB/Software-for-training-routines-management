using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Final_DAM
{
    class Mensajes
    {
        // Mensaje general para elegir si o no, devuelve el valor true al elegir Sí y false al elegir No.
        public static bool mensajeSiNo(string cuerpo, string cabecera) {
            MessageBoxButton boton = MessageBoxButton.YesNo;
            MessageBoxImage icono = MessageBoxImage.Warning;
            MessageBoxResult seleccion = MessageBox.Show(cuerpo, cabecera, boton, icono);
            if (seleccion == MessageBoxResult.Yes)
            {
                return true;
            }
            else {
                return false;
            }
        }
        // Mensaje general de información
        public static void mensajeInformacion(string cuerpo, string cabecera) {
            MessageBoxButton boton = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Information;
            MessageBox.Show(cuerpo, cabecera, boton, icono);
        }
        // Mensaje general de error
        public static void mensajeError(string cuerpo, string cabecera)
        {
            MessageBoxButton boton = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Error;
            MessageBox.Show(cuerpo, cabecera, boton, icono);
        }
        // Mensaje para las validaciones de los campos en blanco
        public static void mensajeValidacionCampoBlanco(string campo) {
            MessageBoxButton boton = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Information;
            MessageBox.Show("El valor del campo " + campo + " no puede estar en blanco.", "Campo en blanco", boton, icono);
        }
        // Mensaje para las validaciones de los campos en blanco
        public static void mensajeCampoNumerico(string campo)
        {
            MessageBoxButton boton = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Information;
            MessageBox.Show("El valor del campo " + campo + " sólo puede ser númerico", "Formato incorrecto", boton, icono);
        }
        // Mensaje para las validaciones de longitud de campos
        public static void mensajeLongitudCampo(string campo, int longitudActual, int longitudExacta) {
            MessageBoxButton boton = MessageBoxButton.OK;
            MessageBoxImage icono = MessageBoxImage.Information;
            MessageBox.Show("La longitud del campo " + campo + " tiene que ser de " + longitudExacta + " caracteres, actualmente es de " + longitudActual, "Número de caracteres inexacto", boton, icono);
        }
    }
}
