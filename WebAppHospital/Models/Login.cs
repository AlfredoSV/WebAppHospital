using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppHospital.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Ingrese un usuario")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Ingrese una contraseña")]
        public string Contrasenia { get; set; }
    }
}