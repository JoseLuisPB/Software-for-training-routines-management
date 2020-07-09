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

namespace Proyecto_Final_DAM.vistaEntrenador.asignar
{
    /// <summary>
    /// Lógica de interacción para EliminarAsignaciones.xaml
    /// </summary>
    public partial class EliminarAsignaciones : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioAsignarRutinas ventanaAsignarRutinas;
        string dni;
        DateTime fechaSeleccionada;
        RutinasAsignadasLV rutinaSeleccionada = null;
        List<RutinasAsignadasLV> listaRutinasLV;
        public EliminarAsignaciones(InicioUsuario ventanaRecibidaInicioUsuario, InicioAsignarRutinas ventanaRecibidaAsignarRutinas,string ident, string name)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Eliminar rutina asignada";
            ventanaAsignarRutinas = ventanaRecibidaAsignarRutinas;
            dni = ident;
            textoAlumno.Text = "Eliminar rutinas de " + name + " para la fecha: ";

            // Caso de que el que se conecte sea el alumno
            if (ventanaInicioUsuario.roles.SelectedItem.Equals("alumno")) {
                textoAlumno.Text = "Eliminar rutinas del día: ";
            }

            // Ajuste de las fechas
            fechaPicker.SelectedDate = DateTime.Today;
            fechaSeleccionada = DateTime.Today;
            // Actualización del listview
            this.actualizarListViewRutinas();
           
        }
        // Función que muestra las actividades que contiene una rutina
        private void botonMostrarActividadesRutina(object sender, RoutedEventArgs e)
        {
            Button botonPulsado = sender as Button;
            RutinasAsignadasLV routine = botonPulsado.CommandParameter as RutinasAsignadasLV;
            string codigoRutinaSeleccionada = Convert.ToString(routine.CodigoRutina);
            DetalleRutinas pantallaDetalleRutinas = new DetalleRutinas(codigoRutinaSeleccionada);
            pantallaDetalleRutinas.ShowDialog();
        }

        // Función para cambiar de fecha cuando se actualice el datepicker
        private void cambioFecha(object sender, SelectionChangedEventArgs e)
        {
            fechaSeleccionada = (DateTime)fechaPicker.SelectedDate;
            this.actualizarListViewRutinas();
        }
        // Función para mostrar los datos de mañana
        public void botonManyana(object sender, RoutedEventArgs e)
        {
            fechaPicker.SelectedDate = fechaSeleccionada.AddDays(1);
        }
        // Función para mostrar los datos de ayer
        public void botonAyer(object sender, RoutedEventArgs e)
        {
            fechaPicker.SelectedDate = fechaSeleccionada.AddDays(-1);
        }
        // Función para guardar la información de la rutina seleccionada
        private void listViewRutinaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rutinaSeleccionada = (RutinasAsignadasLV)listViewRutinasAsignadas.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }
        
        // Función cargar el listview de rutinas asignadas cuando se inicia la página --> Eliminado, se utilizaba cuando existía un botón para ejecutar
        private void cargarRutinasDia(object sender, RoutedEventArgs e) {
             fechaSeleccionada = (DateTime)fechaPicker.SelectedDate;
            this.actualizarListViewRutinas();
        }
        // Función para actualizar el listview de rutinas asignadas
        private void actualizarListViewRutinas()
        {
            listaRutinasLV = new List<RutinasAsignadasLV>();
            List<Realizar> listaRutinasAsignadas = RealizarController.obtener(dni, fechaSeleccionada.ToString("yyyy-MM-dd"));
            foreach (Realizar item in listaRutinasAsignadas)
            {
                string estadoRutina;

                if (item.Ejecutada == true)
                {
                    estadoRutina = "Sí";
                }
                else {
                    estadoRutina = "No";
                }
                string nombreDeLaRutina = RutinaController.obtener(item.Codigo_rutina)[0].Nombre;
                listaRutinasLV.Add(new RutinasAsignadasLV() { Dni = dni, NombreRutina = nombreDeLaRutina, CodigoRutina = item.Codigo_rutina, Fecha = item.Fecha, Ejecutada = item.Ejecutada, EjecutadaString = estadoRutina });
            }
            listViewRutinasAsignadas.ItemsSource = listaRutinasLV;
        }
        // Función para borrar la rutina seleccionada
        private void eliminarRutinas(object sender, RoutedEventArgs e)
        {
            if (rutinaSeleccionada != null)
            {
                Boolean estadoRutina = RealizarController.rutinaFecha(dni, rutinaSeleccionada.CodigoRutina, fechaSeleccionada.ToString("yyyy-MM-dd"))[0].Ejecutada;
                string dniCreador = RutinaController.obtener(rutinaSeleccionada.CodigoRutina)[0].Dni_usuario;
                // Comprobación de que la rutina no esté ejecutada
                if (estadoRutina == true)
                {
                    Mensajes.mensajeInformacion("No se puede borrar una rutina que ya ha sido ejecutada", "Rutina ejecutada");
                }
                // Comprobación de que el usuario que va a borrar la rutina es quien la ha asignado
                else if (!Sesion.Dni.Equals(dniCreador)) {
                    Mensajes.mensajeInformacion("No se puede borrar una rutina asignada por otro usuario", "Rutina de otro usuario");
                }
                else {
                    if (Mensajes.mensajeSiNo("¿Deseas eliminar la rutina seleccionada?", "Eliminar rutina"))
                    {
                        RealizarController.eliminar(dni, rutinaSeleccionada.CodigoRutina, fechaSeleccionada.ToString("yyyy-MM-dd"));
                        this.actualizarListViewRutinas();
                    }
                }

            }
            else {
                Mensajes.mensajeError("Selecciona una rutina de la lista", "Rutina no seleccionada");
            }
        }


    }

    // Clase para gestionar la listView
    public class RutinasAsignadasLV {
        private string dni;
        private string nombreRutina;
        private int codigoRutina;
        private string fecha;
        private Boolean ejecutada;
        private string ejecutadaString;

        public RutinasAsignadasLV(){
        }
        public string Dni { get => dni; set => dni = value; }
        public string NombreRutina { get => nombreRutina; set => nombreRutina = value; }
        public int CodigoRutina { get => codigoRutina; set => codigoRutina = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public bool Ejecutada { get => ejecutada; set => ejecutada = value; }
        public string EjecutadaString { get => ejecutadaString; set => ejecutadaString = value; }
    }
}
