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
    /// Lógica de interacción para GestionarPerfilAlumno.xaml
    /// </summary>
    public partial class GestionarPerfilAlumno : Page
    {
        InicioUsuario ventanaInicioUsuario;
        Alumno alumne;
        public GestionarPerfilAlumno(InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Gestionar perfil";
            alumne = AlumnoController.obtener(Sesion.Dni)[0];
            dolencias.Text = alumne.Dolencias;
            objetivo.Text = alumne.Objetivo;
        }
        // Función para guardar los datos del perfil
        private void botonGuardar(object sender, RoutedEventArgs e)
        {
            AlumnoController.actualizar(alumne.Dni_usuario,alumne.Entrenador_asignado, dolencias.Text,objetivo.Text);
        }
    }
}
