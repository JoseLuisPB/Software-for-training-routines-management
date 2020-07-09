using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Realizar
    {
        private string dni_usuario;
        private int codigo_rutina;
        private string fecha;
        private Boolean ejecutada;


        public Realizar(string dni_usuario, int codigo_rutina, string fecha, bool ejecutada)
        {
            this.Dni_usuario = dni_usuario;
            this.Codigo_rutina = codigo_rutina;
            this.Fecha = fecha;
            this.Ejecutada = ejecutada;
        }

        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        public int Codigo_rutina { get => codigo_rutina; set => codigo_rutina = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public bool Ejecutada { get => ejecutada; set => ejecutada = value; }
        
    }
}
