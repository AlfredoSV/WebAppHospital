using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.DTOs
{
    public class DtoDoctor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Especialidad { get; set; }
        public string Departamento { get; set; }
        public string NumCedula { get; set; }
    }
}
