using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    public class listaPacientes
    {
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Direccion { set; get; }
        public string Telefono { set; get; }
        public string Edad { set; get; }
        public string Sexo { set; get; }
        public string Correo { set; get; }
        public string DNI { get; set; }
        public Uri Foto { get; set; }



        public listaPacientes(string nombre,string apellidos, string direccion, string telefono, string edad, string sexo, string correo, string dni, Uri foto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefono = telefono;
            Edad = edad;
            Sexo = sexo;
            Correo = correo;
            DNI = dni;
            Foto = foto;

        }
    }
}
