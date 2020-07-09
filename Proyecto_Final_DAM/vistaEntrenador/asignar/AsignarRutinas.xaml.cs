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

namespace Proyecto_Final_DAM.vistaEntrenador.asignar
{
    /// <summary>
    /// Lógica de interacción para AsignarRutinas.xaml
    /// </summary>
    public partial class AsignarRutinas : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioAsignarRutinas ventanaInicioAsignarRutinas;
        string dni;
        Alumno alumne;
        List<ElementosListViewRutinas> listaRutinasDisponibles;
        List<ElementosListViewRutinas> listaRutinasAsignadas;

        ElementosListViewRutinas rutinaDisponibleSeleccionada = null;
        ElementosListViewRutinas rutinaAsignadaSeleccionada = null;

        public AsignarRutinas(InicioUsuario ventanaRecibidaIniciousuario, InicioAsignarRutinas ventanaRecibidaAsignarRutinas, string ident, string nombre)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaIniciousuario;
            ventanaInicioUsuario.titulo.Text = "Asignar rutina a alumno";
            ventanaInicioAsignarRutinas = ventanaRecibidaAsignarRutinas;

            // Gestion datos del alumno
            dni = ident;
            nombreAlumno.Text = nombre;
            alumne = AlumnoController.obtener(dni)[0];
            dolencias.Text = alumne.Dolencias;
            objetivos.Text = alumne.Objetivo;

            // Creación de las listas
            listaRutinasDisponibles = new List<ElementosListViewRutinas>();
            listaRutinasAsignadas = new List<ElementosListViewRutinas>();

            // Caso de acceso como alumno
            if (ventanaInicioUsuario.roles.SelectedItem.Equals("alumno")) {
                spAlumno.Visibility = Visibility.Collapsed;
                spDolencias.Visibility = Visibility.Collapsed;
                spObjetivos.Visibility = Visibility.Collapsed;
                ventanaInicioUsuario.titulo.Text = "Asignar rutina";
            }

