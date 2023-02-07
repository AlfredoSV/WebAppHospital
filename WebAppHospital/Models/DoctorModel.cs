using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppHospital.Models
{
    public class DoctorModel
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
        [Required(ErrorMessage = "Ingrese una especialidad")]

        public string Especialidad { get; set; }
        [Required(ErrorMessage = "Ingrese una especialidad")]

        public string Departamento { get; set; }
        [Required(ErrorMessage = "Ingrese un número de cedula")]
        [DisplayName("NHúmero de cedula")]
        public string NumCedula { get; set; }
    }
}