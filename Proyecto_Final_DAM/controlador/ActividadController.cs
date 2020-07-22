using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Proyecto_Final_DAM.modelo;

namespace Proyecto_Final_DAM.controlador
{
    class ActividadController
    {
        // Función que envía una petición al servidor para que devuelva todos los valores de la tabla
        public static List<Actividad> listar()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividad", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Actividad>>(response.Content);
        }
        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Actividad> obtener(int codigo)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividad/{codigo}", Method.GET);
            request.AddUrlSegment("codigo", codigo);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Actividad>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(int codigo, string nombre, string tipo, string nivel, string imagen, bool activa, string dni_usuario)
        {
            Actividad activity = new Actividad(codigo, nombre, tipo, nivel, imagen, activa, dni_usuario);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividad", Method.POST);
            request.AddJsonBody(activity);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Actividad creada con éxito", "Actividad creada");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos", "Error al crear");
            }
        }
        // Función que envía una petición al servidor para que actualice un registro en la tabla
        public static void actualizar(int codigo, string nombre, string tipo, string nivel, string imagen, bool activa, string dni_usuario)
        {
            Actividad activity = new Actividad(codigo, nombre, tipo, nivel, imagen, activa, dni_usuario);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividad", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(activity);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Actividad actualizada con éxito", "Actividad actualizada");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos", "Error al actualizar");
            }
        }
        /*
        // Función que envía una petición al servidor para modificar el campo obsoleta con el valor no (Actividad activa)
        public static void altaActividad(int codigo)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/altaActividad/{codigo}", Method.PUT);
            request.AddUrlSegment("codigo", codigo);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Actividad dada de alta", "Alta de actividad");
            }
            else
            {
                Mensajes.mensajeError("La actividad no ha sido dada de alta", "Error");
            }
        }

        // Función que envía una petición al servidor para modificar el campo activo obsoleta el valor si (Actividad inactiva)
        public static void bajaActividad(int codigo)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/bajaActividad/{codigo}", Method.PUT);
            request.AddUrlSegment("codigo", codigo);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Actividad dada de baja", "Baja de actividad");
            }
            else
            {
                Mensajes.mensajeError("La actividad no ha sido dada de baja", "Error");
            }
        }
        */
        // Función que envía una petición al servidor para que devuelva el codigo de la última actividad creada
        public static List<Actividad> ultimaActividadCreada()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/ultimaActividadCreada", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Actividad>>(response.Content);
        }

        // Función que envía una petición al servidor para que devuelva los tipos de actividad existentes
        public static List<Actividad> tipoActividad()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/tipoActividad", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Actividad>>(response.Content);
        }

        // Función que envía una petición al servidor para que devuelva todas las actividades que correspondan con el tipo que se le ha pasado por parámetro
        public static List<Actividad> actividadesPorTipo(string tipoActividad)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividadesPorTipo/{tipoActividad}", Method.GET);
            request.AddUrlSegment("tipoActividad", tipoActividad);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Actividad>>(response.Content);
        }
    }
}
