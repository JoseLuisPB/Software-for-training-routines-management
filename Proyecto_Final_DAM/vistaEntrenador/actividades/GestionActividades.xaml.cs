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

namespace Proyecto_Final_DAM.vistaEntrenador.actividades
{
    /// <summary>
    /// Lógica de interacción para GestionActividades.xaml
    /// </summary>
    public partial class GestionActividades : Page
    {
        InicioUsuario ventanaInicioUsuario;
        InicioActividades ventanaInicioActividades;
        Actividad activity;
        int codigoActividad;
        String tipoDeAccion;
        public GestionActividades(InicioUsuario ventanaRecibidaInicioUsuario, InicioActividades ventanaRecibidaInicioActvidades,int codigo, string accion)
        {
            InitializeComponent();
            ventanaInicioUsuario = ventanaRecibidaInicioUsuario;
            ventanaInicioUsuario.titulo.Text = "Crear actividad";
            ventanaInicioActividades = ventanaRecibidaInicioActvidades;
            codigoActividad = codigo;
            tipoDeAccion = accion;
            activa.IsChecked = true;

            if (!tipoDeAccion.Equals("crear"))
            {
                activity = ActividadController.obtener(codigo)[0];
                ventanaInicioUsuario.titulo.Text = "Gestión de la actividad";
                nombre.Text = activity.Nombre;
                tipoActividad.Text = activity.Tipo;
                nivel.Text = activity.Nivel;
                this.marcarRadioButton(activity.Activa);
            }

        }
        // Función para guardar los datos
        private void botonGuardar(object sender, RoutedEventArgs e) {

            if (tipoDeAccion.Equals("crear"))
            {
                // Se pregunta al usuario si quiere guardar, en caso afirmativo se procede a guardar
                if (Mensajes.mensajeSiNo("Desea guardar la actividad", "Guardar actividad"))
                {
                    try
                    {

                        int numeroActividad = ActividadController.ultimaActividadCreada()[0].Codigo;
                        ActividadController.insertar(numeroActividad + 1, nombre.Text, tipoActividad.Text, nivel.Text, "", this.radioButtonActivado(), Sesion.Dni);
                    }
                    // Al crear la primera inserción en la base de datos dará un error porque no encontrará el numero de la última actividad creada, se indica 1 a mano
                    catch (System.ArgumentOutOfRangeException)
                    {
                        ActividadController.insertar(1, nombre.Text, tipoActividad.Text, nivel.Text, "", this.radioButtonActivado(), Sesion.Dni);
                    }
                }
            }
            else {
                if (Mensajes.mensajeSiNo("Desea Actualizar la actividad", "Guardar actividad"))
                {
                    ActividadController.actualizar(activity.Codigo, nombre.Text, tipoActividad.Text, nivel.Text, activity.Imagen, this.radioButtonActivado(), activity.Dni_usuario);
                }    
            }
            ventanaInicioActividades.frameActividades.Content = new ListaActividades(ventanaInicioUsuario, ventanaInicioActividades);
        
        }
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
    }
}
