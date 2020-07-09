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
using Proyecto_Final_DAM.controlador;
using Proyecto_Final_DAM.modelo;

namespace Proyecto_Final_DAM.vistaEntrenador.asignar
{
    /// <summary>
    /// Lógica de interacción para DetalleRutinas.xaml
    /// </summary>
    public partial class DetalleRutinas : Window
    {
        List<DetalleActividades> listaDeActividades;
        int codigoRutina;
        public DetalleRutinas(string codRutinaRecibido)
        {
            InitializeComponent();
            codigoRutina = Convert.ToInt32(codRutinaRecibido);
            listaDeActividades = new List<DetalleActividades>();
            this.cargarActividades();
        }

        // Método para cargar las actividades
        public void cargarActividades() {
            // Listado de actividades que contiene la rutina
            List<Contener> listaActividadesContenidasRutina = ContenerController.listaActividadesRutina(codigoRutina);
            // Creación de los objetos para mostrarlos en la ventana
            foreach (Contener item in listaActividadesContenidasRutina) {
                string nombreActividad = ActividadController.obtener(item.Codigo_actividad)[0].Nombre;
                listaDeActividades.Add(new DetalleActividades() { Nombre = nombreActividad, Series = Convert.ToString(item.Series) ,Repeticiones = Convert.ToString(item.Repeticiones) });
            }
            ActividadesContenidas.ItemsSource = listaDeActividades;
        }

        // Método para cerrar la ventana
        private void botonSalir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    // Clase para controlar los items mostrados en el item control
    public class DetalleActividades {
        private string nombre;
        private string series;
        private string repeticiones;

        public DetalleActividades() { 
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Series { get => series; set => series = value; }
        public string Repeticiones { get => repeticiones; set => repeticiones = value; }
    }
}
