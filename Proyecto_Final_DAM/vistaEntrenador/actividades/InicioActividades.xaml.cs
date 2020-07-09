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

namespace Proyecto_Final_DAM.vistaEntrenador.actividades
{
    /// <summary>
    /// Lógica de interacción para InicioActividades.xaml
    /// </summary>
    public partial class InicioActividades : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioEntrenador ventanaInicioEntrenador;

        public InicioActividades(InicioUsuario ventanaRecibidaInicioUsuario, InicioEntrenador VentanaRecibidaInicioEntrenador)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioEntrenador = VentanaRecibidaInicioEntrenador;

            frameActividades.Content = new ListaActividades(ventanaInicioUsuario, this);
        }
        // Función para cargar cargar la page de crear una actividad
        private void botonCrear(object sender, RoutedEventArgs e)
        {
            frameActividades.Content = new GestionActividades(ventanaInicioUsuario, this,0,"crear");
        }
        // Función para cargar la page de la lista de actividades
        private void botonListar(object sender, RoutedEventArgs e)
        {
            frameActividades.Content = new ListaActividades(ventanaInicioUsuario,this);
        }
    }
}
