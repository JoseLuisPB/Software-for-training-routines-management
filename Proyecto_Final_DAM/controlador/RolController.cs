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
    class RolController
    {
        // Función que envía una petición al servidor para que devuelva todos los valores de la tabla
        public static List<Rol> listar()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rol", Method.GET);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rol>>(response.Content);
        }
        // Función que envía una petición al servidor para que obtenga un registro en la tabla
        public static List<Rol> obtener(int codigo)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/rol/{codigo}", Method.GET);
            request.AddUrlSegment("codigo", codigo);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Rol>>(response.Content);
        }
    }
}
