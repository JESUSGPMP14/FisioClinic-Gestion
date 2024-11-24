using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABORATORIO
{
    public class PacientesAtendidos
    {
        public string Nombre2 { get; set; }
        public string DNI2 { get; set; }
    }

    public class Horarios
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
    }

    public class listaPersonalSanitario
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Correo { get; set; }
        public string DNI { get; set; }

        public List<PacientesAtendidos> PacientesAtendidos { get; set; }
        public List<Horarios> Horarios { get; set; }

        public listaPersonalSanitario(string nombre, string apellidos, string telefono, string edad, string sexo, string correo, string dni)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Edad = edad;
            Sexo = sexo;
            Correo = correo;
            DNI = dni;

            // Inicializar las listas
            PacientesAtendidos = new List<PacientesAtendidos>();
            Horarios = new List<Horarios>();
        }

        
    }
}