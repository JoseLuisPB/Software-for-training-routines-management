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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        // Función para acceder al sistema, se comprueba que el usuario y password sean correctos
        private void botonAcceder(object sender, RoutedEventArgs e)
        {
            //Validación de campos para acceder al sistema

            // Se valida que el campo del usuario no esté en blanco
            if (Validaciones.campoEnBlanco("usuario", usuario.Text) == false)
            {
                // Se crea un objeto con los datos del usuario que inicia sesión
                try
                {
                    Usuario user = UsuarioController.obtener(usuario.Text.ToLower())[0];
                    // Se comprueba que el usuario esté activo
                    if (user.Activo)
                    {
                        // Se comprueba que el campo password no esté en blanco
                        if (Validaciones.campoEnBlanco("contraseña", contrasenya.Password.ToString()) == false)
                        {
                            // Se comprueba que el password es el correcto
                            if (Sesion.gestionPasswords(contrasenya.Password.ToString()).Equals(user.Contrasenya))
                            {
                                // Se recogen los datos del usuario para la sesión
                                Sesion.Nombre = user.Nombre;
                                Sesion.Apellidos = user.Apellidos;
                                Sesion.Dni = user.Dni;

                                // Se recoge el número de roles que tiene el usuario
                                int numRoles = TenerController.numRolesUsuario(usuario.Text)[0].Total;
                                //Según el número de roles cargará una u otra pantalla
                                if (numRoles > 0)
                                {
                                    //Accedo a la pantalla
                                    InicioUsuario inicio = new InicioUsuario(numRoles);
                                    inicio.Show();
                                    this.Close();
                                }
                                else
                                {
                                    Mensajes.mensajeInformacion("El usuario no tiene roles asignados", "Usuario sin roles");
                                }

                            }
                            else
                            {
                                Mensajes.mensajeError("El password introducido no es correcto", "Password incorrecto");
                            }
                        }

                    }
                    else
                    { //El usuario no está activo
                        Mensajes.mensajeInformacion("El usuario " + usuario.Text + " está dado de baja, consulta con el administrador para darlo de alta", "Usuario inactivo");
                    }
                }
                // El servidor todavía no se ha inicializado
                catch (System.NullReferenceException) {
                    Mensajes.mensajeInformacion("El servidor está apagado, por favor arránquelo", "Servidor apagado");
                }
                // El usuario no existe en la base de datos.
                catch (ArgumentOutOfRangeException)
                { 
                    Mensajes.mensajeInformacion("El usuario " + usuario.Text + " no existe en el sistema", "Usuario inexistente");
                }
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
