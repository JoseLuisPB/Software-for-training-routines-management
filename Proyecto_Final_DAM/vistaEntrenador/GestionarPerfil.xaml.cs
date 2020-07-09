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

namespace Proyecto_Final_DAM.vistaEntrenador
{
    /// <summary>
    /// Lógica de interacción para GestionarPerfil.xaml
    /// </summary>
    public partial class GestionarPerfil : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioEntrenador ventanaInicioEntrenador;

        public GestionarPerfil(InicioUsuario ventanaRecibidaInicioUsuario, InicioEntrenador ventanaRecibidaInicioEntrenador)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Gestionar perfil";
            ventanaInicioEntrenador = ventanaRecibidaInicioEntrenador;

            Entrenador trainer = EntrenadorController.obtener(Sesion.Dni)[0];
            perfil.Text = trainer.Biografia;
        }
        // Función para actualizar los datos del perfil
        private void botonGuardar(object sender, RoutedEventArgs e)
        {
            string biografia = perfil.Text;
            EntrenadorController.actualizar(Sesion.Dni, biografia);
        }
    }
}
