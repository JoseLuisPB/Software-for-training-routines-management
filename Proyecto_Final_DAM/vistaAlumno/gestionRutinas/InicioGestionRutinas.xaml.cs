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
using Proyecto_Final_DAM.vistaEntrenador;
using Proyecto_Final_DAM.vistaEntrenador.asignar;

namespace Proyecto_Final_DAM.vistaAlumno.gestionRutinas
{
    /// <summary>
    /// Lógica de interacción para InicioGestionRutinas.xaml
    /// </summary>
    public partial class InicioGestionRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;
        public InicioGestionRutinas(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Gestión rutinas asignadas";
            frameRutinas.Content = new EjecucionRutinas(ventanaInicioUsuario);
        }
        // Función para cargar el frame donde el alumno indica la rutina que ha ejecutado
        private void botonEjecutarRutina(object sender, RoutedEventArgs e)
        {
            frameRutinas.Content = new EjecucionRutinas(ventanaInicioUsuario);
        }
        // Función para cargar el frame donde el alumno se asigna sus rutinas
        private void botonAsignarRutinas(object sender, RoutedEventArgs e) {
            // Se necesita la pantalla de asignar rutinas y para la pantalla de asignar rutinas se necesita la del entrenador
            // Para usar esta parte necesito cargar el frame en un frame de una pantalla del entrenador, por lo tanto se crea
            InicioEntrenador pantallaEntrenador = new InicioEntrenador(ventanaInicioUsuario);
            InicioAsignarRutinas pantallaAsignarRutinas = new InicioAsignarRutinas(ventanaInicioUsuario, pantallaEntrenador);
            string nombreCompleto = Sesion.Nombre + " " + Sesion.Apellidos;
            frameRutinas.Content = new AsignarRutinas(ventanaInicioUsuario, pantallaAsignarRutinas, Sesion.Dni, nombreCompleto);
        }
        // Función para cargar el frame donde el usuario elimina las rutinas que tiene asignadas
        private void botonEliminarRutinas(object sender, RoutedEventArgs e)
        {
            InicioEntrenador pantallaEntrenador = new InicioEntrenador(ventanaInicioUsuario);
            InicioAsignarRutinas pantallaAsignarRutinas = new InicioAsignarRutinas(ventanaInicioUsuario, pantallaEntrenador);
            string nombreCompleto = Sesion.Nombre + " " + Sesion.Apellidos;
            frameRutinas.Content = new EliminarAsignaciones(ventanaInicioUsuario, pantallaAsignarRutinas, Sesion.Dni, nombreCompleto);
        }
        // Función para cargar el frame con las rutinas asignadas
        private void botonRutinasAsignadas(object sender, RoutedEventArgs e)
        {
            InicioEntrenador pantallaEntrenador = new InicioEntrenador(ventanaInicioUsuario);
            InicioAsignarRutinas pantallaAsignarRutinas = new InicioAsignarRutinas(ventanaInicioUsuario, pantallaEntrenador);
            string nombreCompleto = Sesion.Nombre + " " + Sesion.Apellidos;
            frameRutinas.Content = new MostrarAsignaciones(ventanaInicioUsuario, pantallaAsignarRutinas , Sesion.Dni, nombreCompleto);
        }
        // Función para cargar el frame con las estadisticas
        private void botonEstadisticas(object sender, RoutedEventArgs e)
        {
            string nombreCompleto = Sesion.Nombre + " " + Sesion.Apellidos;
            frameRutinas.Content = new EstadisticasAlumno(ventanaInicioUsuario, Sesion.Dni, nombreCompleto);
        }
    }
}
