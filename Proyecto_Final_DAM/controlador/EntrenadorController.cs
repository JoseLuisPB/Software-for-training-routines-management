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
    class EntrenadorController
    {
        // Función que envía una petición al servidor para obtener la lista de usuarios con el rol de entrenador y que están activos
        public static List<Entrenador> listaEntrenadoresActivos()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaEntrenadoresActivos", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Entrenador>>(response.Content);
        }

        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Entrenador> obtener(string dni)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/entrenador/{dni}", Method.GET);
            request.AddUrlSegment("dni", dni);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Entrenador>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(string dni_usuario, string biografia)
        {
            Entrenador trainer = new Entrenador(dni_usuario, biografia);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/entrenador", Method.POST);
            request.AddJsonBody(trainer);
            rest.Execute(request);
        }
        // Función que envía una petición al servidor para que actualice un registro en la tabla
        public static void actualizar(string dni_usuario, string biografia)
        {
            Entrenador trainer = new Entrenador(dni_usuario, biografia);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/entrenador", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(trainer);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Datos del actualizados con éxito", "Datos entrenador actualizados");
            }
            else
            {
                Mensajes.mensajeError("Los datos no se han actualizado en la base de datos", "Error al actualizar");
            }
        }
        /*
        // Función que envía una petición al servidor para modificar el campo activo con el valor si (usuario dado de alta)
        public static void borrarBiografia(string dni)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/borrarBiografia/{dni}", Method.PUT);
            request.AddUrlSegment("dni", dni);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Biografía borrada", "Borrar biografía");
            }
            else
            {
                Mensajes.mensajeError("La biografía no se ha borrado", "Error");
            }
        }
        */
        // Función que devuelve el número de alumnos que tiene asignado un entrenador
        public static List<Entrenador> numeroAlumnosAsignados(string dni) {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/numeroAlumnosAsignados/{dni}", Method.GET);
            request.AddUrlSegment("dni", dni);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Entrenador>>(response.Content);
        }
    }
}
