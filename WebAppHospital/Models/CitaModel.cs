using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppHospital.Models
{
    public class CitaModel
    {
        public Guid Id { get; set; }
      
        public Guid IdPaciente { get; set; }
        public string NombrePaciente { get; set; }

        [Required(ErrorMessage = "Sleccione un doctor")]
        public Guid IdDoctor { get; set; }
        public string NombreDoctor { get; set; }
        [Required(ErrorMessage = "Ingrese comentarios")]

        public string Comentarios { get; set; }
        [Required(ErrorMessage = "Seleccione un horario de cita")]

        public string Horario { get; set; }
        [Required(ErrorMessage = "Seleccione una fecha de la cita")]

        public DateTime FechaCita { get; set; }

    }
}