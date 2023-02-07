using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int Edad { get; set; }
        public decimal Peso { get; set; }
        
        public string Sexo { get; set; }

        public Paciente(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, int edad, decimal peso, string sexo)
        {
            Id = id;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Edad = edad;
            Peso = peso;
            Sexo = sexo;
        }


        public Paciente( string nombre, string apellidoPaterno, string apellidoMaterno, int edad, decimal peso, string sexo)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Edad = edad;
            Peso = peso;
       
            Sexo = sexo;
        }

        public static Paciente Crear(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, int edad, decimal peso, string sexo)
        {
            return new Paciente(id, nombre, apellidoPaterno, apellidoMaterno, edad, peso, sexo);
        }

        public static Paciente Crear( string nombre, string apellidoPaterno, string apellidoMaterno, int edad, decimal peso, string sexo)
        {
            return new Paciente(nombre, apellidoPaterno, apellidoMaterno, edad, peso, sexo);
        }
    }
}
