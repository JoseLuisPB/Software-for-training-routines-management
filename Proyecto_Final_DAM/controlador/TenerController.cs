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
    class TenerController
    {
        // Función que envía una petición al servidor para que obtenga los roles del usuario que se le pasa por parámetro
        public static List<Tener> obtener(string dni_usuario)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/tener/{dni_usuario}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Tener>>(response.Content);
        }
        // Función que envía una petición al servidor para que obtenga el número de roles que tiene un usuario.
        public static List<Tener> numRolesUsuario(string dni_usuario)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/numRolesUsuario/{dni_usuario}", Method.GET);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Tener>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro
        public static void insertar(string dni_usuario, int codigo_rol)
        {
            Tener objTener = new Tener(dni_usuario, codigo_rol);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/tener", Method.POST);
            request.AddHeader("Content.Type", "application/json");
            request.AddJsonBody(objTener);
            rest.Execute(request);
        }
        // Función que envía una petición al servidor para que elimine el registro que corresponda con el parámetro pasado
        public static void eliminar(string dni_usuario, int codigo_rol)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/tener/{dni_usuario}/{codigo_rol}", Method.DELETE);
            request.AddUrlSegment("dni_usuario", dni_usuario);
            request.AddUrlSegment("codigo_rol", codigo_rol);
            rest.Execute(request);
        }
    }
}
