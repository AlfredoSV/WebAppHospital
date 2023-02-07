using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppHospital.Models
{
    public class LoginRegistro
    {
        [Required(ErrorMessage = "Ingrese un usuario")]

        public string Usuario { get; set; }
        [Required(ErrorMessage = "Ingrese una contraseña")]
        [DisplayName("Contraseña")]
        public string Contrasenia { get; set; }
        [Required(ErrorMessage = "Ingrese una nombre")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese una apellido paterno")]
        [DisplayName("Apellido paterno")]
        public string ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Ingrese una apellido materno")]
        [DisplayName("Apellido materno")]
        public string ApellidoMaterno { get; set; }
    }
}