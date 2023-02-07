using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppHospital.Dominio.DTOs
{
    public class DtoUsuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Contrasenia { get; set; }
        public Boolean Activo { get; set; }
        public string UsuarioNom { get; set; }
    }
}
