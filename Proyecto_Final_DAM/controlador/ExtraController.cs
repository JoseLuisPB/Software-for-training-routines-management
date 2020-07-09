using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Proyecto_Final_DAM.controlador
{
    class ExtraController
    {
        //Función que llama a la API y detiene el servidor
        public static void detener()
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/detener", Method.GET);
            rest.Execute(request);
        }
    }
}
