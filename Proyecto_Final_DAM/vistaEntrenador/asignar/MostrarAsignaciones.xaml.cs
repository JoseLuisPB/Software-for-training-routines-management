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
using System.Printing;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Markup;
using System.Net.Mail;
using System.Globalization;
using System.Threading;

namespace Proyecto_Final_DAM.vistaEntrenador.asignar
{
    /// <summary>
    /// Lógica de interacción para MostrarAsignaciones.xaml
    /// </summary>
    public partial class MostrarAsignaciones : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioAsignarRutinas ventanaInicioAsignarRutinas;
        string dni;
        string nombreCompleto;

        DateTime inicio;
        DateTime fin;
        List<Realizar> listaRutinasARealizar;
        List<DatosRutina> listaDatosRutinaRango;
        List<DatosRutina> listViewActualizada;

        // Variables usadas para dar formato al itemsource
        string fechaIterada;
        string ultimaFechaIterada;
        string rutinaIterada;
        string ultimaRutinaIterada;

        public MostrarAsignaciones(InicioUsuario ventanaRecibidaInicioUsuario, InicioAsignarRutinas ventanaRecibidaInicioAsignarRutinas, string ident, string name)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Mostrar rutinas asignadas";
            ventanaInicioAsignarRutinas = ventanaRecibidaInicioAsignarRutinas;
            dni = ident;
            nombreCompleto = name;
            nombreAlumno.Text = nombreCompleto;
            // Al tratarse de un elemento fuera del alcance del proyecto se deja deshabilitado
            //enviar.Visibility = Visibility.Collapsed;
            // Listas
            listaDatosRutinaRango = new List<DatosRutina>();
            // Fechas
            fechaFin.SelectedDate = DateTime.Today;
            fechaInicio.SelectedDate = DateTime.Today;

            // Caso de que el usuario conectado sea alumno
            if (ventanaInicioUsuario.roles.SelectedItem.Equals("alumno")) {
                spAlumno.Visibility = Visibility.Collapsed;
            }
            
