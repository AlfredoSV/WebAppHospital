using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Models
{
    public class HorarioCitaModelSelect
    {
        public Guid Id { get; set; }
        public string HoraInicioFin { get; set; }
   
       
    }

    public class PacienteModelSelect
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

    }

    public class DoctorModelSelect
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

    }
}