            this.cargarRutinasDisponibles();
            
        }

        // Función que muestra las actividades que contiene una rutina
        private void botonMostrarActividadesRutina(object sender, RoutedEventArgs e)
        {
            Button botonPulsado = sender as Button;
            ElementosListViewRutinas routine = botonPulsado.CommandParameter as ElementosListViewRutinas;
            string codigoRutinaSeleccionada = Convert.ToString(routine.Codigo_rutina);
            DetalleRutinas pantallaDetalleRutinas = new DetalleRutinas(codigoRutinaSeleccionada);
            pantallaDetalleRutinas.ShowDialog();
        }

        // GESTIÓN DE LISTVIEWS
        // GESTION LISTAS DE LOS LISTVIEWS
        // Función para cargar las rutinas disponibles
        private void cargarRutinasDisponibles() {
           List<Rutina> listaRutinasBD = RutinaController.listaRutinasEntrenador(Sesion.Dni);
            foreach (Rutina item in listaRutinasBD) {
                listaRutinasDisponibles.Add(new ElementosListViewRutinas() { Nombre = item.Nombre, Codigo_rutina=item.Codigo});
            }
            
            listViewRutinasDisponibles.ItemsSource = listaRutinasDisponibles;
        }
        // COMPROBAR QUE HAYA UN ITEM SELECCIONADO
        // Función para guardar la información de la rutina disponible seleccionada
        private void listViewRutinaDisponibleSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rutinaDisponibleSeleccionada = (ElementosListViewRutinas)listViewRutinasDisponibles.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }
        // Función para guardar la información de la rutina asignada seleccionada
        private void listViewRutinaAsignadaSeleccionada(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rutinaAsignadaSeleccionada = (ElementosListViewRutinas)listViewRutinasAsignadas.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }

        // Función para actualizar el listview
        private void actualizarListView() {
            List<ElementosListViewRutinas> listaRutinasActualizada = new List<ElementosListViewRutinas>();
            foreach (ElementosListViewRutinas item in listaRutinasAsignadas) {
                listaRutinasActualizada.Add(new ElementosListViewRutinas() {Nombre = item.Nombre, Codigo_rutina = item.Codigo_rutina });
            }
            listViewRutinasAsignadas.ItemsSource = listaRutinasActualizada;
        }
        private void anyadirRutina(object sender, RoutedEventArgs e) {
            try
            {
                Boolean existeRutina = false;
                // Recorro la lista de rutinas asignadas para comprobar si el item seleccionado de la lista ya está asignado
                foreach (ElementosListViewRutinas item in listaRutinasAsignadas)
                {
                    // Compruebo si el código de la rutina seleccionada ya está contenido en la lista de rutinas asignadas
                    if (item.Codigo_rutina == rutinaDisponibleSeleccionada.Codigo_rutina)
                    {
                        existeRutina = true;
                    }
                }
                // En el caso de que la rutina no esté en la lista, la asigno
                if (existeRutina == false)
                {
                    listaRutinasAsignadas.Add(new ElementosListViewRutinas() { Nombre = rutinaDisponibleSeleccionada.Nombre, Codigo_rutina = rutinaDisponibleSeleccionada.Codigo_rutina});
                }
                this.actualizarListView();
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de rurtinas disponibles", "Item no seleccionado");
            }
        }
        private void quitarRutina(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se copia la lista que voy a recorrer
                List<ElementosListViewRutinas> listaAuxiliar = listaRutinasAsignadas.ToList();
                foreach (ElementosListViewRutinas item in listaAuxiliar)
                {
                    if (item.Codigo_rutina == rutinaAsignadaSeleccionada.Codigo_rutina)
                    {
                        listaRutinasAsignadas.Remove(item);
                    }
                }
                this.actualizarListView();
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de rutinas asignadas", "Item no seleccionado");
            }
        }

        // GESTIONAR FECHAS SELECCIONADAS
        //Función que comprueba si ya existe la rutina en la/s fecha/s seleccionada/s, se le pasa por parámetro la lista de fechas seleccionadas
        private Boolean rutinaAsignada() {
            Boolean existeRutinaEnFecha = false;
            Boolean mensajeMostrado = false; // Variable para controlar que sólo se muestre un mensaje.
            SelectedDatesCollection lista = calendario.SelectedDates;
            // Iteración por la lista de fechas
            for (int i = 0; i < lista.Count; i++) {
                // Se obtiene la lista con las rutinas asignadas en la fecha iterada

                DateTime fechaRecorrida = lista[i];
                List<Realizar> rutinasARealizar = RealizarController.obtener(dni, fechaRecorrida.ToString("yyyy-MM-dd"));
                // Se recorre la lista de rutinas a realizar y se comprueba si la rutina asignada está o no está en la lista
                foreach (Realizar item in rutinasARealizar) { 
                 // Se compara el item actual con los items de la lista de rutinas asignadas
                 foreach(ElementosListViewRutinas elemento in listaRutinasAsignadas)
                    {
                        if (item.Codigo_rutina == elemento.Codigo_rutina && mensajeMostrado == false) {
                            existeRutinaEnFecha = true;
                            Mensajes.mensajeError("La rutina " + elemento.Nombre + " ya ha sido asignada el día " + fechaRecorrida.ToString("dd-MM-yyyy"), "Rutina ya asignada");
                            mensajeMostrado = true;
                        }
                    }
                }
            }
            return existeRutinaEnFecha;
        }
        // GUARDAR
        // Función para guardar las rutinas asignadas
        private void botonGuardar(object sender, RoutedEventArgs e) {
            // Creación de una lista con las fechas seleccionadas
            System.Collections.IList listaDefechas = calendario.SelectedDates;
            // comprobación de que se ha seleccionado como mínimo una fecha.
            if (listaDefechas.Count > 0)
            {
                // Comprobación de que la lista no está en blanco
                if (listaRutinasAsignadas.Count != 0)
                {
                    // Comprabación de que no haya rutinas ya asignadas en una de las fechas
                    if (this.rutinaAsignada() == false)
                    {
                        //Inserción de las rutinas en las fechas indicadas
                        for (int i = 0; i < listaDefechas.Count; i++)
                        {
                            DateTime fechaIterada = (DateTime)listaDefechas[i];
                            // Se recorre la lista de rutinas asignadas y se van añadiendo todas las rutinas a la fecha iterada
                            foreach (ElementosListViewRutinas item in listaRutinasAsignadas)
                            {
                                RealizarController.insertar(dni, item.Codigo_rutina, fechaIterada.ToString("yyyy-MM-dd"), false);
                            }
                        }
                        Mensajes.mensajeInformacion("Rutinas guardadas con éxito", "Guardar");
                        // Reinicio de los datos del listview
                        listaRutinasAsignadas = new List<ElementosListViewRutinas>();
                        // Actualización del listview
                        this.actualizarListView();
                    }
                }
                else {
                    Mensajes.mensajeInformacion("La lista de rutinas asignadas está vacía", "Lista vacía");
                }
            }
            else {
                Mensajes.mensajeError("Selecciona una o varias fechas para asignar las rutinas", "Fecha no seleccionada");
            }
        }

        // Función para liberar el mouse y evitar el doble click
        private void liberarMouse(object sender, MouseEventArgs e)
        {
            UIElement originalElement = e.OriginalSource as UIElement;
            if (originalElement != null)
            {
                originalElement.ReleaseMouseCapture();
            }
        }
    }
    public class ElementosListViewRutinas {
        private string nombre;
        private int codigo_rutina;

        public ElementosListViewRutinas() { 
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Codigo_rutina { get => codigo_rutina; set => codigo_rutina = value; }
    }
}
