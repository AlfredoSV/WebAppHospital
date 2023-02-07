using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class DtoHoarioCita
    {
        public Guid Id { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool Disponible { get; set; }

       
    }
}
