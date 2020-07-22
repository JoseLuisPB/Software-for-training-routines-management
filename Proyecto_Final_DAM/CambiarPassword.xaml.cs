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
            
            // Se accede aquí desde la pantalla de crear o al pulsar el botón de reinicio del password para darle un password inicial al usuario
            if (tipoOperacion.Equals("crear") || tipoOperacion.Equals("reset")) {
                titulo.Text = "Introduce una contraseña de inicio";
                repetirContrasenyaLabel.Visibility = Visibility.Collapsed;
                repetirContrasenyaLabel.SetValue(Grid.RowProperty, 3);
                repetirContrasenya.Visibility = Visibility.Collapsed;
                repetirContrasenya.SetValue(Grid.RowProperty, 3);
                cancelar.Visibility = Visibility.Collapsed;
                cancelar.SetValue(Grid.RowProperty, 2);
                aceptar.SetValue(Grid.RowProperty, 2);
            }
        }
        // Función que actualiza el password
        private void botonAceptar(object sender, RoutedEventArgs e)
        {
            // Se comprueba que el campo password no esté en blanco
            if (Validaciones.campoEnBlanco("nueva contraseña", nuevaContrasenya.Password.ToString()) == false) {
                if (tipoOperacion.Equals("crear") || tipoOperacion.Equals("reset"))
                {
                    // Se actualiza el campo password
                    UsuarioController.actualizarPassword(Sesion.gestionPasswords(nuevaContrasenya.Password.ToString()), dni);
                    // Se resetea el estado para que el usuario pueda introducir el password que quiera
                    UsuarioController.resetPassword(dni);
                    this.Close();
                }
                else {
                    // Se comprueba que el texto de los dos campos es el mismo
                    if (nuevaContrasenya.Password.ToString().Equals(repetirContrasenya.Password.ToString()))
                    {
                        // Se actualiza el campo password
                        UsuarioController.actualizarPassword(Sesion.gestionPasswords(nuevaContrasenya.Password.ToString()), dni);
                        this.Close();
                    }
                    else
                    { // El valor de los dos campos es diferente
                        Mensajes.mensajeInformacion("El texto de los dos campos tiene que ser el mismo", "Valores diferentes");
                    }
                }
                
            }
        }
        //Función para cerrar la pantalla sin ejecutar cambios
        private void botonCancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
