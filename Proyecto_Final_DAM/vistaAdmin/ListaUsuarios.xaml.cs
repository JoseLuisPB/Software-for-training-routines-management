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

namespace Proyecto_Final_DAM.vistaAdmin
{
    /// <summary>
    /// Lógica de interacción para ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Page
    {
        InicioAdmin ventanaInicioAdmin; //Objeto para acceder a la propiedades de la pantalla InicioAdmin
        InicioUsuario ventanaInicioUsuario; //Objeto para acceder a la propiedades de la pantalla InicioUsuario
        List<DatosListViewUsuario> listaDeUsuarios;
        int eleccionCombobox = 1;
        public ListaUsuarios(InicioAdmin ventanaRecibidaInicioAdmin, InicioUsuario ventanaRecibidaInicioUsuario)
        {
            InitializeComponent();
            ventanaInicioAdmin = ventanaRecibidaInicioAdmin;
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Lista de usuarios";
            this.cargarComboBox();
            cbFiltro.SelectedIndex = 1;
            this.cargarDatosListView(UsuarioController.listaActivos());
        }

        //Función para cargar los datos del listView
        private void cargarDatosListView(List<Usuario> lista) {
            string estadoUsuario;
            // Creo o reinicio la lista
            listaDeUsuarios = new List<DatosListViewUsuario>();

            foreach (Usuario item in lista) {
                // Valor que se mostrará en la tabla, en el campo activo
                if (item.Activo == true)
                {
                    estadoUsuario = "Sí";
                }
                else {
                    estadoUsuario = "No";
                }

                listaDeUsuarios.Add(new DatosListViewUsuario() {NombreCompleto = item.Nombre + " " + item.Apellidos, Dni = item.Dni, Activo = item.Activo, ActivoString = estadoUsuario});

            }
            listaUsuarios.ItemsSource = listaDeUsuarios;
        }
        // Función para acceder a la pantalla de gestion del usuario
        private void botonGestion(object sender, RoutedEventArgs e)
        {
            // Recogemos los datos del boton que se ha pulsado
            Button botonPulsado = sender as Button;
            DatosListViewUsuario user = botonPulsado.CommandParameter as DatosListViewUsuario;
            //Generamos el frame
            ventanaInicioAdmin.framePrincipal.Content = new GestionarUsuario(ventanaInicioUsuario, ventanaInicioAdmin, user.Dni, "editar");
        }
        //Función para cargar el combobox
        public void cargarComboBox()
        {
            var listaCB = new List<string>();
            listaCB.Add("Todos");
            listaCB.Add("Sólo activos");
            listaCB.Add("Sólo inactivos");
            cbFiltro.ItemsSource = listaCB;
        }
        // Función para gestionar el cambio de elección en el combobox
        private void cambioCombobox(object sender, SelectionChangedEventArgs e)
        {
            eleccionCombobox = (sender as ComboBox).SelectedIndex;
            List<Usuario> listaEleccionCB = new List<Usuario>();
            // Filtrado de datos según el valor seleccionado en el combobox
            if (eleccionCombobox == 0)
            {
                listaEleccionCB = UsuarioController.listar();
            }
            else if (eleccionCombobox == 1)
            {
                listaEleccionCB = UsuarioController.listaActivos();
            }
            else {
                listaEleccionCB = UsuarioController.listaInactivos();
            }
            this.cargarDatosListView(listaEleccionCB);
        }

        // Función para acceder a la pantalla de gestion del usuario
        private void botonBuscar(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = tbFiltro.Text;
            string textoAjustado = "%" + textoBusqueda + "%";
            this.cargarDatosListView(UsuarioController.busquedaNombre(textoAjustado));
        }
    }
    public class DatosListViewUsuario {
        private string nombreCompleto;
        private string dni;
        private Boolean activo;
        private string activoString;

        public DatosListViewUsuario() { 
        }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Dni { get => dni; set => dni = value; }
        public bool Activo { get => activo; set => activo = value; }
        public string ActivoString { get => activoString; set => activoString = value; }
    }
}
