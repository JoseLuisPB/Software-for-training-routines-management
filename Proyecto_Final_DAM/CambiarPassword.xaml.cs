using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Proyecto_Final_DAM.modelo;
using Proyecto_Final_DAM.controlador;

namespace Proyecto_Final_DAM
{
    /// <summary>
    /// Lógica de interacción para CambiarPassword.xaml
    /// </summary>
    public partial class CambiarPassword : Window
    {
        string dni;
        string tipoOperacion;
        public CambiarPassword(string identificador, string operacion)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            dni = identificador;
            tipoOperacion = operacion;

            if (tipoOperacion.Equals("crear") || tipoOperacion.Equals("reset"))
            {
                this.ajustesCrearReset();
            }
        }
        // Función para ajustar la pantalla cuando se crea o se resetea un password
        public void ajustesCrearReset()
        {
            titulo.Text = "Introduce una contraseña de inicio";
            repetirContrasenyaLabel.Visibility = Visibility.Collapsed;
            repetirContrasenyaLabel.SetValue(Grid.RowProperty, 3);
            repetirContrasenya.Visibility = Visibility.Collapsed;
            repetirContrasenya.SetValue(Grid.RowProperty, 3);
        }
        // Función que actualiza el password
        private void botonAceptar(object sender, RoutedEventArgs e)
        {
            // Se comprueba que el campo password no esté en blanco
            if (!Validaciones.campoEnBlanco("nueva contraseña", nuevaContrasenya.Password.ToString()))
            {
                if (tipoOperacion.Equals("crear") || tipoOperacion.Equals("reset"))
                {
                    this.cambiarPassword();
                    UsuarioController.resetPassword(dni);

                }
                else
                {
                    // Se comprueba que el texto de los dos campos es el mismo
                    if (nuevaContrasenya.Password.ToString().Equals(repetirContrasenya.Password.ToString()))
                    {
                        this.cambiarPassword();
                    }
                    else
                    { // El valor de los dos campos es diferente
                        Mensajes.mensajeInformacion("El texto de los dos campos tiene que ser el mismo", "Valores diferentes");
                    }
                }
            }
        }
        private void cambiarPassword()
        {
            UsuarioController.actualizarPassword(Sesion.gestionPasswords(nuevaContrasenya.Password.ToString()), dni);
            this.Close();
        }
        //Función para cerrar la pantalla sin ejecutar cambios
        private void botonSalir(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
