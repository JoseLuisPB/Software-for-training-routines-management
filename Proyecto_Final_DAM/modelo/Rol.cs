using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Rol
    {
        private int codigo;
        private string nombre;

        public Rol(int codigo, string nombre)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
    }
}
