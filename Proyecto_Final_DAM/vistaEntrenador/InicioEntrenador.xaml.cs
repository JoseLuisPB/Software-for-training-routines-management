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
using Proyecto_Final_DAM.vistaEntrenador;
using Proyecto_Final_DAM.vistaEntrenador.actividades;
using Proyecto_Final_DAM.vistaEntrenador.rutinas;
using Proyecto_Final_DAM.vistaEntrenador.asignar;
using Proyecto_Final_DAM.controlador;
using Proyecto_Final_DAM.modelo;

namespace Proyecto_Final_DAM.vistaEntrenador
{
    /// <summary>
    /// Lógica de interacción para InicioEntrenador.xaml
    /// </summary>
    public partial class InicioEntrenador : Page
    {
        InicioUsuario ventanaInicioUsuario;
        public InicioEntrenador(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            this.comprobarEntrenador();
            framePrincipal.Content = new InicioAsignarRutinas(ventanaInicioUsuario, this);
        }

        // Función para acceder a los datos específicos del perfil del entrenador
        private void botonPerfil(object sender, RoutedEventArgs e) {
            framePrincipal.Content = new GestionarPerfil(ventanaInicioUsuario, this);
        }
        // Función para acceder a la gestión de actividades
        private void botonActividades(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new InicioActividades(ventanaInicioUsuario, this);
        }
        // Función para acceder a la gestión de rutinas
        private void botonRutinas(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new InicioRutinas(ventanaInicioUsuario);
        }
        // Función para acceder a la gestión de rutinas
        private void botonAsignarRutinas(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new InicioAsignarRutinas(ventanaInicioUsuario, this);
        }

        // Función que comprueba si el entrenador existe en la base de datos
        private void comprobarEntrenador() {

            try
            {
                Entrenador trainer = EntrenadorController.obtener(Sesion.Dni)[0];
            }
            catch (ArgumentOutOfRangeException) // Sólo ocurre la primera vez que el entrenador trata de modificar su biograría, ya que no existe el registro en la base de datos
            {
                EntrenadorController.insertar(Sesion.Dni,"");
                framePrincipal.Content = new GestionarPerfil(ventanaInicioUsuario, this);
            }
        }
    }
}
