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
using Proyecto_Final_DAM.vistaAlumno;

namespace Proyecto_Final_DAM.vistaAdmin
{
    /// <summary>
    /// Lógica de interacción para GestionarUsuario.xaml
    /// </summary>
    public partial class GestionarUsuario : Page
    {
        Usuario user;
        InicioUsuario ventanaInicioUsuario;
        InicioAdmin ventanaInicioAdmin;
        string tipoOperacion;
        Boolean asignacionRolAlumno = false; //Variable que controla si se ha asignado el rol del alumno

        //Listas
        List<ElementosListViewRoles> listaRolesDisponibles; // Lista con todos los roles existentes
        List<ElementosListViewRoles> listaRolesAsignadosIniciales; // Lista inicial de los roles asignados
        List<ElementosListViewRoles> listaRolesAsignadosActualizada; // Lista final con los roles asignados

        //Selecciones --> Guardan la información del objeto seleccionado
        ElementosListViewRoles rolDisponibleSeleccionado = null;
        ElementosListViewRoles rolAsignadoSeleccionado = null;
        public GestionarUsuario(InicioUsuario ventanaRecibidaInicioUsuario, InicioAdmin ventanaRecibidaInicioAdmin, string identificador, string operacion)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioAdmin = ventanaRecibidaInicioAdmin;
            tipoOperacion = operacion;
            // Ajustamos los datos de la pantalla a la operación seleccionada
            if (operacion.Equals("crear"))
            {
                ventanaInicioUsuario.titulo.Text = "Crear usuario";
                resetPass.Visibility = Visibility.Collapsed;
                resetPass.IsEnabled = false;
                nacimiento.SelectedDate = nacimiento.SelectedDate = DateTime.Today;
                alta.SelectedDate = DateTime.Today;
                activo.IsChecked = true;
                gestionRoles.Visibility = Visibility.Collapsed;
                gestionRoles.SetValue(Grid.RowProperty, 2);
                botonera.SetValue(Grid.RowProperty, 1);
            }
            else
            {
                ventanaInicioUsuario.titulo.Text = "Gestión de usuario";
                user = UsuarioController.obtener(identificador)[0];
                dni.IsReadOnly = true;
                resetPass.Visibility = Visibility.Visible;
                resetPass.IsEnabled = true;
                // Rellenado de campos
                nombre.Text = user.Nombre;
                apellidos.Text = user.Apellidos;
                dni.Text = user.Dni;
                nacimiento.Text = user.Fecha_nacimiento;
                alta.Text = user.Fecha_alta;
                this.marcarRadioButton(user.Activo);

                localidad.Text = user.Localidad;
                direccion.Text = user.Direccion;
                cp.Text = user.Cp;
                pais.Text = user.Pais;
                telefono.Text = user.Telefono;
                email.Text = user.Email;
            }

            //LISTAS

            // Creación de las listas vacías
            // Roles que el usuario tiene asignados al cargar el programa
            listaRolesAsignadosIniciales = new List<ElementosListViewRoles>();
            // Lista con que se actualiza con los roles que se añaden o quitan
            listaRolesAsignadosActualizada = new List<ElementosListViewRoles>();
            // Lista que contiene todos los roles existentes
            listaRolesDisponibles = new List<ElementosListViewRoles>();

            // Añadir elementos a las listas y asignarlas al listview
            if (!tipoOperacion.Equals("crear"))
            {
                this.cargaListasRolesAsignados();
            }
            this.cargaListaRolesDisponibles();

        }

