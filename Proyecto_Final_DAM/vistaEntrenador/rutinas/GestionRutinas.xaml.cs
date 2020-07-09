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

namespace Proyecto_Final_DAM.vistaEntrenador.rutinas
{
    /// <summary>
    /// Lógica de interacción para GestionRutinas.xaml
    /// </summary>
    public partial class GestionRutinas : Page
    {
        
        InicioUsuario ventanaInicioUsuario;
        InicioRutinas ventanaInicioRutinas;
        int codigoRutina;
        String tipoDeAccion;
        Rutina routine;

        // Listas
        List<ActividadesListView> listaActividadesAsignadasInicial;
        List<ActividadesListView> listaActividadesAsignadasActualizada;
        // Lista que recoge los elementos que haya en el listview de actividades asignadas
        List<ActividadesListView> elementosEnListView;

        //Selecciones --> Guardan la información del objeto seleccionado
        Actividad ActividadDisponibleSeleccionada = null;
        ActividadesListView ActividadAsignadaSeleccionada = null;
        public GestionRutinas(InicioUsuario ventanaRecibidaInicioUsuario, InicioRutinas ventanaRecibidaInicioRutinas, int codigo, string accion)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioRutinas = ventanaRecibidaInicioRutinas;
            codigoRutina = codigo;
            tipoDeAccion = accion;

            // Creación de las listas
            listaActividadesAsignadasInicial = new List<ActividadesListView>();
            listaActividadesAsignadasActualizada = new List<ActividadesListView>();
                
            // Pantalla de crear
            if (tipoDeAccion.Equals("crear"))
            {
                ventanaInicioUsuario.titulo.Text = "Crear rutina";
                activa.IsChecked = true;
                gestionActividadesRutina.Visibility = Visibility.Hidden;
                gestionActividadesRutina.SetValue(Grid.RowProperty, 2);
                botonera.SetValue(Grid.RowProperty, 1);
            }
            // Pantalla de editar
            else {
                // Carga del combobox
                this.cargarComboBox();
                // Se indica que quiero el elemento en la posición 0 del combobox y por lo tanto se ejecuta la función de cambioSeleccionCombobox
                tipoActividad.SelectedIndex = 0;
                ventanaInicioUsuario.titulo.Text = "Gestionar rutina";
                // Creación de un objeto que contiene los datos de la rutina
                routine = RutinaController.obtener(codigoRutina)[0];
                // Datos mostrados por pantalla
                nombre.Text = routine.Nombre;
                this.marcarRadioButton(routine.Activa);
                this.cargaListViewActividadesAsignadas();
                // Se crea la lista de actividades actualizadas como copia de la de actividades asignadas de inicio
                listaActividadesAsignadasActualizada = listaActividadesAsignadasInicial.ToList();
            }
        }
        // COMBOBOX
        //Función para cargar el combobox
        public void cargarComboBox()
        {
            //Creo mi lista para mostrar en el combobox y que al cambiar de selección coja el texto con normalidad.
            List<Actividad> listaTipoActividades = ActividadController.tipoActividad();
            var listaCB = new List<string>();
            // El valor todas no está en la query que se trae de la BD, así que se añade a mano
            listaCB.Add("todas");
            // Se añaden a la lista todos los tipos de actividades que ha devuelto la BD
            foreach (Actividad item in listaTipoActividades) {
                listaCB.Add(item.Tipo);
            }
            tipoActividad.ItemsSource = listaCB;
        }
        // Función para gestionar los cambios de elección en el comboBox
        private void cambioSeleccionComboBox(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string textoCB = (sender as ComboBox).SelectedItem as string;
            this.cargaListViewActividadesDisponibles(textoCB);
        }

        // CARGA ELEMENTO LISTVIEWS
        // Función para actualizar el listview con las actividades disponibles, se le pasa por parámetro el valor del combobox
        private void cargaListViewActividadesDisponibles(string tipoActividad)
        {
            // Se crea la lista de actividades disponibles con las actividades correspondientes al tipo
            List<Actividad> listaActividades;

            if (tipoActividad.Equals("todas"))
            {
                listaActividades = ActividadController.listar();
            }
            else
            {
                listaActividades= ActividadController.actividadesPorTipo(tipoActividad);
            }
           
            listViewActividadesDisponibles.ItemsSource = listaActividades;
        }
        // Función para actuliazar el listview con las actividades asignadas
        private void cargaListViewActividadesAsignadas()
        {
            // Carga de las actividades que contiene la rutina seleccionada
            List<Contener> lista = ContenerController.listaActividadesRutina(routine.Codigo);
            // La lista de actividades es recorrida para crear una lista de objetos de la clase ActividadesListView
            foreach (Contener item in lista)
            {
                // Creación de objeto de la clase Actividad para recuperar valores para ciertos campos
                Actividad activity = ActividadController.obtener(item.Codigo_actividad)[0];
                // Se añaden elementos a la lista de objetos de la clase ActividadesListView
                listaActividadesAsignadasInicial.Add(new ActividadesListView() { Nombre = activity.Nombre, Codigo_actividad = item.Codigo_actividad, TipoActividad = activity.Tipo, Series = Convert.ToString(item.Series), Repeticiones = Convert.ToString(item.Repeticiones) });
            }
            // Asignación de la lista al ListView
            listViewActividadesAsignadas.ItemsSource = listaActividadesAsignadasInicial;
        }
        // FUNCIONAMIENTO LISTVIEWS
        // RECOGER ITEM SELECCIONADO EN EL LISTVIEW
        // Función que recoge los datos del item seleccionado en la lista Actividades disponibles.
        private void seleccionActividadDisponible(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ActividadDisponibleSeleccionada = (Actividad)listViewActividadesDisponibles.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }
        // Función que recoge los datos del item seleccionado en la lista Actividades asignadas.
        private void seleccionActividadAsignada(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ActividadAsignadaSeleccionada = (ActividadesListView)listViewActividadesAsignadas.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
        }

