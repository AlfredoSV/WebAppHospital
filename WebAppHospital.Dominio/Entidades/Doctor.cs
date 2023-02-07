using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Especialidad { get; set; }
        public string Departamento { get; set; }
        public string NumCedula { get; set; }

        public Doctor(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, string especialidad, string departamento, string numCedula)
        {
            Id = id;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Especialidad = especialidad;
            Departamento = departamento;
            NumCedula = numCedula;
        }

        public Doctor( string nombre, string apellidoPaterno, string apellidoMaterno, string especialidad, string departamento, string numCedula)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Especialidad = especialidad;
            Departamento = departamento;
            NumCedula = numCedula;
        }
        public static Doctor Crear(Guid id, string nombre, string apellidoPaterno, string apellidoMaterno, string especialidad, string departamento, string numCedula)
        {
            return new Doctor( id,  nombre,  apellidoPaterno,  apellidoMaterno,  especialidad,  departamento,   numCedula);
        }
        public static Doctor Crear( string nombre, string apellidoPaterno, string apellidoMaterno, string especialidad, string departamento, string numCedula)
        {
            return new Doctor(nombre, apellidoPaterno, apellidoMaterno, especialidad, departamento, numCedula);
        }
    }
}
