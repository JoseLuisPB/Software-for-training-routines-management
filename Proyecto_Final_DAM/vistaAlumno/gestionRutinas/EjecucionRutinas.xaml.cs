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

namespace Proyecto_Final_DAM.vistaAlumno.gestionRutinas
{
    /// <summary>
    /// Lógica de interacción para EjecucionRutinas.xaml
    /// </summary>
    public partial class EjecucionRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;
        List<DatosRutinasEjecutadas> listaNombresRutinas;
        List<DatosRutinasEjecutadas> listaNombresEjercicios;
        public EjecucionRutinas(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Ejecución de rutinas";
            fechaRutina.SelectedDate = DateTime.Today;
            this.actualizarDatos(0);
        }

        // Función para actualizar los datos mostrados
        public void actualizarDatos(int numero) {

            // Se crean las listas a usar
            listaNombresRutinas = new List<DatosRutinasEjecutadas>();
            listaNombresEjercicios = new List<DatosRutinasEjecutadas>();
            // Se ajusta la fecha
            DateTime fechaSeleccionada = (DateTime)fechaRutina.SelectedDate;
            DateTime fechaAjustada = fechaSeleccionada.AddDays(numero);
            fechaRutina.SelectedDate = fechaAjustada;

            List<Realizar> rutinasARealizar = RealizarController.obtener(Sesion.Dni, fechaAjustada.ToString("yyyy-MM-dd"));
            
            foreach (Realizar item in rutinasARealizar) {
                
                string nombreDeRutina = RutinaController.obtener(item.Codigo_rutina)[0].Nombre;
                listaNombresRutinas.Add(new DatosRutinasEjecutadas() { NombreRutina = nombreDeRutina, CodigoRutina = item.Codigo_rutina, Ejecutada=item.Ejecutada});
            }
            rutinasContenidas.ItemsSource = listaNombresRutinas;
        }

        // Función para actualizar de forma automática los datos al cambiar de fecha
        private void actualizarDatosCambioFecha(object sender, SelectionChangedEventArgs e)
        {
            this.actualizarDatos(0);
        }

        // Función para mostrar los datos de mañana
        public void botonManyana(object sender, RoutedEventArgs e)
        {
            this.actualizarDatos(1);
        }
        // Función para mostrar los datos de ayer
        public void botonAyer(object sender, RoutedEventArgs e)
        {
            this.actualizarDatos(-1);
        }

        // Función para actualizar con valor true el campo ejecutado de la tabla realizar
        public void botonOk(object sender, RoutedEventArgs e) {
            Button botonPulsado = sender as Button;
            DatosRutinasEjecutadas infoBoton = botonPulsado.CommandParameter as DatosRutinasEjecutadas;
            DateTime fechaSeleccionada = (DateTime)fechaRutina.SelectedDate;
            RealizarController.rutinaEjecutada(Sesion.Dni,infoBoton.CodigoRutina,fechaSeleccionada.ToString("yyyy-MM-dd"));
            this.actualizarDatos(0);

        }
        // Función para actualizar con valor false el campo ejecutado de la tabla realizar
        public void botonNoOk(object sender, RoutedEventArgs e)
        {
            Button botonPulsado = sender as Button;
            DatosRutinasEjecutadas infoBoton = botonPulsado.CommandParameter as DatosRutinasEjecutadas;
            DateTime fechaSeleccionada = (DateTime)fechaRutina.SelectedDate;
            RealizarController.rutinaNoEjecutada(Sesion.Dni, infoBoton.CodigoRutina, fechaSeleccionada.ToString("yyyy-MM-dd"));
            this.actualizarDatos(0);
        }

        // Fúnción para dar el Ok a todas las rutinas
        public void botonTodoOk(object sender, RoutedEventArgs e) {
            DateTime fechaSeleccionada = (DateTime)fechaRutina.SelectedDate;
            List<Realizar> rutinasARealizar = RealizarController.obtener(Sesion.Dni, fechaSeleccionada.ToString("yyyy-MM-dd"));
            foreach (Realizar item in rutinasARealizar) {
                RealizarController.rutinaEjecutada(item.Dni_usuario, item.Codigo_rutina, fechaSeleccionada.ToString("yyyy-MM-dd"));
            }
            this.actualizarDatos(0);
        }

        // Fúnción para dar el no Ok a todas las rutinas
        public void botonTodoNoOk(object sender, RoutedEventArgs e)
        {
            DateTime fechaSeleccionada = (DateTime)fechaRutina.SelectedDate;
            List<Realizar> rutinasARealizar = RealizarController.obtener(Sesion.Dni, fechaSeleccionada.ToString("yyyy-MM-dd"));
            foreach (Realizar item in rutinasARealizar)
            {
                RealizarController.rutinaNoEjecutada(item.Dni_usuario, item.Codigo_rutina, fechaSeleccionada.ToString("yyyy-MM-dd"));
            }
            this.actualizarDatos(0);
        }

    }
    // Clase para crear los datos de la ejecución de las rutinas

    public class DatosRutinasEjecutadas {

        private string nombreRutina;
        private int codigoRutina;
        private Boolean ejecutada;


        public DatosRutinasEjecutadas() { }

        public string NombreRutina { get => nombreRutina; set => nombreRutina = value; }
        public int CodigoRutina { get => codigoRutina; set => codigoRutina = value; }
        public Boolean Ejecutada { get => ejecutada; set => ejecutada = value; }
        
    }
}