        // FUNCIONAMIENTO DE LOS BOTONES AÑADIR Y QUITAR
        // Función seleccionar actividad en actividad disponible
        private void anyadirActividad(object sender, RoutedEventArgs e)
        {
            Boolean existeActividad = false;
            try {
                // Se recorre la lista para ver si la actividad seleccionada existe en la lista de actividades asignadas
                foreach (ActividadesListView item in listaActividadesAsignadasActualizada) {
                    if (item.Codigo_actividad == ActividadDisponibleSeleccionada.Codigo) {
                        existeActividad = true;
                    }
                }
                if (existeActividad == false) {
                    // Se recogen todos los elementos del listview de actividades asignadas
                    elementosEnListView = (List<ActividadesListView>)listViewActividadesAsignadas.ItemsSource;
                    // Se recorre la lista de elementos que hay actualmente en el listview de actividades asignadas
                    foreach (ActividadesListView item in elementosEnListView)
                    {
                        // Se recorre la lista actualizada de actividades asignadas para actualizar los valores de series y repeticiones que se hayan insertado
                        foreach (ActividadesListView elemento in listaActividadesAsignadasActualizada)
                        {
                            // Actualización de de series y repeticiones
                            if (item.Codigo_actividad == elemento.Codigo_actividad)
                            {
                                elemento.Series = item.Series;
                                elemento.Repeticiones = item.Repeticiones;
                            }
                        }
                    }
                    // Se añade la nueva actividad con el número de series y repeticiones = 0, ya que el valor se insertará directamente en el listview
                    listaActividadesAsignadasActualizada.Add(new ActividadesListView() { Nombre = ActividadDisponibleSeleccionada.Nombre, Codigo_actividad = ActividadDisponibleSeleccionada.Codigo, Series = "0", Repeticiones = "0" });
                    this.actualizarlistViewActividadesAsignadas();
                }
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de actividades disponibles", "Item no seleccionado");
            }
            
        }
        private void quitarActividad(object sender, RoutedEventArgs e) {
            try
            {
                // Se copia la lista que se va a recorrer
                List<ActividadesListView> listaAuxiliar = listaActividadesAsignadasActualizada.ToList();
                foreach (ActividadesListView item in listaAuxiliar)
                {
                    if (item.Codigo_actividad == ActividadAsignadaSeleccionada.Codigo_actividad)
                    {
                        listaActividadesAsignadasActualizada.Remove(item);
                    }
                }
                // Se recogen todos los elementos que haya en el listview de actividades asignadas
                elementosEnListView = (List<ActividadesListView>)listViewActividadesAsignadas.ItemsSource;
                foreach (ActividadesListView item in elementosEnListView)
                {
                    // Se recorre la lista actualizada de actividades asignadas para hacer un set de los valores de las repeticiones y series insertadas en el listview
                    foreach (ActividadesListView elemento in listaActividadesAsignadasActualizada)
                    {
                        // Cuando la actividad de la lista actualizada llega a la actividad que hay en el list view ejecuta este if
                        if (item.Codigo_actividad == elemento.Codigo_actividad)
                        {
                            // Esta parte actualiza los campos series y repeticiones de la actividad de la lista actualizada
                            elemento.Series = item.Series;
                            elemento.Repeticiones = item.Repeticiones;
                        }
                    }
                }
                this.actualizarlistViewActividadesAsignadas();
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de actividades asignadas", "Item no seleccionado");
            }
        }
        // Función para actualizar la lista de roles
        private void actualizarlistViewActividadesAsignadas()
        {
            // Creo una nueva lista que será la que mostraré en el listview de actividades asignadas
            List<ActividadesListView> listaElementosListViewActualizada = new List<ActividadesListView>();
            // Relleno la nueva lista con los roles que tenga en la lista de roles asignados actualizada, ordeno la lista por orden alfabético
            foreach (ActividadesListView item in listaActividadesAsignadasActualizada.OrderBy(a => a.TipoActividad))
            {
                listaElementosListViewActualizada.Add(new ActividadesListView() { Nombre = item.Nombre, Codigo_actividad = Convert.ToInt32(item.Codigo_actividad), Series = item.Series, Repeticiones = item.Repeticiones });
            }
            listViewActividadesAsignadas.ItemsSource = listaElementosListViewActualizada;
        }
        
