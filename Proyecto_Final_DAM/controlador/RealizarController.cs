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
    class RealizarController
    {
        // Función que envía una petición al servidor para que devuelva las rutinas de un alumno en un rango de fechas
        public static List<Realizar> listaRutinasAlumnoRango(string dni_usuario, string fecha1, string fecha2)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaRutinasAlumnoRango/{dni_usuario}/{fecha1}/{fecha2}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("fecha1", fecha1);
            request.AddUrlSegment("fecha2", fecha2);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Realizar>>(response.Content);
        }
        // Función que envía una petición al servidor para que devuelva las rutinas asignadas al usuario para una fecha en concreto
        public static List<Realizar> obtener(string dni_usuario, string fecha)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/realizar/{dni_usuario}/{fecha}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("fecha", fecha);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Realizar>>(response.Content);
        }
        // Función que envía una petición al servidor para que devuelva la rutina asignada a un usuario en una fecha concreta
        public static List<Realizar> rutinaFecha(string dni_usuario, int codigo_rutina, string fecha)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutinaFecha/{dni_usuario}/{codigo_rutina}/{fecha}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            request.AddUrlSegment("fecha", fecha);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Realizar>>(response.Content);
        }

        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(string dni_usuario, int codigo_rutina, string fecha, bool ejecutada)
        {
            Realizar hacer = new Realizar(dni_usuario, codigo_rutina, fecha, ejecutada);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/realizar", Method.POST);
            request.AddJsonBody(hacer);
            rest.Execute(request);
        }

        // Función que envía una petición al servidor para que borre un registro de la tabla
        public static void eliminar(string dni_usuario, int codigo_rutina, string fecha)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/realizar/{dni_usuario}/{codigo_rutina}/{fecha}", Method.DELETE);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            request.AddUrlSegment("fecha", fecha);
            rest.Execute(request);
        }
        // Función que envía una petición al servidor para que actualice con el valor true el campo ejecutada
        public static void rutinaEjecutada(string dni_usuario, int codigo_rutina, string fecha)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutinaEjecutada/{dni_usuario}/{codigo_rutina}/{fecha}", Method.PUT);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            request.AddUrlSegment("fecha", fecha);
            rest.Execute(request);
        }
        // Función que envía una petición al servidor para que actualice con el valor false el campo ejecutada
        public static void rutinaNoEjecutada(string dni_usuario, int codigo_rutina, string fecha)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutinaNoEjecutada/{dni_usuario}/{codigo_rutina}/{fecha}", Method.PUT);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            request.AddUrlSegment("fecha", fecha);
            rest.Execute(request);
        }

        // Función que envía una petición al servidor para que devuelva la rutina asignada a un usuario en una fecha concreta
        public static List<Realizar> estadoRutina(int codigo_rutina)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/estadoRutina/{codigo_rutina}", Method.GET);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Realizar>>(response.Content);
        }
    }
}
