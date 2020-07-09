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

namespace Proyecto_Final_DAM.vistaEntrenador.rutinas
{
    /// <summary>
    /// Lógica de interacción para InicioRutinas.xaml
    /// </summary>
    public partial class InicioRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;

        public InicioRutinas(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            frameRutinas.Content = new ListaRutinas(ventanaInicioUsuario,this);
        }
        // Función para cargar el frame donde crear una rutina
        private void botonCrear(object sender, RoutedEventArgs e) {
            frameRutinas.Content = new GestionRutinas(ventanaInicioUsuario, this,0,"crear");
        }
        // Función para cargar el frame donde aparece la lista de rutinas
        private void botonListar(object sender, RoutedEventArgs e) {
            frameRutinas.Content = new ListaRutinas(ventanaInicioUsuario, this);
        }
    }
}
