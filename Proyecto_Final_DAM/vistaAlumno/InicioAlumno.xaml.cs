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
using Proyecto_Final_DAM.controlador;
using Proyecto_Final_DAM.modelo;
using Proyecto_Final_DAM.vistaEntrenador.rutinas;
using Proyecto_Final_DAM.vistaEntrenador.asignar;
using Proyecto_Final_DAM.vistaAlumno.gestionRutinas;

namespace Proyecto_Final_DAM.vistaAlumno
{
    /// <summary>
    /// Lógica de interacción para InicioAlumno.xaml
    /// </summary>
    public partial class InicioAlumno : Page
    {
        InicioUsuario ventanaInicioUsuario;
        public InicioAlumno(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            framePrincipal.Content = new InicioGestionRutinas(ventanaInicioUsuario);
        }
        // Método para cargar el frame que muestra la pantalla donde el usuario puede modificar su perfil
        private void botonPerfil(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new GestionarPerfilAlumno(ventanaInicioUsuario);
        }
        // Método para cargar el frame que muestra la pantalla donde se pueden gestionar las rutinas
        private void botonGestionarRutinas(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new InicioRutinas(ventanaInicioUsuario);
        }

        // Método para cargar el frame que muestra la pantalla donde se puede cambiar el entrenador asignado
        private void botonEntrenador(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new CambiarEntrenador(ventanaInicioUsuario, Sesion.Dni,"alumno");
        }

        // Método para cargar el frame que muestra la pantalla donde se pueden gestionar la asignación de rutinas
        private void botonRutinasAsignadas(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new InicioGestionRutinas(ventanaInicioUsuario);
        }
    }
}
