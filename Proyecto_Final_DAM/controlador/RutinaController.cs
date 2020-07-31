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
    class RutinaController
    {
        // Función que envía una petición al servidor para que devuelva todas las rutinas de la tabla
        public static List<Rutina> listar()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutina", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }
        // Función que envía una petición al servidor para que devuelva todas las rutinas del usuario
        public static List<Rutina> listaRutinasEntrenador(string dni_usuario)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaRutinasEntrenador/{dni_usuario}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }

        // Función que envía una petición al servidor para que devuelva todas las rutinas del usuario que estén activas
        public static List<Rutina> listaRutinasActivas(string dni_usuario)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaRutinasActivas/{dni_usuario}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }

        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Rutina> obtener(int codigo)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutina/{codigo}", Method.GET);
            request.AddUrlSegment("codigo", codigo);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(int codigo, string nombre, Boolean activa, string dni_usuario)
        {
            Rutina routine = new Rutina(codigo, nombre, activa, dni_usuario);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutina", Method.POST);
            request.AddJsonBody(routine);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("A continuación podrás asignar actividades a la rutina creada.", "Asignar actividades");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos", "Error al crear");
            }
        }
        // Función que envía una petición al servidor para que actualice un registro en la tabla
        public static void actualizar(int codigo, string nombre, bool activa, string dni_usuario)
        {
            Rutina routine = new Rutina(codigo, nombre, activa, dni_usuario);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rutina", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(routine);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Rutina actualizada con éxito", "Rutina actualizada");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha actualizado en la base de datos", "Error al actualizar");
            }
        }

        // Función que envía una petición al servidor para obtener el código de la última rutina creada
        public static List<Rutina> ultimaRutina()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/ultimaRutina", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }

        // Función que envía una petición al servidor para que busque las rutinas según el texto que se le pasa
        public static List<Rutina> busquedaNombreRutina(string nombre, string dni_usuario)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/busquedaNombreRutina/{nombre}/{dni_usuario}", Method.GET);
            request.AddUrlSegment("nombre", nombre);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rutina>>(response.Content);
        }
    }
}