        // RADIOBUTTONS
        // Función para marcar el radiobutton cuando se carga la página
        private void marcarRadioButton(bool estado)
        {
            //Desde la base de datos se envía si la actividad está activa(true) o no lo esta (false), sabiendo esto se marca uno de los dos radiobuttons
            if (estado == true)
            {
                activa.IsChecked = true;
            }
            else
            {
                inactiva.IsChecked = true;
            }
        }
        // Función para recoger el radiobutton que está marcado
        private bool radioButtonActivado()
        {
            if (activa.IsChecked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // GUARDAR
        // Función para guardar los datos
        private void botonGuardar(object sender, RoutedEventArgs e)
        {
            if (tipoDeAccion.Equals("crear"))
            {
                try
                {
                    int codigoUltimaRutina = RutinaController.ultimaRutina()[0].Codigo;
                    RutinaController.insertar(codigoUltimaRutina + 1, nombre.Text, this.radioButtonActivado(), Sesion.Dni);
                    ventanaInicioRutinas.frameRutinas.Content = new GestionRutinas(ventanaInicioUsuario, ventanaInicioRutinas, codigoUltimaRutina + 1, "editar");
                }
                catch (System.ArgumentOutOfRangeException) // Sólo sucede al crear la primera rutina, ya que la base de datos no tiene ninguna entrada
                {
                    RutinaController.insertar(1, nombre.Text, this.radioButtonActivado(), Sesion.Dni);
                    ventanaInicioRutinas.frameRutinas.Content = new GestionRutinas(ventanaInicioUsuario, ventanaInicioRutinas, 1, "editar");
                }
                    

            }
            else
            {
                //recorrer toda la lista de items antes de guardar
                elementosEnListView = (List<ActividadesListView>)listViewActividadesAsignadas.ItemsSource;

                Boolean todoNumeros = true;
                foreach (ActividadesListView item in elementosEnListView)
                {
                    // Comprobación de que los valores de las series y repeticiones son numéricos
                    int i;
                    if (!int.TryParse(item.Series, out i))
                    {
                        todoNumeros = false;
                    }
                    else if (!int.TryParse(item.Repeticiones, out i)){
                        todoNumeros = false;
                    }   
                }
                if (todoNumeros == true)
                {
                    // Comprobación de que la rutina que se trata de modificar no ha sido ejecutada por ningún alumno
                    Boolean rutinaEjecutada = false;
                    try
                    {
                        rutinaEjecutada = RealizarController.estadoRutina(routine.Codigo)[0].Ejecutada;
                    }
                    catch (System.ArgumentOutOfRangeException) { 
                    
                    }

                    if (rutinaEjecutada == false)
                    {
                        if (Mensajes.mensajeSiNo("¿Deseas guardar los cambios realizados?", "Guardar cambios") == true)
                        {
                            // Eliminar todas las actividades asociadas a la rutina antes de guardar 
                            ContenerController.eliminar(routine.Codigo);
                            foreach (ActividadesListView item in elementosEnListView)
                            {
                                ContenerController.insertar(codigoRutina, item.Codigo_actividad, Convert.ToInt32(item.Series), Convert.ToInt32(item.Repeticiones), Convert.ToInt32(item.Series) * Convert.ToInt32(item.Repeticiones));
                            }
                            RutinaController.actualizar(routine.Codigo, nombre.Text, this.radioButtonActivado(), routine.Dni_usuario);
                            ventanaInicioRutinas.frameRutinas.Content = new ListaRutinas(ventanaInicioUsuario, ventanaInicioRutinas);
                        }
                    }
                    else {
                        Mensajes.mensajeInformacion("La rutina ya ha sido ejecutada por al menos un alumno y no se puede modificar", "Rutina ya ejecutada");
                    }
                }
                else {
                    Mensajes.mensajeInformacion("Los datos del número de repeticiones y de series correspondientes a la lista de actividades asignadas han de ser numéricos", "Formato datos incorrecto");
                }
            }
        }
    }
    // Objeto para gestionar los datos que se mostrarán en el listview de actividades asignadas
    public class ActividadesListView {
        private string nombre;
        private int codigo_actividad;
        private string tipoActividad;
        private string nivelDificultad;
        private string series;
        private string repeticiones;

        public ActividadesListView()
        {

        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Codigo_actividad { get => codigo_actividad; set => codigo_actividad = value; }
        public string TipoActividad { get => tipoActividad; set => tipoActividad = value; }
        public string NivelDificultad { get => nivelDificultad; set => nivelDificultad = value; }
        public string Series { get => series; set => series = value; }
        public string Repeticiones { get => repeticiones; set => repeticiones = value; }
        
    }
}
