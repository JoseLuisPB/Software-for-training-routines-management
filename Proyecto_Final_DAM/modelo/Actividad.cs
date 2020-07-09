using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Actividad
    {
        private int codigo;
        private string nombre;
        private string tipo;
        private string nivel;
        private string imagen;
        private Boolean activa;
        private string dni_usuario;

        public Actividad(int codigo, string nombre, string tipo, string nivel, string imagen, bool activa, string dni_usuario)
        {
            this.Codigo = codigo;
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Nivel = nivel;
            this.Imagen = imagen;
            this.Activa = activa;
            this.Dni_usuario = dni_usuario;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Nivel { get => nivel; set => nivel = value; }
        public string Imagen { get => imagen; set => imagen = value; }
        public bool Activa { get => activa; set => activa = value; }
        public string Dni_usuario { get => dni_usuario; set => dni_usuario = value; }
        
    }
}
