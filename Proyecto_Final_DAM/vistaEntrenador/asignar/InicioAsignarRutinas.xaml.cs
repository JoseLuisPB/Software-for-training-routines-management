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
using Proyecto_Final_DAM.modelo;
using Proyecto_Final_DAM.controlador;
using Proyecto_Final_DAM.vistaAlumno;

namespace Proyecto_Final_DAM.vistaEntrenador.asignar
{
    /// <summary>
    /// Lógica de interacción para InicioAsignarRutinas.xaml
    /// </summary>
    public partial class InicioAsignarRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioEntrenador ventanaInicioEntrenador;
        List<AlumnosLV> listaMostradaListView;
        AlumnosLV alumnoSeleccionado = null;
        public InicioAsignarRutinas(InicioUsuario ventanaRecibidaInicioUsuario, InicioEntrenador ventanaRecibidaInicioEntrenador)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioEntrenador = ventanaRecibidaInicioEntrenador;
            ventanaInicioUsuario.titulo.Text = "Lista de alumnos asignados";
            listaMostradaListView = new List<AlumnosLV>();
            this.cargaListaAlumnosAsignados();
        }
        //Función para cargar la lista de alumnos asignados al entrenador
        public void cargaListaAlumnosAsignados() {
            //Se pide a la BD la lista de alumnos activos asignados al entrenador
            List<Usuario> listaAlumnos = UsuarioController.listaUsuarioActivosAsignados(Sesion.Dni);
            foreach (Usuario item in listaAlumnos) {
                listaMostradaListView.Add(new AlumnosLV() {Dni = item.Dni, NombreCompleto = item.Nombre + " " + item.Apellidos });
            }
            listaAlumnosAsignados.ItemsSource = listaMostradaListView;
        }
        // Función para recoger los datos del elemento seleccionado en el listview
        private void seleccionActividadAsignada(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                alumnoSeleccionado = (AlumnosLV)listaAlumnosAsignados.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }
        // Función para cargar el frame donde se asignarán las rutinas al usuario seleccionado
        private void botonAsignarRutinas(object sender, RoutedEventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                ventanaInicioEntrenador.framePrincipal.Content = new AsignarRutinas(ventanaInicioUsuario, this, alumnoSeleccionado.Dni, alumnoSeleccionado.NombreCompleto);
            }
            else {
                Mensajes.mensajeError("Selecciona un alumno de la lista", "Alumno no seleccionado");
            }

        }
        // Función para cargar el frame donde se podrán eliminar las rutinas asignadas al usuario seleccionado
        private void botonEliminarRutinasAsignadas(object sender, RoutedEventArgs e) {
            if (alumnoSeleccionado != null)
            {
                ventanaInicioEntrenador.framePrincipal.Content = new EliminarAsignaciones(ventanaInicioUsuario, this, alumnoSeleccionado.Dni, alumnoSeleccionado.NombreCompleto);
            }
            else
            {
                Mensajes.mensajeError("Selecciona un alumno de la lista", "Alumno no seleccionado");
            }
        }
        // Función para cargar el frame donde se verán las rutinas asignadas al usuario seleccionado
        private void botonGestionarRutinasAsignadas(object sender, RoutedEventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                ventanaInicioEntrenador.framePrincipal.Content = new MostrarAsignaciones(ventanaInicioUsuario, this, alumnoSeleccionado.Dni, alumnoSeleccionado.NombreCompleto);
            }
            else
            {
                Mensajes.mensajeError("Selecciona un alumno de la lista", "Alumno no seleccionado");
            }
        }
        // Función para cargar el frame donde se verán las estadísticas del alumno seleccionado
        private void botonEstadisticas(object sender, RoutedEventArgs e)
        {
            if (alumnoSeleccionado != null)
            {
                ventanaInicioEntrenador.framePrincipal.Content = new EstadisticasAlumno(ventanaInicioUsuario,alumnoSeleccionado.Dni, alumnoSeleccionado.NombreCompleto);
            }
            else
            {
                Mensajes.mensajeError("Selecciona un alumno de la lista", "Alumno no seleccionado");
            }
        }
    }

    public class AlumnosLV {
        string dni;
        string nombreCompleto;

        public AlumnosLV(){
        
        }
        public string Dni { get => dni; set => dni = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
    }
}
