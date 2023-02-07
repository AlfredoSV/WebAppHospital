using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.DTOs
{
    public class DtoCita
    {
        public Guid Id { get; set; }
        public Guid IdPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public Guid IdDoctor { get; set; }
        public string NombreDoctor { get; set; }
        public string Comentarios { get; set; }
        public string HorarioCita { get; set; }
        public DateTime FechaCita { get; set; }

    }
}
