using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.DTOs
{
    public class DtoConsultaHorario
    {
        public Guid IdDoctor { get; set; }
        public DateTime Fecha { get; set; }
    }
}
