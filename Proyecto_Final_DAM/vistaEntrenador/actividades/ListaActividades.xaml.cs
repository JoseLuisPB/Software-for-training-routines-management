
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

namespace Proyecto_Final_DAM.vistaEntrenador.actividades
{
    /// <summary>
    /// Lógica de interacción para ListaActividades.xaml
    /// </summary>
    public partial class ListaActividades : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioActividades ventanaInicioActividades;
        public ListaActividades(InicioUsuario ventanaRecibidaInicioUsuario, InicioActividades ventanaRecibidaInicioActividades)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioActividades = ventanaRecibidaInicioActividades;
            ventanaInicioUsuario.titulo.Text = "Lista de actividades";
            this.cargarComboBox();
            cbFiltro.SelectedIndex = 0;
            this.cargarListView(ActividadController.listar());  
        }
        // Función para cargar  los datos en el listview
        private void cargarListView(List<Actividad> listaRecibida) {
            // Creación de la nueva lista
            List<DatosListaActividades> listaActividadesDefinitiva = new List<DatosListaActividades>();
            // Se recorre la lista pasada para ajustar los datos
            foreach (Actividad item in listaRecibida) {
                string textoActiva ="Sí";

                if (item.Activa == false)
                {
                    textoActiva = "No";
                }

                listaActividadesDefinitiva.Add(new DatosListaActividades() {Codigo = item.Codigo, Nombre = item.Nombre, TipoActividad = item.Tipo, Dificultad = item.Nivel, Activa = item.Activa, ActivaString = textoActiva });
            }

            listViewActividades.ItemsSource = listaActividadesDefinitiva;
        }

        // Botón para pasar a la pantalla de gestión de la actividad
        private void botonGestion(object sender, RoutedEventArgs e)
        {
            // Recogemos los datos del boton que se ha pulsado
            Button botonPulsado = sender as Button;
            DatosListaActividades activity = botonPulsado.CommandParameter as DatosListaActividades;

            ventanaInicioActividades.frameActividades.Content = new GestionActividades(ventanaInicioUsuario,ventanaInicioActividades,activity.Codigo,"editar");
        }
        // GESTIÓN DEL COMBOBOX
        // Función para cargar el combobox
        private void cargarComboBox()
        {
            //Creo mi lista para mostrar en el combobox y que al cambiar de selección coja el texto con normalidad.
            List<Actividad> listaTipoActividades = ActividadController.tipoActividad();
            var listaCB = new List<string>();
            // El valor todas no está en la query que se trae de la BD, así que se añade a mano
            listaCB.Add("todas");
            // Se añaden a la lista todos los tipos de actividades que ha devuelto la BD
            foreach (Actividad item in listaTipoActividades)
            {
                listaCB.Add(item.Tipo);
            }
            cbFiltro.ItemsSource = listaCB;
        }

        // Función para gestionar los cambios de elección en el comboBox
        private void cambioSeleccionComboBox(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string textoCB = (sender as ComboBox).SelectedItem as string;

            // Se filtra la lista a mostrar
            if (textoCB.Equals("todas"))
            {
                this.cargarListView(ActividadController.listar());
            }
            else
            {
                this.cargarListView(ActividadController.actividadesPorTipo(textoCB));

            }
            //this.cargaListViewActividadesDisponibles(textoCB);
        }
    }
    public class DatosListaActividades {
        private int codigo;
        private string nombre;
        private string tipoActividad;
        private string dificultad;
        private Boolean activa;
        private string activaString;

        public DatosListaActividades() { }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string TipoActividad { get => tipoActividad; set => tipoActividad = value; }
        public string Dificultad { get => dificultad; set => dificultad = value; }
        public bool Activa { get => activa; set => activa = value; }
        public string ActivaString { get => activaString; set => activaString = value; }
        
    }
}
