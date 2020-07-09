using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Tener
    {
        private String dni_usuario;
        private int codigo_rol;
        private int total;

        public Tener(string dni_usuario, int codigo_rol)
        {
            this.Dni_usuario = dni_usuario;
            this.Codigo_rol = codigo_rol;
        }

        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        public int Codigo_rol { get => codigo_rol; set => codigo_rol = value; }
        public int Total { get => total; set => total = value; }
    }
}
