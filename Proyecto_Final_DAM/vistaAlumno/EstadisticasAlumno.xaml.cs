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

namespace Proyecto_Final_DAM.vistaAlumno
{
    /// <summary>
    /// Lógica de interacción para EstadisticasAlumno.xaml
    /// </summary>
    public partial class EstadisticasAlumno : Page
    {
        InicioUsuario ventanaInicioUsuario;
        string nombre;
        string dni;
        DateTime inicio;
        DateTime fin;
        double numDias;

        public EstadisticasAlumno(InicioUsuario ventanaRecibidaInicioUsuario, string ident, string name)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Estadísticas";
            dni = ident;
            nombre = name;

            tbAlumno.Text = "Estadísticas para el alumno " + nombre;
            // Caso de que el usuario conectado sea alumno
            if (ventanaInicioUsuario.roles.SelectedItem.Equals("alumno"))
            {
                tbAlumno.Visibility = Visibility.Collapsed;
            }
            
            fechaFin.SelectedDate = DateTime.Today;
            fechaInicio.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //fechaInicio.SelectedDate = DateTime.Today;
            this.actualizarListview();
        }

        // Función que se lanza cuando se cambia la fecha de inicio y sirve ajustar la fecha de fin cuando la de inicio es superior a ella.
        private void ajustarFechaFin(object sender, SelectionChangedEventArgs e)
        {
            if (this.comparacionFechas() == 1)
            {
                fechaFin.SelectedDate = fechaInicio.SelectedDate;
            }
        }

        // Función que sirve para comparar dos fechas. Si el resultado de la comparación es = 1 significa que la fecha de fin es inferior a la de inicio
        private int comparacionFechas()
        {
            this.recogerDatosFechas();
            int compararFechas = DateTime.Compare(inicio, fin);
            return compararFechas;
        }

        // Función para recoger fechas y transformarlas a DateTime, además del número de días del rango
        private void recogerDatosFechas() {
            // Recogida de fechas y transoformo el DateTime completo a su formato de fecha corto
            string fechaDesde = fechaInicio.SelectedDate.Value.Date.ToShortDateString();
            string fechaHasta = fechaFin.SelectedDate.Value.Date.ToShortDateString();
            // Transformación de fechas para poder compararlas y usarlas posteriormente
            inicio = DateTime.Parse(fechaDesde);
            fin = DateTime.Parse(fechaHasta);
            // Se suma un día porque no se tiene en cuenta el día de hoy
            numDias = (fin - inicio).TotalDays + 1;
        }
        // Función para mostrar las fechas cuando se pulsa el botón de ejecutar
        private void mostrarFechas(object sender, RoutedEventArgs e) {
            if (this.comparacionFechas() != 1)
            {
                this.actualizarListview();
            }
            else {
                Mensajes.mensajeInformacion("La fecha de fin no puede ser inferior a la de inicio", "fechas incorrectas");
            }
            
        }
        // Función para actualizar el listview
        private void actualizarListview() {

            // Actualizo el valor de las fechas de los datepickers y el número de días del rango
            this.recogerDatosFechas();
            // Lista a mostrar en el listview
            List<Estadisticas> listaEstadisticas = new List<Estadisticas>();

            //Estadísticas anuales
            DateTime inicioAnyo = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime finAnyo = new DateTime(DateTime.Now.Year, 12, 31);

            // Recogida del código de todas las actividades ejecutadas en el rango de fechas
            List<Contener> listaActividades = ContenerController.actividadesEjecutadasRango(dni,inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"));

            // Se recorre la lista de actividades para crear la lista a mostrar
            foreach (Contener item in listaActividades) {
                string nombre = ActividadController.obtener(item.Codigo_actividad)[0].Nombre;
                
                // Estadísticas del rango seleccionado
                int numRepeticiones = ContenerController.totalRepeticiones(dni, item.Codigo_actividad, inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"))[0].Total;
                double mediaRepeticiones = numRepeticiones / numDias;

                // Estadísticas anuales
                int numRepetcionesAnual = ContenerController.totalRepeticiones(dni,item.Codigo_actividad, inicioAnyo.ToString("yyyy-MM-dd"), finAnyo.ToString("yyyy-MM-dd"))[0].Total;
                double mediaRepeticionesAnual = numRepetcionesAnual / this.diasAnyoEnCurso();

                //Añadir ítem a la lista
                listaEstadisticas.Add(new Estadisticas() { Actividad=nombre, Total = numRepeticiones,  MediaRepeticiones = Math.Round(mediaRepeticiones,2), MediaAnual = Math.Round(mediaRepeticionesAnual,2) });
            }
            listViewRangoActividades.ItemsSource = listaEstadisticas;
        }
        // Función que comprueba si el año es bisiesto y devuelve el número de días apropiado
        private double diasAnyoEnCurso() {
            double diasAnyo;

            int year = Convert.ToInt32(DateTime.Now.Year);
            if ((year % 400 == 0 || year % 100 != 0) && (year % 4 == 0))
            {
                diasAnyo = 366;
            }
            else
            {
                diasAnyo = 365;
            }
            return diasAnyo;
        }
    }

    public class Estadisticas {
        private string actividad;
        private int total;
        private double mediaRepeticiones;
        private double mediaAnual;

        public Estadisticas() { }

        public string Actividad { get => actividad; set => actividad = value; }
        public int Total { get => total; set => total = value; }
        public double MediaRepeticiones { get => mediaRepeticiones; set => mediaRepeticiones = value; }
        public double MediaAnual { get => mediaAnual; set => mediaAnual = value; }
    }
}
