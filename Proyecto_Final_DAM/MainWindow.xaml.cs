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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Proyecto_Final_DAM.vistaAdmin;
using Proyecto_Final_DAM.vistaEntrenador;
using Proyecto_Final_DAM.modelo;
using Proyecto_Final_DAM.controlador;

namespace Proyecto_Final_DAM
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Usuario user;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void botonAcceder(object sender, RoutedEventArgs e)
        {
            //Validación de campos para acceder al sistema

            // Se valida que el campo del usuario no esté en blanco
            if (!Validaciones.campoEnBlanco("usuario", usuario.Text))
            {
                try
                {
                    this.accesoDatosUsuarioConectado();

                }
                catch (System.NullReferenceException)
                {
                    Mensajes.mensajeInformacion("El servidor está apagado, por favor arránquelo", "Servidor apagado");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Mensajes.mensajeInformacion("El usuario " + usuario.Text + " no existe en el sistema", "Usuario inexistente");
                }

            }
        }
        // Función para acceder a los datos del usuario
        public void accesoDatosUsuarioConectado()
        {
            user = UsuarioController.obtener(usuario.Text.ToLower())[0];

            // Se comprueba que el usuario esté activo
            if (user.Activo)
            {
                this.validarCampoPasswordUsuario();

            }
            else
            {
                Mensajes.mensajeInformacion("El usuario " + usuario.Text + " está dado de baja, consulta con el administrador para darlo de alta", "Usuario inactivo");
            }

        }
        // Función para validar el password
        public void validarCampoPasswordUsuario()
        {
            // Se comprueba que el campo password no esté en blanco
            if (!Validaciones.campoEnBlanco("contraseña", contrasenya.Password.ToString()))
            {
                // Se comprueba que el password es el correcto
                if (Sesion.gestionPasswords(contrasenya.Password.ToString()).Equals(user.Contrasenya))
                {
                    this.accesoAlSistema();
                }
                else
                {
                    Mensajes.mensajeError("El password introducido no es correcto", "Password incorrecto");
                }
            }
        }
        // Función para acceder al sistema
        public void accesoAlSistema()
        {
            int numRoles = TenerController.numRolesUsuario(usuario.Text)[0].Total;

            if (numRoles > 0)
            {
                // Se recogen los datos del usuario para la sesión
                Sesion.Nombre = user.Nombre;
                Sesion.Apellidos = user.Apellidos;
                Sesion.Dni = user.Dni;

                // Acceso a la pantalla correspondiente
                InicioUsuario inicio = new InicioUsuario(numRoles);
                inicio.Show();
                this.Close();
            }
            else
            {
                Mensajes.mensajeInformacion("El usuario no tiene roles asignados, para acceder al sistema el usuario tiene que tener asignado al menos 1 rol", "Usuario sin roles");
            }
        }
        // Función para salir del sistema
        private void botonSalir(object sender, RoutedEventArgs e)
        {
            this.Close();
            ExtraController.detener();
        }
    }
}
