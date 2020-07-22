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
    class UsuarioController
    {
        // Función que envía una petición al servidor para que devuelva todos los valores de la tabla
        public static List<Usuario> listar()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/usuario", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }
        // Función que envía una petición al servidor para que obtenga los alumnos asignados al entrenador que se le pasa por parámetro, sólo muestra usuarios activos
        public static List<Usuario> listaUsuarioActivosAsignados(string entrenador_asignado)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaUsuarioActivosAsignados/{entrenador_asignado}", Method.GET);
            request.AddUrlSegment("entrenador_asignado", entrenador_asignado);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }

        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Usuario> obtener(string dni)
        {
            // Esta variable guarda una instancia de RestClient, recibe como argumento la url de nuestro servidor
            var rest = new RestClient("http://localhost:3000");
            // Se guarda una instancia de RestRequest indicando la URL y el parámetro que se le pasa, ademas indicamos el tipo de petición HTTP a realizar
            var request = new RestRequest("/usuario/{dni}", Method.GET);
            // Aquí se indica que el parámetro de la url anterior sea igual al que le pasamos al invocar el método
            request.AddUrlSegment("dni", dni);
            // Creamos una variable response que guarde la ejecución de la petición
            var response = rest.Execute(request);
            //Creamos un return para que deserialice el contenido de la respuesta y lo convierta a una lista Usuario, node devuelve un array de objetos JSON y RestSharp obliga a deserializar en List<T>
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(string dni, string nombre, string apellidos, string contrasenya, string direccion, string localidad, string cp, string pais, string telefono, string email, string fecha_nacimiento, string fecha_alta, bool activo, bool cambiar_password)
        {
            Usuario user = new Usuario(dni, nombre, apellidos, contrasenya, direccion, localidad, cp, pais, telefono, email, fecha_nacimiento, fecha_alta, activo, cambiar_password);
            // Esta variable guarda una instancia de RestClient, recibe como argumento la url de nuestro servidor
            var rest = new RestClient("http://localhost:3000");
            // Se guarda una instancia de RestRequest indicando la URL de nuestra API RESTFul y el tipo de petición
            var request = new RestRequest("/usuario", Method.POST);
            // Le pasamos a la variable request el objeto con el método AddJsonBody se convierte el objeto a JSON añadiéndolo al cuerpo de la petición HTTP
            request.AddJsonBody(user);
            // Enviamos la petición a la API RESTFul, guardamos el resultado en una variable
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("A continuación puedes asignar los roles al usuario que acaba de crear", "Usuario creado");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos","Error al crear");
            }
        }
        // Función que envía una petición al servidor para que actualice un registro en la tabla
        public static void actualizar(string dni, string nombre, string apellidos, string contrasenya, string direccion, string localidad, string cp, string pais, string telefono, string email, string fecha_nacimiento, string fecha_alta, bool activo, bool cambiar_password)
        {
            Usuario user = new Usuario(dni, nombre, apellidos, contrasenya, direccion, localidad, cp, pais, telefono, email, fecha_nacimiento, fecha_alta, activo, cambiar_password);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/usuario", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(user);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                Mensajes.mensajeInformacion("Usuario actualizado con éxito", "Usuario actualizado");
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha actualizado en la base de datos", "Error al actualizar");
            }
        }
        // Función que envía una petición al servidor para modificar el campo cambiar password (resetear el password)
        public static void resetPassword(string dni)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/resetPassword/{dni}", Method.PUT);
            request.AddUrlSegment("dni", dni);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {

            }
            else
            {
                Mensajes.mensajeError("El password no se ha reiniciado", "Error");
            }
        }

        // Función que envía una petición al servidor para modificar el campo cambiar password (no resetear el password)
        public static void actualizarPassword(string contrasenya, string dni)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actualizarPassword/{contrasenya}/{dni}", Method.PUT);
            request.AddUrlSegment("contrasenya", contrasenya);
            request.AddUrlSegment("dni", dni);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
               
            }
            else
            {
                Mensajes.mensajeError("El password no se ha reiniciado", "Error");
            }
        }

        // Filtros

        // Función que envía una petición al servidor para que devuelva todos los usuarios activos
        public static List<Usuario> listaActivos()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaActivos", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }

        // Función que envía una petición al servidor para que devuelva todos los usuarios inactivos
        public static List<Usuario> listaInactivos()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("listaInactivos", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }

        // Función que envía una petición al servidor para que busque los usuarios según el texto que se le pasa
        public static List<Usuario> busquedaNombre(string nombre)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/busquedaNombre/{nombre}", Method.GET);
            request.AddUrlSegment("nombre", nombre);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
        }
    }
}
