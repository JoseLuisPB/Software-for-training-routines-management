using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final_DAM.modelo
{
    class Contener
    {
        private int codigo_rutina;
        private int codigo_actividad;
        private int series;
        private int repeticiones;
        private int total;

        public Contener(int codigo_rutina, int codigo_actividad, int series, int repeticiones, int total)
        {
            this.Codigo_rutina = codigo_rutina;
            this.Codigo_actividad = codigo_actividad;
            this.Series = series;
            this.Repeticiones = repeticiones;
            this.Total = total;
        }

        public int Codigo_rutina { get => codigo_rutina; set => codigo_rutina = value; }
        public int Codigo_actividad { get => codigo_actividad; set => codigo_actividad = value; }
        public int Series { get => series; set => series = value; }
        public int Repeticiones { get => repeticiones; set => repeticiones = value; }
        public int Total { get => total; set => total = value; }
    }
}
