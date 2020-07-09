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

namespace Proyecto_Final_DAM.vistaAdmin
{
    /// <summary>
    /// Lógica de interacción para InicioAdmin.xaml
    /// </summary>
    public partial class InicioAdmin : Page
    {
        InicioUsuario ventanaInicioUsuario;
        public InicioAdmin(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            // Carga de la lista de usuarios al iniciar la sesión como administrador
            framePrincipal.Content = new ListaUsuarios(this, ventanaInicioUsuario);
        }
        // Función para acceder a la lista de los usuarios para gestionarlos
        private void botonGestionar(object sender, RoutedEventArgs e)
        {
            // Para utilizar el frame de inicioUsuario e inicioAdmin pasamos los dos objetos
            framePrincipal.Content = new ListaUsuarios(this, ventanaInicioUsuario);
        }
        // Función para acceder a la pantalla de crear usuarios
        private void botonCrear(object sender, RoutedEventArgs e)
        {
            framePrincipal.Content = new GestionarUsuario(ventanaInicioUsuario, this,"","crear");
        }
    }
}
