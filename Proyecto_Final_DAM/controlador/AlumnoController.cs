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
    class AlumnoController
    {
        
        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Alumno> obtener(string dni)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/alumno/{dni}", Method.GET);
            request.AddUrlSegment("dni", dni);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Alumno>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(string dni_usuario, string entrenador_asignado, string dolencias, string objetivo)
        {
            Alumno alumne = new Alumno(dni_usuario,entrenador_asignado,dolencias,objetivo);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/alumno", Method.POST);
            request.AddJsonBody(alumne);
            rest.Execute(request);
        }
        // Función que envía una petición al servidor para que actualice un registro en la tabla
        public static void actualizar(string dni_usuario, string entrenador_asignado, string dolencias, string objetivo)
        {
            Alumno alumne = new Alumno(dni_usuario, entrenador_asignado, dolencias, objetivo);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/alumno", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(alumne);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Datos actualizados con éxito", "Datos alumno actualizados");
            }
            else
            {
                Mensajes.mensajeError("Los datos no se han actualizado en la base de datos", "Error al actualizar");
            }
        }
    }
}
