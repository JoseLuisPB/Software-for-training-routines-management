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
using Proyecto_Final_DAM.vistaAdmin;
using Proyecto_Final_DAM.vistaEntrenador;
using Proyecto_Final_DAM.vistaAlumno;

namespace Proyecto_Final_DAM
{
    /// <summary>
    /// Lógica de interacción para InicioUsuario.xaml
    /// </summary>
    public partial class InicioUsuario : Window
    {
        public InicioUsuario(int numRoles)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            nombre.Text = Sesion.Nombre + " " + Sesion.Apellidos;
            this.cargarComboBox();
            this.mostrarPantalla(numRoles);

            // Comprobamos si el usuario necesita o no necesita cambiar de contraseña
            if (UsuarioController.obtener(Sesion.Dni)[0].Cambiar_password) {
                CambiarPassword pass = new CambiarPassword(Sesion.Dni, "editar");
                pass.ShowDialog();
            }
        }

        //Función para cargar el combobox
        public void cargarComboBox()
        {
            // Recupero los roles de la BD
            List<Tener> listaCodigosRol = TenerController.obtener(Sesion.Dni);
            //Cremo mi lista para mostrar en el combobox
            var listaRoles = new List<string>();

            foreach (Tener item in listaCodigosRol)
            {
                // Obtener código del rol
                int codigo = item.Codigo_rol;
                // Obtener nombre del rol
                String nombreRol = RolController.obtener(codigo)[0].Nombre;
                listaRoles.Add(nombreRol);
            }
            roles.ItemsSource = listaRoles;
        }
        //Función para mostrar la pantalla de inicio en el caso de que el usuario sólo tenga 1 rol.
        public void mostrarPantalla(int numRoles)
        {
            if (numRoles == 1)
            {
                // Recojo el código del rol para saber que pantalla tengo que cargar
                int codigoRol = TenerController.obtener(Sesion.Dni)[0].Codigo_rol;
                if (codigoRol == 0)
                {
                    frameInicio.Content = new InicioAdmin(this);
                }
                else if (codigoRol == 1)
                {
                    frameInicio.Content = new InicioEntrenador(this);
                }
                else if (codigoRol == 2)
                {
                    frameInicio.Content = new InicioAlumno(this);
                }
                roles.SelectedIndex = 0;
            }
        }
        //Función para recoger los cambios de selección del combobox y mostrar la pantalla acorde al rol seleccionado en el combobox
        private void cambioEleccionCombobox(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Cogemos el texto actual del combobox
            string textoCB = (sender as ComboBox).SelectedItem as string;
            if (textoCB.Equals("administrador"))
            {
                frameInicio.Content = new InicioAdmin(this);
            }
            else if (textoCB.Equals("entrenador"))
            {
                frameInicio.Content = new InicioEntrenador(this);
            }
            else if (textoCB.Equals("alumno"))
            {
                frameInicio.Content = new InicioAlumno(this);
            }
        }
        //Función para salir a la pantalla de login
        private void botonSalir(object sender, RoutedEventArgs e) {
            MainWindow pantallaLogin = new MainWindow();
            pantallaLogin.Show();
            this.Close();
        }
    }                           
}
    
