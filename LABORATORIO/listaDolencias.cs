using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    public class listaDolencias
    {
        public string Nombre { set; get; }
        public string Fecha { set; get; }
        public Uri RayosX { set; get; }
        public string Tratamiento { set; get; }

        public listaDolencias(string nombre, string fecha, string tratamiento, Uri foto)
        {
            Nombre = nombre;
            Fecha = fecha;
            Tratamiento = tratamiento;
            RayosX = foto;
        }
    }
}
