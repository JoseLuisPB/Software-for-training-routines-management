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
using Proyecto_Final_DAM.vistaAdmin;

namespace Proyecto_Final_DAM.vistaAlumno
{
    /// <summary>
    /// Lógica de interacción para CambiarEntrenador.xaml
    /// </summary>
    public partial class CambiarEntrenador : Page
    {
        InicioUsuario ventanaInicioUsuario;
        List<EntrenadoresLV> listaListView; // Lista que se quiere mostrar en el listView
        EntrenadoresLV entrenadorLVSeleccionado = null;
        Alumno alumne;
        string perfilAcceso;

        public CambiarEntrenador(InicioUsuario ventanaRecibidaInicioUsuario, string ident, string perfil)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            perfilAcceso = perfil;
            ventanaInicioUsuario.titulo.Text = "Asignar entrenador";
            this.cargaLV();
            // Creo un objeto de tipo alumno para tener los datos del alumno que ha iniciado sesión
            alumne = AlumnoController.obtener(ident)[0];
        }

        //Método para cargar el listview
        public void cargaLV() {
            listaListView = new List<EntrenadoresLV>();
            List<Entrenador> listaEntrenadoresActivos = EntrenadorController.listaEntrenadoresActivos();
            
            // Se recorre la lista de entrenadores para crear la lista con los entrenadores disponibles
            foreach (Entrenador item in listaEntrenadoresActivos) {
                Boolean estaAsignado = false; // Variable para controlar si el entrenador está asignado al alumno conectado
                // Datos del entrenador para rellenar el listview
                Usuario user = UsuarioController.obtener(item.Dni_usuario)[0];
                string totalAlumnosAsignados = EntrenadorController.numeroAlumnosAsignados(item.Dni_usuario)[0].Total;
                // Entrenador que tiene asignado el alumno que ha iniciado la sesión
                try
                {
                    // Busco quien es el entrenador que tiene asignado el usuario que ha iniciado la sesión
                    string entrenadorAsignado = AlumnoController.obtener(Sesion.Dni)[0].Entrenador_asignado;
                    // Se comprueba si el entrenador de la lista coincide con el entrenador asignado al alumno
                    if (entrenadorAsignado.Equals(item.Dni_usuario))
                    {
                        estaAsignado = true;
                    }
                }
                catch (System.ArgumentOutOfRangeException e) { 
                
                }

                listaListView.Add(new EntrenadoresLV() {Dni = user.Dni, NombreCompleto = user.Nombre + " "+ user.Apellidos, Descripcion = item.Biografia, Asignacion = estaAsignado, TotalAlumnos= totalAlumnosAsignados});
            }
            listaEntrenadores.ItemsSource = listaListView;
        }
        // Función que abre una nueva ventana con la descripción del entrenador
        private void botonVisualizar(object sender, RoutedEventArgs e) {
            Button botonPulsado = sender as Button;
            EntrenadoresLV entrenador = botonPulsado.CommandParameter as EntrenadoresLV;
            DatosEntrenador pantallaEntrenador = new DatosEntrenador(entrenador.Dni, entrenador.NombreCompleto);
            pantallaEntrenador.ShowDialog();
        }
        // Función que recoge los datos del item seleccionado en la lista Roles disponibles.
        private void listaEntrenadoresItemSeleccionado(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                entrenadorLVSeleccionado= (EntrenadoresLV)listaEntrenadores.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }
        // Función para asignarle al alumno el entrenador seleccionado
        private void botonGuardar(object sender, RoutedEventArgs e) {

            if (entrenadorLVSeleccionado != null)
            {
                // Se actualiza todo y el campo entrenador, por eso se obtienen los datos del alumno al principio.
                AlumnoController.actualizar(alumne.Dni_usuario,entrenadorLVSeleccionado.Dni, alumne.Dolencias, alumne.Objetivo);
                this.cargaLV();

                // Esta se ejecuta cuando se crea un usuario y se le ha de asignar un entrenador y así después de la asignación se muestra la lista de usuarios
                if (perfilAcceso.Equals("administrador")) {
                    ventanaInicioUsuario.frameInicio.Content = new InicioAdmin(ventanaInicioUsuario);
                }

            }
            else {
                Mensajes.mensajeInformacion("Selecciona un entrenador de la lista", "Entrenador no seleccionado");
            }
        }
    }
    // Clase para gestionar el listView
    public class EntrenadoresLV{
        private string nombreCompleto;
        private string dni;
        private string descripcion;
        private Boolean asignacion;
        private string totalAlumnos;

        public EntrenadoresLV() { 
        
        }

        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Asignacion { get => asignacion; set => asignacion = value; }
        public string TotalAlumnos { get => totalAlumnos; set => totalAlumnos = value; }
    }
}
