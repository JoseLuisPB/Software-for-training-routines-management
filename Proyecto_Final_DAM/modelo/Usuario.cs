using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Usuario
    {
        private string dni;
        private string nombre;
        private string apellidos;
        private string contrasenya;
        private string direccion;
        private string localidad;
        private string cp;
        private string pais;
        private string telefono;
        private string email;
        private string fecha_nacimiento;
        private string fecha_alta;
        private bool activo;
        private bool cambiar_password;

        public Usuario(string dni, string nombre, string apellidos, string contrasenya, string direccion, string localidad, string cp, string pais, string telefono, string email, string fecha_nacimiento, string fecha_alta, bool activo, bool cambiar_password)
        {
            this.Dni = dni;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Contrasenya = contrasenya;
            this.Direccion = direccion;
            this.Localidad = localidad;
            this.Cp = cp;
            this.Pais = pais;
            this.Telefono = telefono;
            this.Email = email;
            this.Fecha_nacimiento = fecha_nacimiento;
            this.Fecha_alta = fecha_alta;
            this.Activo = activo;
            this.Cambiar_password = cambiar_password;
        }

        public string Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Contrasenya { get => contrasenya; set => contrasenya = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Localidad { get => localidad; set => localidad = value; }
        public string Cp { get => cp; set => cp = value; }
        public string Pais { get => pais; set => pais = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        public string Fecha_nacimiento { get => fecha_nacimiento; set => fecha_nacimiento = value; }
        public string Fecha_alta { get => fecha_alta; set => fecha_alta = value; }
        public Boolean Activo { get => activo; set => activo = value; }
        public Boolean Cambiar_password { get => cambiar_password; set => cambiar_password = value; }
    }
}
