using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Alumno
    {
        private string dni_usuario;
        private string dolencias;
        private string entrenador_asignado;
        private string objetivo;
        
        public Alumno(string dni_usuario, string entrenador_asignado, string dolencias, string objetivo)
        {
            this.Dni_usuario = dni_usuario;
            this.Entrenador_asignado = entrenador_asignado;
            this.Dolencias = dolencias;
            this.Objetivo = objetivo;
            
        }

        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        public string Entrenador_asignado { get => entrenador_asignado; set => entrenador_asignado = value; }
        public string Dolencias { get => dolencias; set => dolencias = value; }
        public string Objetivo { get => objetivo; set => objetivo = value; }
        
    }
}
