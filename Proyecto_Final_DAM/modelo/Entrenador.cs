using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Entrenador
    {
        private string dni_usuario;
        private string biografia;
        private string total;

        public Entrenador() { 
        
        }
        public Entrenador(string dni_usuario, string biografia)
        {
            this.Dni_usuario = dni_usuario;
            this.Biografia = biografia;
        }

        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        public string Biografia { get => biografia; set => biografia = value; }
        public string Total { get => total; set => total = value; }
    }
}
