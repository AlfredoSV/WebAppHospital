using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppHospital.Models
{
    public class PacienteModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre")]
        
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una apellido paterno")]
        [DisplayName("Apellido paterno")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Ingrese una apellido materno")]
        [DisplayName("Apellido materno")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Ingrese una edad")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "Ingrese un peso")]
        public decimal Peso { get; set; }
        [Required(ErrorMessage = "Ingrese una fecha de nacimiento")]
    
   
        public string Sexo { get; set; }
    }
}