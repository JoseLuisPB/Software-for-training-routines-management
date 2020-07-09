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

namespace Proyecto_Final_DAM.vistaAlumno
{
    /// <summary>
    /// Lógica de interacción para DatosEntrenador.xaml
    /// </summary>
    public partial class DatosEntrenador : Window
    {
        string dni;
        public DatosEntrenador(string ident, string nombre)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            dni = ident;
            cabecera.Text = "Datos de " + nombre;
            perfil.Text = EntrenadorController.obtener(dni)[0].Biografia;
        }
        // Función para cerrar el cuadro de diálogo
        private void botonSalir(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