            // Mostrar al inicio la rutina del día actual.
            this.mostrarFechas(null, null);
        }

        // Función para mostrar las rutinas asignadas en el rango de fechas seleccionadas
        private void mostrarFechas(object sender, RoutedEventArgs e)
        {
            // Se comparan las fechas seleccionadas para que la fecha de fin no sea mayor que la de inicio.
            if (this.comparacionFechas() != 1)
            {
                // Cada vez que se ejecuta una consulta se reinicia la lista
                listaDatosRutinaRango = new List<DatosRutina>();
                listaRutinasARealizar = RealizarController.listaRutinasAlumnoRango(dni, inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"));

                // Se recorren todos los ítems de la lista de rutinas
                foreach (Realizar item in listaRutinasARealizar)
                {
                    string rutina = RutinaController.obtener(item.Codigo_rutina)[0].Nombre;
                    // Se crea una lista con las actividades que contiene la rutina
                    List<Contener> listaActividadesRutina = ContenerController.listaActividadesRutina(item.Codigo_rutina);
                    // Se recorren los items de la rutina
                    foreach (Contener elemento in listaActividadesRutina)
                    {
                        string actividad = ActividadController.obtener(elemento.Codigo_actividad)[0].Nombre;
                        listaDatosRutinaRango.Add(new DatosRutina() { Fecha = item.Fecha, NombreRutina = rutina, NombreEjercicio = actividad, Series = Convert.ToString(elemento.Series), Repeticiones = Convert.ToString(elemento.Repeticiones), Ejecutada = item.Ejecutada });
                    }
                }
                //Reinicio de fechas y rutinas
                fechaIterada = "no";
                ultimaFechaIterada = "no";
                rutinaIterada = "no";
                ultimaRutinaIterada = "no";
                this.actualizarListView();
            }
            else
            {
                Mensajes.mensajeError("La fecha inicial no puede ser superior a la fecha final", "Rango de fechas incorrectas");
            }
        }

        // Función que se lanza cuando se cambia la fecha de inicio y sirve ajustar la fecha de fin cuando la de inicio es superior a ella.
        private void ajustarFechaFin(object sender, SelectionChangedEventArgs e)
        {
            if (this.comparacionFechas() == 1) {
                fechaFin.SelectedDate = fechaInicio.SelectedDate;
            }
        }

        // Función que sirve para comparar dos fechas. Si el resultado de la comparación es = 1 significa que la fecha de fin es inferior a la de inicio
        private int comparacionFechas() {

            // Recogida de fechas y transoformo el DateTime completo a su formato de fecha corto
            string fechaDesde = fechaInicio.SelectedDate.Value.Date.ToShortDateString();
            string fechaHasta = fechaFin.SelectedDate.Value.Date.ToShortDateString();
            // Transformación de fechas para poder compararlas y usarlas posteriormente
            inicio = DateTime.Parse(fechaDesde);
            fin = DateTime.Parse(fechaHasta);

            int compararFechas = DateTime.Compare(inicio, fin);
            return compararFechas;
        }
        // Función para transformar el formato de la fecha
        private string formatoFecha(string fecha) {
            String[] fechaSpliteada = fecha.Split('-');
            return fechaSpliteada[2] + "/" + fechaSpliteada[1] + "/" + fechaSpliteada[0];
        }

        // Función para actualizar el listview
        private void actualizarListView()
        {
            listViewActualizada = new List<DatosRutina>();
            // En este foreach se recorrerá toda la lista de rutinas y se irán creando los elementos de forma que no se repitan los datos
            foreach (DatosRutina item in listaDatosRutinaRango)
            {
                fechaIterada = item.Fecha;
                rutinaIterada = item.NombreRutina;
                // Transformación del estado de la rutina de booleano a string
                if (item.Ejecutada == true)
                {
                    item.EjecutadaString = "Sí";
                }
                else
                {
                    item.EjecutadaString = "No";
                }
                //Transformación del formato de la fecha
                string fechaTransformada = this.formatoFecha(item.Fecha);
                // Se comprueba que la fecha no se repita
                if (fechaIterada.Equals(ultimaFechaIterada)) // La fecha se repite
                {
                    if (rutinaIterada.Equals(ultimaRutinaIterada)) // El nombre de la rutina se repite
                    {
                        listViewActualizada.Add(new DatosRutina() { Fecha = "", NombreRutina = "", NombreEjercicio = item.NombreEjercicio, Series = Convert.ToString(item.Series), Repeticiones = Convert.ToString(item.Repeticiones) });
                    }
                    else
                    { // El nombre de la rutina no se repite
                        listViewActualizada.Add(new DatosRutina() { Fecha = fechaTransformada, NombreRutina = item.NombreRutina, NombreEjercicio = item.NombreEjercicio, Series = Convert.ToString(item.Series), Repeticiones = Convert.ToString(item.Repeticiones), Ejecutada = item.Ejecutada, EjecutadaString = item.EjecutadaString });
                    }
                }
                else
                { // La fecha no se repite
                    listViewActualizada.Add(new DatosRutina() { Fecha = fechaTransformada, NombreRutina = item.NombreRutina, NombreEjercicio = item.NombreEjercicio, Series = Convert.ToString(item.Series), Repeticiones = Convert.ToString(item.Repeticiones), Ejecutada = item.Ejecutada, EjecutadaString = item.EjecutadaString });
                }
                ultimaFechaIterada = item.Fecha;
                ultimaRutinaIterada = item.NombreRutina;

            }
            // Se asigna la lista al listview
            listViewRangoRutinas.ItemsSource = listViewActualizada;
        }
        
        private void botonEnviarMail(object sender, RoutedEventArgs e)
        {
            Mensajes.mensajeInformacion("La funcionalidad está deshabilitada", "Función deshabilitada");
            /*
            Boolean primeraPasada = true;
            // Ajustar día al idioma pertinente
            var culture = new System.Globalization.CultureInfo("es-ES");
            Usuario user = UsuarioController.obtener(dni)[0];
            string mensaje = "Hola " + user.Nombre + " a continuación te detallo las actividades que has de ejecutar desde el " + culture.DateTimeFormat.GetDayName(inicio.DayOfWeek) + " " + inicio.ToString("dd-MM-yyyy") + " hasta el " + culture.DateTimeFormat.GetDayName(fin.DayOfWeek) + " " + fin.ToString("dd-MM-yyyy") + "\n";
            string ultimaFechaIterada = "";

            foreach (DatosRutina item in listaDatosRutinaRango) {
                
                DateTime diaSemana = Convert.ToDateTime(formatoFecha(item.Fecha));
                var dia = culture.DateTimeFormat.GetDayName(diaSemana.DayOfWeek);
                string fechaIterada = item.Fecha;
                
                if (ultimaFechaIterada.Equals(fechaIterada))
                {
                    mensaje += String.Format("{0,-50} {1,-20} {2,-10}\n", "Actividad: " + item.NombreEjercicio, "Series: " + item.Series, "Repeticiones: " + item.Repeticiones);
                }
                else {
                    if (primeraPasada == false) {
                        mensaje += this.guiones() +"\n";
                    }
                    mensaje += "\n" + dia.ToUpper() + " " + this.formatoFecha(fechaIterada) + "\n";
                    mensaje += this.guiones() + "\n";
                    mensaje += String.Format("{0,-50} {1,-20} {2,-10}\n", "Actividad: " + item.NombreEjercicio, "Series: " + item.Series, "Repeticiones: " + item.Repeticiones);
                }
                primeraPasada = false;
                ultimaFechaIterada = fechaIterada;
            }
            mensaje += this.guiones() + "\n";
            mensaje += "\n" + " ¡¡¡ Buen entrenamiento !!!".ToUpper();
            this.enviarMail(mensaje, user.Email, inicio.ToString("dd-MM-yyyy"), fin.ToString("dd-MM-yyyy"));
            */
        }
        /*
        // Función para dar formato
        private string guiones() {
            return "---------------------------------------------------------------------------------------------------------";
        }

        // Función para enviar un correo al usuario
        private void enviarMail(string cuerpo, string mail, string fecha1, string fecha2)
        {

            try
            {
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true;
                // Desde aquí la aplicación se conecta a la cuenta de correo que se le indica
                mailServer.Credentials = new System.Net.NetworkCredential("Cuenta de correo", "password de la cuenta de correo");

                string from = "Cuenta de correo";
                string to = mail; // Correo al que se enviará el mail
                MailMessage msg = new MailMessage(from, to);
                msg.Subject = "Actividades desde el " + fecha1 + " hasta el " + fecha2;
                msg.Body = cuerpo;
                mailServer.Send(msg);

                Mensajes.mensajeInformacion("Actividades enviadas con éxito", "Actividades enviadas");
            }
            catch (Exception ex)
            {
                Mensajes.mensajeError("No se pudo enviar el correo. Error : " + ex, "Email no enviado");
            }
        }*/
        
    }
    public class DatosRutina
    {
        private string fecha;
        private string nombreRutina;
        private string nombreEjercicio;
        private string series;
        private string repeticiones;
        private Boolean ejecutada;
        private string ejecutadaString;

        public DatosRutina()
        {
        }
        public string Fecha { get => fecha; set => fecha = value; }
        public string NombreRutina { get => nombreRutina; set => nombreRutina = value; }
        public string NombreEjercicio { get => nombreEjercicio; set => nombreEjercicio = value; }
        public string Series { get => series; set => series = value; }
        public string Repeticiones { get => repeticiones; set => repeticiones = value; }
        public bool Ejecutada { get => ejecutada; set => ejecutada = value; }
        public string EjecutadaString { get => ejecutadaString; set => ejecutadaString = value; }
    }
}
