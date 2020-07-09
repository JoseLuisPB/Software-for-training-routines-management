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

namespace Proyecto_Final_DAM.vistaEntrenador.rutinas
{
    /// <summary>
    /// Lógica de interacción para ListaRutinas.xaml
    /// </summary>
    public partial class ListaRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioRutinas ventanaInicioRutinas;
        public ListaRutinas(InicioUsuario ventanaRecibidaInicioUsuario, InicioRutinas ventanaRecibidaInicioRutinas)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioRutinas = ventanaRecibidaInicioRutinas;
            ventanaInicioUsuario.titulo.Text = "Lista de rutinas";

            // Cambio de color de fondo para ajustarlo al del alumno
            if (ventanaInicioUsuario.roles.Text.Equals("alumno"))
            {
                BrushConverter bc = new BrushConverter();
                ventanaRecibidaInicioRutinas.panelInicioRutinas.Background = (Brush)bc.ConvertFrom("#57C7FF");
            }
            // Aunque el método pone entrenador, filtra las rutinas por el dni de quien se ha conectado
            this.cargarListaRutinas(RutinaController.listaRutinasEntrenador(Sesion.Dni));
            
        }

        // Función para gardar la lista de rutinas
        private void cargarListaRutinas(List<Rutina> listaRecibida) {
            List<DatosListaRutinas> listaRutinasDefinitiva = new List<DatosListaRutinas>();
            // Se recorre la lista de rutinas de la base de datos y se crea una nueva lista ajustada a los datos que queremos mostrar
            foreach (Rutina item in listaRecibida) {
                string textoEstado = "Sí";
                if (item.Activa == false) {
                    textoEstado = "No";
                }
                listaRutinasDefinitiva.Add(new DatosListaRutinas() { Codigo = item.Codigo, Nombre = item.Nombre, Activa = item.Activa, ActivaString = textoEstado});
            }
            listViewRutinas.ItemsSource = listaRutinasDefinitiva;
        }
        // Botón para pasar a la pantalla de gestión de la rutina
        private void botonGestion(object sender, RoutedEventArgs e)
        {
            // Recogemos los datos del boton que se ha pulsado
            Button botonPulsado = sender as Button;
            DatosListaRutinas routine = botonPulsado.CommandParameter as DatosListaRutinas;
            ventanaInicioRutinas.frameRutinas.Content = new GestionRutinas(ventanaInicioUsuario, ventanaInicioRutinas, routine.Codigo, "editar");
        }

        // Botón para ejecutar una busqueda de rutinas
        private void botonBuscar(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = nombreRutina.Text;
            string textoAjustado = "%" + textoBusqueda + "%";
            this.cargarListaRutinas(RutinaController.busquedaNombreRutina(textoAjustado, Sesion.Dni));
        }
    }
    public class DatosListaRutinas {
        private int codigo;
        private string nombre;
        private Boolean activa;
        private string activaString;

        public DatosListaRutinas() { }
        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Activa { get => activa; set => activa = value; }
        public string ActivaString { get => activaString; set => activaString = value; }
    }
}
