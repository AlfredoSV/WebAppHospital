using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.Entidades
{
    public class HoarioCita
    {
        public Guid Id { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool Disponible { get; set; }

        public HoarioCita(Guid id, string horaInicio, string horaFin, bool disponible)
        {
            Id = id;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Disponible = disponible;
        }

        public static HoarioCita Crear(Guid id, string horaInicio, string horaFin, bool disponible)
        {
            return new HoarioCita(  id,   horaInicio,   horaFin,   disponible);
        }
    }
}
