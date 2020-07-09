using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Rutina
    {
        private int codigo;
        private string nombre;
        private bool activa;
        private string dni_usuario;

        public Rutina(int codigo, string nombre, bool activa, string dni_usuario)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Activa = activa;
            this.Dni_usuario = dni_usuario;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public bool Activa { get => activa; set => activa = value; }
        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        
    }
}
