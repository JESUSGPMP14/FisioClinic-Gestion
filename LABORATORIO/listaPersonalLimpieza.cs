using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    public class listaPersonalLimpieza
    {
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Telefono { set; get; }
        public string Edad { set; get; }
        public string Sexo { set; get; }
        public string Correo { set; get; }
        public string DNI { get; set; }



        public listaPersonalLimpieza(string nombre, string apellidos, string telefono, string edad, string sexo, string correo, string dni)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Edad = edad;
            Sexo = sexo;
            Correo = correo;
            DNI = dni;

        }
    }
}