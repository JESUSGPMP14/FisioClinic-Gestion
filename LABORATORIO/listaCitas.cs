using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    public class listaCitas
    {
        public string Nombre { set; get; }
        public string Fecha { set; get; }
        public string Hora { set; get; }
        public string PerEncargado { set; get; }
        public string Area { set; get; }
        public string Observaciones { set; get; }


        public listaCitas(string nombre, string fecha, string hora, string perencargado, string area, string observaciones)
        {
            Nombre = nombre;
            Fecha = fecha;
            Hora = hora;
            PerEncargado = perencargado;
            Area = area;
            Observaciones = observaciones;
        }
    }
}
