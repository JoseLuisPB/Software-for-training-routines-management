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
    class ContenerController
    {
        // Función que envía una petición al servidor para que obtenga una lista de registros en la tabla
        public static List<Contener> listaActividadesRutina(int codigo_rutina)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/listaActividadesRutina/{codigo_rutina}", Method.GET);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Contener>>(response.Content);
        }
        // Función que envía una petición al servidor para que inserte un registro en la tabla
        public static void insertar(int codigo_rutina, int codigo_actividad, int series, int repeticiones, int total)
        {
            Contener contingut = new Contener(codigo_rutina, codigo_actividad, series, repeticiones, total);
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/contener", Method.POST);
            request.AddJsonBody(contingut);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {
                
            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos", "Error al crear");
            }
        }
        public static void eliminar(int codigo_rutina)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/contener/{codigo_rutina}}", Method.DELETE);
            request.AddUrlSegment("codigo_rutina", codigo_rutina);
            var resultado = rest.Execute(request);
            if ((int)resultado.StatusCode == 200)
            {

            }
            else
            {
                Mensajes.mensajeError("El registro no se ha insertado en la base de datos", "Error al crear");
            }
        }
        // Función para obtener la lista de actividades realizadas en un rango de fechas determinado
        public static List<Contener> actividadesEjecutadasRango(string dni, string fechaInicio, string fechaFin)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/actividadesEjecutadasRango/{dni}/{fechaInicio}/{fechaFin}", Method.GET);
            request.AddUrlSegment("dni", dni);
            request.AddUrlSegment("fechaInicio", fechaInicio);
            request.AddUrlSegment("fechaFin", fechaFin);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Contener>>(response.Content);
        }
        // Función para obtener el total de repeticiones ejecutadas en un rango de fechas determinado
        public static List<Contener> totalRepeticiones(string dni, int codigoActividad, string fechaInicio, string fechaFin)
        {
            var rest = new RestClient("http://localhost:3000");
            var request = new RestRequest("/totalRepeticiones/{dni}/{codigoActividad}/{fechaInicio}/{fechaFin}", Method.GET);
            request.AddUrlSegment("dni", dni);
            request.AddUrlSegment("codigoActividad", codigoActividad);
            request.AddUrlSegment("fechaInicio", fechaInicio);
            request.AddUrlSegment("fechaFin", fechaFin);
            var response = rest.Execute(request);
            return JsonConvert.DeserializeObject<List<Contener>>(response.Content);
        }
    }
}