        // GESTION LISTVIEWS
        // Función que recoge los datos del item seleccionado en la lista Roles disponibles.
        private void listViewRolesDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rolDisponibleSeleccionado = (ElementosListViewRoles)listViewRolesDisponibles.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }
        // Función que recoge los datos del item seleccionado en la lista Roles asignados.
        private void listViewRolesAsignados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rolAsignadoSeleccionado = (ElementosListViewRoles)listViewRolesAsignados.SelectedItems[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }

        }
        //Función que actualiza los datos del listview
        private void actualizarlistView()
        {
            // Creo una nueva lista que será la que mostraré en el listview de roles asignados
            List<ElementosListViewRoles> listaElementosListViewActualizada = new List<ElementosListViewRoles>();

            // Relleno la nueva lista con los roles que tenga en la lista de roles asignados actualizada, ordeno la lis lista por orden alfabético
            foreach (ElementosListViewRoles item in listaRolesAsignadosActualizada.OrderBy(a => a.Nombre))
            {
                listaElementosListViewActualizada.Add(new ElementosListViewRoles() { Nombre = item.Nombre, Dni_usuario = Sesion.Dni, Codigo_rol = item.Codigo_rol });
            }
            listViewRolesAsignados.ItemsSource = listaElementosListViewActualizada;
        }

        // GESTIÓN DE ROLES
        // Función para cargar las listas iniciales de roles asignados
        private void cargaListasRolesAsignados()
        {
            //lista con los roles asignados
            List<Tener> rolesAsignados = TenerController.obtener(dni.Text);
            // Recorro la lista de roles asignados y creo los objetos de esta clase para añadirlos a las listas
            foreach (Tener item in rolesAsignados)
            {
                // Consigo el nombre del rol
                String nombreRol = RolController.obtener(item.Codigo_rol)[0].Nombre;
                listaRolesAsignadosIniciales.Add(new ElementosListViewRoles() { Nombre = nombreRol, Dni_usuario = dni.Text, Codigo_rol = item.Codigo_rol });
            }
            // Reordena la lista y la duplico para que la lista inicial quede intacta y pueda usarla para comparar los roles iniciales con los que voy a guardar
            listaRolesAsignadosActualizada = listaRolesAsignadosIniciales.OrderBy(a => a.Nombre).ToList();
            // Cargo los items en el listView
            listViewRolesAsignados.ItemsSource = listaRolesAsignadosActualizada;
        }
        // Función para cargar las listas con los roles disponibles
        private void cargaListaRolesDisponibles()
        {
            // Cargamos todos los roles disponibles
            List<Rol> rolesDisponibles = RolController.listar();
            // Recorro la lista de roles disponibles y creo los objetos de esta clase para añadirlos a las listas
            foreach (Rol item in rolesDisponibles)
            {
                listaRolesDisponibles.Add(new ElementosListViewRoles() { Nombre = item.Nombre, Dni_usuario = dni.Text, Codigo_rol = item.Codigo });
            }
            // Cargo los items en el listView
            listViewRolesDisponibles.ItemsSource = listaRolesDisponibles;
        }

        // Función apara añadir roles a la lista de roles asignados
        private void anyadirRol(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean existeRol = false;
                // Se recorre la lista de roles asignados para comprobar si el item seleccionado de la lista disponibles se encuentra o no en la lista de roles asignados
                foreach (ElementosListViewRoles item in listaRolesAsignadosActualizada)
                {
                    if (item.Codigo_rol == rolDisponibleSeleccionado.Codigo_rol)
                    {
                        existeRol = true;
                    }
                }
                if (existeRol == false)
                {
                    listaRolesAsignadosActualizada.Add(new ElementosListViewRoles() { Nombre = rolDisponibleSeleccionado.Nombre, Dni_usuario = dni.Text, Codigo_rol = rolDisponibleSeleccionado.Codigo_rol });
                }
                this.actualizarlistView();
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de roles disponibles", "Item no seleccionado");
            }

        }
        //Función para quitar roles a la lista de roles asignados
        private void quitarRol(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se copia la lista que voy a recorrer, para que no haya problemas al borrar
                List<ElementosListViewRoles> listaAuxiliar = listaRolesAsignadosActualizada.ToList();
                foreach (ElementosListViewRoles item in listaAuxiliar)
                {
                    if (item.Codigo_rol == rolAsignadoSeleccionado.Codigo_rol)
                    {
                        listaRolesAsignadosActualizada.Remove(item);
                    }
                }
                this.actualizarlistView();
            }
            catch (System.NullReferenceException)
            {
                Mensajes.mensajeError("Selecciona un item de la lista de roles asignados", "Item no seleccionado");
            }
        }
        
        // Borrado de roles
        private void borrarRoles()
        {
            // Se almacenan los roles que estén en la lista inicial pero no en la actualizada, es decir, borra lo que esté en la lista inicial y no en la actualizada
            var borrarRoles = listaRolesAsignadosIniciales.Except(listaRolesAsignadosActualizada);
            foreach (ElementosListViewRoles item in borrarRoles)
            {
                TenerController.eliminar(item.Dni_usuario, item.Codigo_rol);
            }
        }
        // Insert de roles
        private void anyadirRoles()
        {
            // Se almacenan los roles que estén en la lista actualizada pero no en la inicial, es decir, añade lo que esté en la lista actualizada y no en la inicial
            var anyadirRoles = listaRolesAsignadosActualizada.Except(listaRolesAsignadosIniciales);
            foreach (ElementosListViewRoles item in anyadirRoles)
            {
                TenerController.insertar(item.Dni_usuario, item.Codigo_rol);
                // Al añadir el rol de alumno se mostrará una pantalla para que se le asigne un entrenador
                if (item.Codigo_rol == 2) {
                    // Se comprueba si el alumno existe en la tabla alumno
                    try
                    {
                        Alumno alumn = AlumnoController.obtener(dni.Text.ToLower())[0];
                    }
                    catch (ArgumentOutOfRangeException) // El alumno no existe y por lo tanto se inserta
                    {
                        AlumnoController.insertar(dni.Text.ToLower(),"","","");
                    }
                    finally {
                        // Se muestra la pantalla para asignar el entrenador
                        Mensajes.mensajeInformacion("A continuación podrás asignar un entrenador al alumno","Asignar entrenador");
                        asignacionRolAlumno = true;
                    }
                }
            }
        }


        // RADIOBUTTONS
        // Función para marcar el radiobutton cuando se carga la página
        private void marcarRadioButton(bool estado)
        {
            //Desde la base de datos se envía si la actividad está activa(true) o no lo esta (false), sabiendo esto se marca uno de los dos radiobuttons
            if (estado == true)
            {
                activo.IsChecked = true;
            }
            else
            {
                inactivo.IsChecked = true;
            }
        }
        // Función para recoger el radiobutton que está marcado
        private bool radioButtonActivado()
        {
            if (activo.IsChecked == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // BOTONES
        // Función para guardar los datos del usuario
        private void botonGuardar(object sender, RoutedEventArgs e)
        {
            // Recogemos la información de los campos de texto       
            string name = nombre.Text;
            string surname = apellidos.Text;
            string ident = dni.Text.ToLower();
            string town = localidad.Text;
            string address = direccion.Text;
            string postCode = cp.Text;
            string country = pais.Text;
            string phone = telefono.Text;
            string mail = email.Text;

            // Recogida de fechas
            // Fecha de nacimiento
            DateTime fechaNacimiento = (DateTime)nacimiento.SelectedDate;
            string birth = fechaNacimiento.ToString("yyyy-MM-dd");
            // Fecha de alta
            DateTime fechaAlta = (DateTime)alta.SelectedDate;
            string register = fechaAlta.ToString("yyyy-MM-dd");

            // Antes de insertar realizamos distintas validaciones
            // Validamos que el campo nombre y apellidos no estén en blanco
            if (Validaciones.campoEnBlanco("nombre", name) == false && Validaciones.campoEnBlanco("apellidos", surname) == false)
            {
                // Validamos la longitud del DNI, tiene que ser 9
                if (Validaciones.formatoDniNie(ident))
                {
                    // Validamos que el campo código postal sea numérico y de longitud = 5
                    if (Validaciones.campoNumerico("codigo postal", postCode) && Validaciones.longitudCampo("codigo postal", postCode, 5))
                    {
                        // Validamos que el campo teléfono sea numérico y tenga una longitud = 9
                        if (Validaciones.campoNumerico("teléfono", phone) && Validaciones.longitudCampo("teléfono", phone, 9))
                        {
                            // Validamos el formato del email
                            if (Validaciones.formatoMail(mail))
                            {
                                // Aquí se separa la gestión de la pantalla dependiendo el tipo de operación
                                if (tipoOperacion.Equals("crear"))
                                {
                                    //Validamos que no exista la clave primaria
                                    if (this.existeClavePrimaria(ident) == false)
                                    {
                                        // Comprobamos que el usuario selecciona la opción si en el MessageBox
                                        if (Mensajes.mensajeSiNo("Desea guardar los datos", "Guardar cambios"))
                                        {
                                            // Se crea el usuario
                                            UsuarioController.insertar(ident, name, surname, "", address, town, postCode, country, phone, mail, birth, register, this.radioButtonActivado(), true);
                                            // Se inserta una contraseña inicial
                                            CambiarPassword pass = new CambiarPassword(ident, "crear");
                                            pass.ShowDialog();
                                            // Se carga la pantalla de gestión de usuario para que se puedan añadir los roles
                                            ventanaInicioAdmin.framePrincipal.Content = new GestionarUsuario(ventanaInicioUsuario, ventanaInicioAdmin, ident, "editar");
                                        }
                                    }
                                }
                                else
                                {
                                    // Comprobamos que el usuario selecciona la opción si en el MessageBox
                                    if (Mensajes.mensajeSiNo("Desea guardar los datos", "Guardar cambios"))
                                    {
                                        // Se actualiza la base de datos
                                        UsuarioController.actualizar(ident, name, surname, user.Contrasenya, address, town, postCode, country, phone, mail, birth, register, this.radioButtonActivado(), user.Cambiar_password);
                                        // Borrado de roles que ya no tiene el usuario
                                        this.borrarRoles();
                                        // Asignación de nuevos roles del usuario
                                        this.anyadirRoles();

                                        if (asignacionRolAlumno == true)
                                        {
                                            ventanaInicioAdmin.framePrincipal.Content = new CambiarEntrenador(ventanaInicioUsuario, ident,"administrador");
                                        }
                                        else {
                                            // Una vez guardados los cambios, si no se ha asignado el rol alumno se carga la pantalla de lista de usuarios
                                            ventanaInicioAdmin.framePrincipal.Content = new ListaUsuarios(ventanaInicioAdmin, ventanaInicioUsuario);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        // Función para resetear el password
        private void botonResetPassword(object sender, RoutedEventArgs e)
        {
            UsuarioController.resetPassword(dni.Text.ToLower());
            CambiarPassword pass = new CambiarPassword(dni.Text.ToLower(), "reset");
            pass.ShowDialog();
        }


        // Función para comprobar si existe la clave primaria en la base de datos
        public Boolean existeClavePrimaria(string identificador)
        {
            Boolean existe = false;
            List<Usuario> lista = UsuarioController.listar();
            foreach (Usuario item in lista)
            {
                if (identificador.Equals(Convert.ToString(item.Dni)))
                {
                    existe = true;
                    Mensajes.mensajeInformacion("Ya existe un registro con el dni " + identificador, "Registro duplicado");
                    break;
                }
            }
            return existe;
        }
    }

    //Clase para gestionar los elementos del listView roles asignados
    public class ElementosListViewRoles
    {
        private string nombre;
        private string dni_usuario;
        private int codigo_rol;

        public ElementosListViewRoles()
        {

        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        public int Codigo_rol { get => codigo_rol; set => codigo_rol = value; }
    }
}
